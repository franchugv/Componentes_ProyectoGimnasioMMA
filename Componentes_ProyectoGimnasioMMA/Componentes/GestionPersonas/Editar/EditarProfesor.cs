using BibliotecaClases_ProyectoGimnasioMMA.Deportes;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Persona;
using BibliotecaClases_ProyectoGimnasioMMA.Personas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes.Funciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml;

namespace Componentes_ProyectoGimnasioMMA.Componentes.GestionPersonas.Editar
{
    public class EditarProfesor : FormularioPersona
    {
        // RECURSOS

        protected EntryConfirmacion _eCDNI;
        protected EntryConfirmacion _eCNombre;
        protected EntryConfirmacion _eCApellidos;
        protected PickerConfirmacion _selectorEscuelaViejaEliminar;
        protected PickerConfirmacion _selectorEscuelaNuevaAgregar;

        protected EntryConfirmacion _eNivel;
        
        protected PickerConfirmacion _pDeporteViejoBorrar;
        protected PickerConfirmacion _pDeporteNuevoAgregar;

        protected PickerConfirmacion _pSelectorUsuarioNuevo;

        protected Button _botonInsertar;

        // Listas de datos
        // La lista de escuelas esta creada en FormularioPersona
        protected List<string> _listaNombreEscuelas;

        // Listas para el picker de agregar deportes
        protected List<Deporte> _listaDeportesAgregar;
        protected List<string> _listaNombreDeportesAgregar;

        // Listas para el picker de eliminar deportes 
        protected List<Deporte> _listaDeportesEliminar;
        protected List<string> _listaNombreDeportesEliminar; 

        // Listas para el picker de cambiar usuario
        protected List<Usuario> _listaUsuariosDisponibles;
        protected List<string> _listaNombresUsuariosDisponibles;

        // Listas para el picker de agregar escuelas
        protected List<Escuela> _listaEscuelasAgregar;
        protected List<string> _listaNombresEscuelasAgregar;

        // Listas para el picker de eliminar escuelas
        protected List<Escuela> _listaEscuelasEliminar;
        protected List<string> _listaNombresEscuelasElimiminar;


        protected Profesores _profesorEditar;

        public event Action EventoVolverPaginaPrincipal;


        public EditarProfesor(Escuela escuela, Usuario usuario, Profesores profesoresEditar) : base(escuela, usuario)
        {
            _profesorEditar = profesoresEditar;  
            CargarDatosConstructor();


        }


