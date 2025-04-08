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
        public EditarAlumnos(Escuela escuela, Usuario usuario, Alumno alumno) : base(escuela, usuario)
        {
            _listaEscuelasAgregar = _api_bd.ListarEscuelasDisponiblesAlumnoNoRegistrado(usuario.Correo, alumno.DNI);
            _listaEscuelasEliminar = _api_bd.ListarEscuelasDeAlumno(alumno.DNI);
            _alumnoAntiguo = alumno;
            GenerarUI();
        }


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
            _eDNI = GeneracionUI.CrearEntryConfirmacion("DNI", "eDNI", entryUnfocus);
            _eNombre = GeneracionUI.CrearEntryConfirmacion("Nombre", "eNombre", entryUnfocus);
            _eApellidos = GeneracionUI.CrearEntryConfirmacion("Apellidos", "eApellidos", entryUnfocus);

            _pickerCategoriaEdad = GeneracionUI.CrearPickerConfirmacion("pCategoriaEdad", "Seleccione su categoría de Edad", Alumno.ObtenerCategoriasEdad, pickerFocusChanged);
            _pickerNuevaEscuela = GeneracionUI.CrearPickerConfirmacion("sEscuela", "Seleccione una Escuela a Agregar", listaNombresEscuelasAgregar, pickerFocusChanged);
            if(listaNombresEscuelasAgregar.Count > 0)
            {
                _pickerNuevaEscuela.PickerEditar.IsEnabled = false;
                _pickerNuevaEscuela.CheckBoxP.IsEnabled = false;
            }
            _pickerEliminarEscuela = GeneracionUI.CrearPickerConfirmacion("sEscuela", "Seleccione una Escuela a Eliminar", listaNombresEscuelasEliminar, pickerFocusChanged);
            if (listaNombresEscuelasEliminar.Count > 0)
            {
                _pickerEliminarEscuela.PickerEditar.IsEnabled = false;
                _pickerEliminarEscuela.CheckBoxP.IsEnabled = false;
            }
            _botonEditar = GeneracionUI.CrearBoton("Editar Alumno", "bEditar", controladorBotones);

            // Añadir interfaz al vsl

            MAIN_VSL.Children.Add(
                _eNombre
            );
            MAIN_VSL.Children.Add(
                _eApellidos
            );
            MAIN_VSL.Children.Add(
                _pickerNuevaEscuela
            );
            MAIN_VSL.Children.Add(
                _pickerEliminarEscuela
            );
            MAIN_VSL.Children.Add(
                _botonEditar
            );
        }

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


        private void controladorBotones(object sender, EventArgs e)
        {
            try
            {
                // Variables locales
                string dni = null;
                string nombre = null;
                string apellidos = null;
                string categoriaEdad = null;

                Escuela escuelaAgregar = null;
                Escuela escuelaEliminar = null;

                if (_eDNI.EstaSeleccionado)
                {
                    Alumno alumno = new Alumno(_eDNI.Texto, TipoMiembro.DNI);
                    dni = _eDNI.Texto;
                }
                if (_eNombre.EstaSeleccionado)
                {
                    Alumno alumno = new Alumno(_eNombre.Texto, TipoMiembro.Nombre);
                    nombre = _eNombre.Texto;
                }
                if (_eApellidos.EstaSeleccionado)
                {
                    Alumno alumno = new Alumno(_eApellidos.Texto, TipoMiembro.Apellidos);
                    apellidos = _eApellidos.Texto;
                }

                if (_pickerCategoriaEdad.EstaSeleccionado)
                {
                    if (_pickerCategoriaEdad.PickerEditar.SelectedItem == null) throw new Exception("Seleccione una Categoría de Edad");
                    categoriaEdad = _pickerCategoriaEdad.PickerEditar.SelectedItem.ToString();
                }

                if (_pickerNuevaEscuela.EstaSeleccionado)
                {
                    if (_pickerNuevaEscuela.PickerEditar.SelectedItem == null) throw new Exception("Seleccione una Escuela a Agregar");
                    for (int indice = 0; indice < _listaEscuelasAgregar.Count; indice++)
                    {
                        if (_pickerNuevaEscuela.PickerEditar.SelectedItem.ToString() == _listaEscuelasAgregar[indice].Nombre) escuelaAgregar = _listaEscuelasAgregar[indice];
                    }

                }
                if (_pickerEliminarEscuela.EstaSeleccionado)
                {
                    if (_pickerEliminarEscuela.PickerEditar.SelectedItem == null) throw new Exception("Seleccione una Escuela a Eliminar");
                    for (int indice = 0; indice < _listaEscuelasEliminar.Count; indice++)
                    {
                        if (_pickerEliminarEscuela.PickerEditar.SelectedItem.ToString() == _listaEscuelasEliminar[indice].Nombre) escuelaEliminar = _listaEscuelasEliminar[indice];
                    }
                }

                // TODO: Hacer update con la base de datos
                if (_pickerNuevaEscuela.EstaSeleccionado && escuelaAgregar != null)
                    _api_bd.CrearRelacionEscuelaAlumno(_alumnoAntiguo, escuelaAgregar.Id);

                if (_pickerEliminarEscuela.EstaSeleccionado && escuelaEliminar != null)
                {
                    // Controlar que no podamos dejar a un alumno sin escuelas
                    if (_listaEscuelasEliminar.Count <= 1) throw new Exception("No puedes dejar a un alumno sin Escuelas");
                    _api_bd.EliminarRelacionEscuelaAlumno(_alumnoAntiguo, escuelaEliminar.Id);
                }



                if (nombre != null || apellidos != null || categoriaEdad != null)
                    _api_bd.ActualizarAlumno(_alumnoAntiguo.DNI, nombre, apellidos, categoriaEdad);

                EventoVolverPaginaPrincipal?.Invoke();

            }
            catch (Exception error)
            {
                Application.Current.MainPage.DisplayAlert("Error", error.Message, "Ok");
            }

        }
    }
}
