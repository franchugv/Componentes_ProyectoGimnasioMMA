using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Persona;
using BibliotecaClases_ProyectoGimnasioMMA.Personas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes.Funciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Componentes_ProyectoGimnasioMMA.Componentes.GestionPersonas.Editar
{
    public class EditarAlumnos : FormularioPersona
    {
        // RECURSOS
        protected EntryConfirmacion _eDNI;
        protected EntryConfirmacion _eNombre;
        protected EntryConfirmacion _eApellidos;
        protected PickerConfirmacion _pickerCategoriaEdad;

        protected PickerConfirmacion _pickerNuevaEscuela;
        protected PickerConfirmacion _pickerEliminarEscuela;

        protected List<Escuela> _listaEscuelasAgregar;
        protected List<Escuela> _listaEscuelasEliminar;

        protected Alumno _alumnoAntiguo;

        protected Button _botonEditar;

        public event Action EventoVolverPaginaPrincipal;

        // CONSTRUCTOR
        public EditarAlumnos(Escuela escuela, Usuario usuario, Alumno alumno) : base(escuela, usuario)
        {
            _listaEscuelasAgregar = _api_bd.ListarEscuelasDisponiblesAlumnoNoRegistrado(usuario.Correo, alumno.DNI);
            _listaEscuelasEliminar = _api_bd.ListarEscuelasDeAlumno(alumno.DNI);
            _alumnoAntiguo = alumno;
            GenerarUI();
        }

        // INICIALIZACIÓN
        protected override void GenerarUI()
        {

            List<string> listaNombresEscuelasAgregar = new List<string>();
            List<string> listaNombresEscuelasEliminar = new List<string>();

            foreach (Escuela escuela in _listaEscuelasAgregar)
            {
                listaNombresEscuelasAgregar.Add(escuela.Nombre);
            }
            foreach (Escuela escuela in _listaEscuelasEliminar)
            {
                listaNombresEscuelasEliminar.Add(escuela.Nombre);
            }
            // Inctanciar Componentes de la interfaz
            _eDNI = GeneracionUI.CrearEntryConfirmacion("Ingrese un nuevo DNI", "eDNI", 10, entryUnfocus);
            _eNombre = GeneracionUI.CrearEntryConfirmacion("Ingrese un nuevo Nombre", "eNombre", 50, entryUnfocus);
            _eApellidos = GeneracionUI.CrearEntryConfirmacion("Ingrese unos nuevos Apellidos", "eApellidos", 100, entryUnfocus);

            _pickerCategoriaEdad = GeneracionUI.CrearPickerConfirmacion("pCategoriaEdad", "Seleccione su categoría de Edad", Alumno.ObtenerCategoriasEdad, pickerFocusChanged);
            _pickerNuevaEscuela = GeneracionUI.CrearPickerConfirmacion("sEscuela", "Seleccione una Escuela a Agregar", listaNombresEscuelasAgregar, pickerFocusChanged);
            if(listaNombresEscuelasAgregar.Count <= 0)
            {
                _pickerNuevaEscuela.PickerEditar.IsEnabled = false;
            }
            _pickerEliminarEscuela = GeneracionUI.CrearPickerConfirmacion("sEscuela", "Seleccione una Escuela a Eliminar", listaNombresEscuelasEliminar, pickerFocusChanged);
            if (listaNombresEscuelasEliminar.Count <= 0)
            {
                _pickerEliminarEscuela.PickerEditar.IsEnabled = false;
            }
            _botonEditar = GeneracionUI.CrearBoton("Editar Alumno", "bEditar", controladorBotones);
            _botonEditar.BackgroundColor = Colors.Green;

            // Añadir interfaz al vsl
            MAIN_VSL.Children.Add(
                _eDNI
            );
            MAIN_VSL.Children.Add(
                _eNombre
            );
            MAIN_VSL.Children.Add(
                _eApellidos
            );
            MAIN_VSL.Children.Add(
                _pickerCategoriaEdad
            );
            MAIN_VSL.Children.Add(
                _pickerNuevaEscuela
            );
            MAIN_VSL.Children.Add(
                _pickerEliminarEscuela
            );
            VSL_BOTON.Children.Add(
                _botonEditar
            );

            AsignarDatos();
        }
        
        private void AsignarDatos()
        {
            _eDNI.Texto = _alumnoAntiguo.DNI;
            _eNombre.Texto = _alumnoAntiguo.Nombre;
            _eApellidos.Texto = _alumnoAntiguo.Apellidos;
            _pickerCategoriaEdad.PickerEditar.SelectedItem = _alumnoAntiguo.CategoriaEdadAlumno;           
        }

        // EVENTOS
        protected override void entryUnfocus(object sender, FocusEventArgs e)
        {
            Entry entry = (Entry)sender;
            PersonaValidacion persona = null;

            try
            {
                switch (entry.StyleId)
                {
                    case "eDNI":
                        _eDNI.limpiarError();
                        persona = new PersonaValidacion(_eDNI.Texto, TipoMiembro.DNI);
                        break;

                    case "eNombre":
                        _eNombre.limpiarError();
                        persona = new PersonaValidacion(_eNombre.Texto, TipoMiembro.Nombre);
                        break;

                    case "eApellidos":
                        _eApellidos.limpiarError();
                        persona = new PersonaValidacion(_eApellidos.Texto, TipoMiembro.Apellidos);
                        break;



                }

            }
            catch (Exception error)
            {
                switch (entry.StyleId)
                {
                    case "eDNI":
                        _eDNI.mostrarError(error.Message);
                        break;
                    case "eNombre":
                        _eNombre.mostrarError(error.Message);
                        break;
                    case "eApellidos":
                        _eApellidos.mostrarError(error.Message);
                        break;

                }
            }

        }

        private async void controladorBotones(object sender, EventArgs e)
        {
            try
            {
                bool confirmar = await GeneracionUI.MostrarConfirmacion(Application.Current.MainPage, "Ventana confirmación", $"¿Desea Actualizar a Alumno {_alumnoAntiguo.Nombre}?");

                if (confirmar)
                {
                    ActualizarAlumno();
                }
              

            }
            catch (Exception error)
            {
                await Application.Current.MainPage.DisplayAlert("Error", error.Message, "Ok");
            }

        }

        private void ActualizarAlumno()
        {
            // Variables locales
            Escuela escuelaAgregar = null;
            Escuela escuelaEliminar = null;

            // Validación
            if ((_pickerCategoriaEdad.EstaSeleccionado) && (_pickerCategoriaEdad.PickerEditar.SelectedItem == null)) throw new Exception("Seleccione una Categoría de Edad");

            // Asignar Escuela a Agregar
            if (_pickerNuevaEscuela.EstaSeleccionado)
            {
                if (_pickerNuevaEscuela.PickerEditar.SelectedItem == null) throw new Exception("Seleccione una Escuela a Agregar");
                for (int indice = 0; indice < _listaEscuelasAgregar.Count; indice++)
                {
                    if (_pickerNuevaEscuela.PickerEditar.SelectedItem.ToString() == _listaEscuelasAgregar[indice].Nombre) escuelaAgregar = _listaEscuelasAgregar[indice];
                }

            }

            // Asignar Escuela a Eliminar
            if (_pickerEliminarEscuela.EstaSeleccionado)
            {
                if (_pickerEliminarEscuela.PickerEditar.SelectedItem == null) throw new Exception("Seleccione una Escuela a Eliminar");
                for (int indice = 0; indice < _listaEscuelasEliminar.Count; indice++)
                {
                    if (_pickerEliminarEscuela.PickerEditar.SelectedItem.ToString() == _listaEscuelasEliminar[indice].Nombre) escuelaEliminar = _listaEscuelasEliminar[indice];
                }
            }


            // Crear relación Escuela/Alumno
            if (_pickerNuevaEscuela.EstaSeleccionado && escuelaAgregar != null)
                _api_bd.CrearRelacionEscuelaAlumno(_alumnoAntiguo, escuelaAgregar.Id);

            // Eliminar Relación Escuela/Alumno
            if (_pickerEliminarEscuela.EstaSeleccionado && escuelaEliminar != null)
            {
                // Controlar que no podamos dejar a un alumno sin escuelas
                if (_listaEscuelasEliminar.Count <= 1) throw new Exception("No puedes dejar a un alumno sin Escuelas");
                _api_bd.EliminarRelacionEscuelaAlumno(_alumnoAntiguo, escuelaEliminar.Id);
            }


            Alumno alumno = new Alumno(_eDNI.Texto, _eNombre.Texto, _eApellidos.Texto, Alumno.StringToCategoriaEdad(_pickerCategoriaEdad.PickerEditar.SelectedItem.ToString()));
            _api_bd.ValidarRepeticionDNIProfesor(alumno.DNI, _escuela.Id);

            // Actualizar Alumno
            _api_bd.ActualizarAlumno(_alumnoAntiguo.DNI, alumno.DNI ,alumno.Nombre, alumno.Apellidos, _pickerCategoriaEdad.PickerEditar.SelectedItem.ToString());

            EventoVolverPaginaPrincipal?.Invoke();
        }
    }
}