        protected override void GenerarUI()
        {


            // Inctanciar Componentes de la interfaz
            _eCDNI = GeneracionUI.CrearEntryConfirmacion("DNI", "eCDNI", entryUnfocus);
            _eCNombre = GeneracionUI.CrearEntryConfirmacion("Nombre", "eCNombre", entryUnfocus);
            _eCApellidos = GeneracionUI.CrearEntryConfirmacion("Apellidos", "eCApellidos", entryUnfocus);


            
            _selectorEscuelaNuevaAgregar = GeneracionUI.CrearPickerConfirmacion("sEscuelaAgregar", "Seleccione una Escuela a Agregar", _listaNombresEscuelasAgregar, pickerFocusChanged);
            if (_listaNombresEscuelasAgregar.Count < 1)
            {
                _selectorEscuelaNuevaAgregar.PickerEditar.IsEnabled = false;
                _selectorEscuelaNuevaAgregar.CheckBoxP.IsEnabled = false;

            }

            _selectorEscuelaViejaEliminar = GeneracionUI.CrearPickerConfirmacion("sEscuelaEliminar", "Seleccione una Escuela a Eliminar", _listaNombresEscuelasElimiminar, pickerFocusChanged);
            if (_listaNombresEscuelasElimiminar.Count < 1)
            {
                _selectorEscuelaViejaEliminar.PickerEditar.IsEnabled = false;
                _selectorEscuelaViejaEliminar.CheckBoxP.IsEnabled = false;
            }

            _pSelectorUsuarioNuevo = GeneracionUI.CrearPickerConfirmacion("sUsuario", "Seleccione un Usuario a Cambiar", _listaNombresUsuariosDisponibles, pickerFocusChanged);
            if (_listaNombresUsuariosDisponibles.Count < 1)
            {
                _pSelectorUsuarioNuevo.PickerEditar.IsEnabled = false;
                _pSelectorUsuarioNuevo.CheckBoxP.IsEnabled = false;
            }

            _eNivel = GeneracionUI.CrearEntryConfirmacion("Inserte el nivel del Profesor", "eCNivel", entryUnfocus);
            
            _pDeporteNuevoAgregar = GeneracionUI.CrearPickerConfirmacion("pDeporteAgregar", "Seleccione un deporte nuevo para el Profesorado", _listaNombreDeportesAgregar, selectedIndexChanged);
            if (_listaNombreDeportesAgregar.Count < 1)
            {
                _pDeporteNuevoAgregar.PickerEditar.IsEnabled = false;
                _pDeporteNuevoAgregar.CheckBoxP.IsEnabled = false;

            }

            _pDeporteViejoBorrar = GeneracionUI.CrearPickerConfirmacion("pDeporteEliminar", "Seleccione un deporte a Eliminar para el Profesorado", _listaNombreDeportesEliminar, selectedIndexChanged);
            if (_listaNombreDeportesEliminar.Count < 1) 
            {
                _pDeporteViejoBorrar.PickerEditar.IsEnabled = false;
                _pDeporteViejoBorrar.CheckBoxP.IsEnabled =false;

            }

            _botonInsertar = GeneracionUI.CrearBoton("Actualizar Profesor", "bInsertar", controladorBoton);

            // Añadir interfaz al vsl
            MAIN_VSL.Children.Add(new Label() { Text = "Ingrese el DNI:"});
            MAIN_VSL.Children.Add(_eCDNI);
            MAIN_VSL.Children.Add(new Label() { Text = "Ingrese el Nombre:" });
            MAIN_VSL.Children.Add(_eCNombre);
            MAIN_VSL.Children.Add(new Label() { Text = "Ingrese los Apellidos:" });
            MAIN_VSL.Children.Add(_eCApellidos);
            MAIN_VSL.Children.Add(new Label() { Text = "Ingrese el Nivel:" });
            MAIN_VSL.Children.Add(_eNivel);

            MAIN_VSL.Children.Add(_selectorEscuelaNuevaAgregar);
            MAIN_VSL.Children.Add(_selectorEscuelaViejaEliminar);

            MAIN_VSL.Children.Add(_pDeporteNuevoAgregar);
            MAIN_VSL.Children.Add(_pDeporteViejoBorrar);

            MAIN_VSL.Children.Add(_pSelectorUsuarioNuevo);
            MAIN_VSL.Children.Add(_botonInsertar);

            AsignarDatos();

        }


        private void AsignarDatos()
        {
            _eCDNI.Texto = _profesorEditar.DNI;
            _eCNombre.Texto = _profesorEditar.Nombre;
            _eCApellidos.Texto = _profesorEditar.Apellidos;
            _eNivel.Texto = _profesorEditar.Nivel;
        }
        private void CargarDatosConstructor()
        {
            try
            {
                _listaDeportesAgregar = new List<Deporte>();
                _listaNombreDeportesAgregar = new List<string>();
                // Cargar lista de deportes que un profesor no tenga asignado AGREGAR
                _listaDeportesAgregar = _api_bd.ObtenerDeportesSinAsignarAProfesor(_escuela.Id, _profesorEditar.DNI);

                // Asignar los nombres 
                if (_listaDeportesAgregar.Count > 0)
                {
                    _listaNombreDeportesAgregar = new List<string>();

                    foreach (Deporte deporte in _listaDeportesAgregar)
                    {
                        _listaNombreDeportesAgregar.Add(deporte.Nombre);
                    }
                }


                _listaDeportesEliminar = new List<Deporte>();
                _listaNombreDeportesEliminar = new List<string>();
                // Cargar lista de deportes de que un usuario SI tenga asignado ELIMINAR
                _listaDeportesEliminar = _api_bd.ObtenerDeportesAsignadosAProfesor(_escuela.Id, _profesorEditar.DNI);

                // Asignar los nombres 
                if (_listaDeportesEliminar.Count > 0)
                {
                    _listaNombreDeportesEliminar = new List<string>();

                    foreach (Deporte deporte in _listaDeportesEliminar)
                    {
                        _listaNombreDeportesEliminar.Add(deporte.Nombre);
                    }
                }


                // Asignar Usuarios Disponibles CAMBIAR
                _listaUsuariosDisponibles = new List<Usuario>();
                _listaUsuariosDisponibles = _api_bd.ObtenerUsuariosTipoProfesorSinProfesorAsignado(_escuela.Id);

                _listaNombresUsuariosDisponibles = new List<string>();
                if (_listaUsuariosDisponibles.Count > 0)
                {
                    _listaNombresUsuariosDisponibles = new List<string>();

                    foreach (Usuario usuario in _listaUsuariosDisponibles)
                    {
                        _listaNombresUsuariosDisponibles.Add(usuario.Correo);
                    }
                }


                // Asignar Lista de escuelas con el profesor asignado ELIMINAR
                _listaEscuelasEliminar = new List<Escuela>();
                _listaEscuelasEliminar = _api_bd.ObtenerEscuelasAsignadasAProfesorYUsuario(_profesorEditar.DNI, _usuario.Correo);
                _listaNombreEscuelas = new List<string>();

                if (_listaEscuelasEliminar.Count > 0)
                {
                    _listaNombresEscuelasElimiminar = new List<string>();
                    foreach (Escuela escuela in _listaEscuelasEliminar)
                    {
                        _listaNombresEscuelasElimiminar.Add(escuela.Nombre);
                    }
                }



                // Asignar Lista de escuelas Sin asignar AGREGAR

                _listaEscuelasAgregar = new List<Escuela>();
                _listaEscuelasAgregar = _api_bd.ObtenerEscuelasNoAsignadasAProfesorYUsuario(_profesorEditar.DNI, _usuario.Correo);
                _listaNombresEscuelasAgregar = new List<string>();

                if (_listaEscuelasAgregar.Count > 0)
                {
                    _listaNombresEscuelasAgregar = new List<string>();
                    foreach (Escuela escuela in _listaEscuelasAgregar)
                    {
                        _listaNombresEscuelasAgregar.Add(escuela.Nombre);
                    }
                }




                GenerarUI();

            }
            catch (Exception error)
            {
                Application.Current.MainPage.DisplayAlert("ERROR", error.Message, "Ok");
            }
        }

        protected override void entryUnfocus(object sender, FocusEventArgs e)
        {

            Entry entry = (Entry)sender;
            PersonaValidacion persona = null;

            try
            {
                switch (entry.StyleId)
                {
                    case "eCDNI":
                        _eCDNI.limpiarError();
                        persona = new PersonaValidacion(_eCDNI.Texto, TipoMiembro.DNI);
                        break;

                    case "eCNombre":
                        _eCNombre.limpiarError();
                        persona = new PersonaValidacion(_eCNombre.Texto, TipoMiembro.Nombre);
                        break;

                    case "eCApellidos":
                        _eCApellidos.limpiarError();
                        persona = new PersonaValidacion(_eCApellidos.Texto, TipoMiembro.Apellidos);
                        break;

                    case "_eCNivel":
                        _eNivel.limpiarError();
                        Profesores profesor = new Profesores(_eNivel.Texto, TipoValorProfesor.Nivel);
                        break;

                }

            }
            catch (Exception error)
            {
                switch (entry.StyleId)
                {
                    case "eCDNI":
                        _eCDNI.mostrarError(error.Message);
                        break;
                    case "eCNombre":
                        _eCNombre.mostrarError(error.Message);
                        break;
                    case "eCApellidos":
                        _eCApellidos.mostrarError(error.Message);
                        break;
                    case "_eCNivel":
                        _eNivel.mostrarError(error.Message);
                        break;


                }
            }

        }

        // Controlador donde haremos la insercción
        private void controladorBoton(object sender, EventArgs e)
        {
            try
            {
                // Recursos

                string dni = null;
                string nombre = null;
                string apellidos = null;
                string nivel = null;
                string correoUsuario = null;

                // Tablas Intermedias
                Escuela escuelaVieja = null;
                Escuela nuevaEscuela = null;

                Deporte deporteViejo = null;
                Deporte deporteNuevo = null;



                if (_eCDNI.EstaSeleccionado)
                {
                    PersonaValidacion profesor = new PersonaValidacion(_eCDNI.Texto, TipoMiembro.DNI);
                    dni = profesor.DNI;
                }
                if (_eCNombre.EstaSeleccionado)
                {
                    PersonaValidacion profesor = new PersonaValidacion(_eCNombre.Texto, TipoMiembro.Nombre);
                    nombre = profesor.Nombre;
                }
                if (_eCApellidos.EstaSeleccionado)
                {
                    PersonaValidacion profesor = new PersonaValidacion(_eCApellidos.Texto, TipoMiembro.Apellidos);
                    apellidos = profesor.Apellidos;
                }
                if (_eNivel.EstaSeleccionado)
                {
                    Profesores profesor = new Profesores(_eNivel.Texto, TipoValorProfesor.Nivel);
                    nivel = profesor.Nivel;
                }
                if (_pSelectorUsuarioNuevo.EstaSeleccionado)
                {
                    if (_pSelectorUsuarioNuevo.PickerEditar.SelectedItem == null) throw new Exception("Para poder cambiar de usuario debe seleccionar uno");
                    Profesores profesor = new Profesores(_pSelectorUsuarioNuevo.PickerEditar.SelectedItem.ToString(), TipoValorProfesor.CorreoProfesor);
                    nivel = profesor.Nivel;
                }


                if (_selectorEscuelaNuevaAgregar.EstaSeleccionado)
                {
                    foreach (Escuela escuela in _listaEscuelasAgregar)
                    {
                        if (escuela.Nombre == _selectorEscuelaNuevaAgregar.PickerEditar.SelectedItem.ToString()) nuevaEscuela = escuela;
                    }
                    _api_bd.AsignarEscuelaAProfesor(_profesorEditar.DNI, nuevaEscuela.Id);
                }

                if (_selectorEscuelaViejaEliminar.EstaSeleccionado)
                {
                    foreach (Escuela escuela in _listaEscuelasEliminar)
                    {
                        if (escuela.Nombre ==_selectorEscuelaViejaEliminar.PickerEditar.SelectedItem.ToString()) escuelaVieja = escuela;
                    }
                    _api_bd.EliminarProfesorDeEscuela(_profesorEditar.DNI, escuelaVieja.Id);
                }
                if (_pDeporteNuevoAgregar.EstaSeleccionado)
                {
                    foreach (Deporte deporte in _listaDeportesAgregar)
                    {
                        if (deporte.Nombre == _pDeporteNuevoAgregar.PickerEditar.SelectedItem.ToString()) deporteNuevo = deporte;
                    }
                    _api_bd.AsignarDeporteAProfesor(_profesorEditar.DNI, deporteNuevo.Id);
                }
                if (_pDeporteViejoBorrar.EstaSeleccionado)
                {
                    foreach (Deporte deporte in _listaDeportesEliminar)
                    {
                        if (deporte.Nombre == _pDeporteViejoBorrar.PickerEditar.SelectedItem.ToString()) deporteViejo = deporte;
                    }
                    _api_bd.AsignarDeporteAProfesor(_profesorEditar.DNI, deporteViejo.Id);
                }

                if(nombre != null || apellidos != null || nivel != null || correoUsuario != null)
                {
                    _api_bd.ActualizarProfesor(_profesorEditar.DNI, nombre, apellidos, nivel, correoUsuario);
                }

                EventoVolverPaginaPrincipal?.Invoke();

            }
            catch (Exception error)
            {
                Application.Current.MainPage.DisplayAlert("ERROR", error.Message, "Ok");
            }
        }

        private void selectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
