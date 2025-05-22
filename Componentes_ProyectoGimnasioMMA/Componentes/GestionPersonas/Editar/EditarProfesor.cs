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

        // CONSTRUCTOR
        public EditarProfesor(Escuela escuela, Usuario usuario, Profesores profesoresEditar) : base(escuela, usuario)
        {
            _profesorEditar = profesoresEditar;  
            CargarDatosConstructor();


        }

        // INICIALIZACIÓN
        protected override void GenerarUI()
        {

            //  Instanciar componentes 
            _eCDNI = GeneracionUI.CrearEntryConfirmacion("Ingrese un nuevo DNI", "eCDNI", 10, entryUnfocus);
            _eCNombre = GeneracionUI.CrearEntryConfirmacion("Ingrese un nuevo Nombre", "eCNombre", 50, entryUnfocus);
            _eCApellidos = GeneracionUI.CrearEntryConfirmacion("Ingrese nuevos Apellidos", "eCApellidos", 100, entryUnfocus);
            _eNivel = GeneracionUI.CrearEntryConfirmacion("Ingrese un nuevo nivel del Profesor", "eCNivel", 50, entryUnfocus);

            _selectorEscuelaNuevaAgregar = GeneracionUI.CrearPickerConfirmacion("sEscuelaAgregar", "Seleccione una Escuela a Agregar", _listaNombresEscuelasAgregar, pickerFocusChanged);

            _selectorEscuelaViejaEliminar = GeneracionUI.CrearPickerConfirmacion("sEscuelaEliminar", "Seleccione una Escuela a Eliminar", _listaNombresEscuelasElimiminar, pickerFocusChanged);

            _pSelectorUsuarioNuevo = GeneracionUI.CrearPickerConfirmacion("sUsuario", "Seleccione un Usuario a Cambiar", _listaNombresUsuariosDisponibles, pickerFocusChanged);

            _pDeporteNuevoAgregar = GeneracionUI.CrearPickerConfirmacion("pDeporteAgregar", "Seleccione un deporte nuevo para el Profesorado", _listaNombreDeportesAgregar, selectedIndexChanged);

            _pDeporteViejoBorrar = GeneracionUI.CrearPickerConfirmacion("pDeporteEliminar", "Seleccione un deporte a Eliminar para el Profesorado", _listaNombreDeportesEliminar, selectedIndexChanged);

            _botonInsertar = GeneracionUI.CrearBoton("Actualizar Profesor", "bInsertar", controladorBoton);
            _botonInsertar.BackgroundColor = Colors.Green;

            // Layout 
            VerticalStackLayout layout = new VerticalStackLayout
            {
                Padding = new Thickness(25, 0),
                Spacing = 8, // Separación 
                Children =
                {
                    new Label { Text = "Gestión del Profesorado", FontSize = 20, FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center },

                    // Datos personales 
                    new Label { Text = "DNI:" },
                    _eCDNI,
                    new Label { Text = "Nombre:" },
                    _eCNombre,
                    new Label { Text = "Apellidos:" },
                    _eCApellidos,
                    new Label { Text = "Nivel del Profesor:" },
                    _eNivel,

                    new BoxView { HeightRequest = 1, Color = Colors.LightGray },

                    // Escuelas 
                    new Label { Text = "Agregar Escuela:" },
                    _selectorEscuelaNuevaAgregar,
                    new Label { Text = "Eliminar Escuela:" },
                    _selectorEscuelaViejaEliminar,

                    new BoxView { HeightRequest = 1, Color = Colors.LightGray },

                    // Deportes 
                    new Label { Text = "Agregar Deporte:" },
                    _pDeporteNuevoAgregar,
                    new Label { Text = "Eliminar Deporte:" },
                    _pDeporteViejoBorrar,

                    new BoxView { HeightRequest = 1, Color = Colors.LightGray },

                    // Usuario 
                    new Label { Text = "Cambiar Usuario Vinculado:" },
                    _pSelectorUsuarioNuevo,

                    new BoxView { HeightRequest = 1, Color = Colors.LightGray },

                    
                }
            };

            // Agregar al layout principal
            MAIN_VSL.Children.Add(layout);

            VSL_BOTON.Add(_botonInsertar);

                        // Asignar datos
                        AsignarDatos();


        }

        private void AsignarDatos()
        {
            _eCDNI.Texto = _profesorEditar.DNI;
            _eCNombre.Texto = _profesorEditar.Nombre;
            _eCApellidos.Texto = _profesorEditar.Apellidos;
            _eNivel.Texto = _profesorEditar.Nivel;
            _pSelectorUsuarioNuevo.PickerEditar.SelectedItem = _profesorEditar.CorreoProfesor;
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

        // EVENTOS
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
        private async void controladorBoton(object sender, EventArgs e)
        {
            try
            {
                bool confirmar = await GeneracionUI.MostrarConfirmacion(Application.Current.MainPage, "Ventana confirmación", $"¿Desea Actualizar a Profesor {_profesorEditar.Nombre}?");
                
                if (confirmar)
                {
                    ActualizarProfesor();
                }
            }
            catch(Exception error)
            {
                await Application.Current.MainPage.DisplayAlert("ERROR", error.Message, "Ok");
            }
        }

        private void ActualizarProfesor()
        {
            try
            {
                // Recursos


                // Tablas Intermedias
                Escuela escuelaVieja = null;
                Escuela nuevaEscuela = null;

                Deporte deporteViejoAEliminar = null;
                Deporte deporteNuevo = null;

                // VALIDACIÓN
                //if (_pSelectorUsuarioNuevo.PickerEditar.SelectedItem == null) throw new Exception("Para poder cambiar de usuario debe seleccionar uno");
                //if (_pDeporteNuevoAgregar.EstaSeleccionado && _pDeporteViejoBorrar.EstaSeleccionado) throw new Exception("Solo podemos seleccionar o un deporte a Agregar o un deporte a Eliminar, no Ambos");

                // Asignar Deporte a un profesor

                if (_pDeporteNuevoAgregar.EstaSeleccionado)
                {
                    foreach (Deporte deporte in _listaDeportesAgregar)
                    {
                        if (deporte.Nombre == _pDeporteNuevoAgregar.PickerEditar.SelectedItem.ToString()) deporteNuevo = deporte;
                    }
                    _api_bd.AsignarDeporteAProfesor(_profesorEditar.DNI, deporteNuevo.Id);
                }

                // Eliminar Deporte de un profesor
                if (_pDeporteViejoBorrar.EstaSeleccionado)
                {
                    foreach (Deporte deporte in _listaDeportesEliminar)
                    {
                        if (deporte.Nombre == _pDeporteViejoBorrar.PickerEditar.SelectedItem.ToString()) deporteViejoAEliminar = deporte;
                    }
                    _api_bd.EliminarDeporteDeProfesor(deporteViejoAEliminar.Id, _profesorEditar.DNI);
                }


                // Agregar Profesor a Escuela
                if (_selectorEscuelaNuevaAgregar.EstaSeleccionado)
                {
                    foreach (Escuela escuela in _listaEscuelasAgregar)
                    {
                        if (escuela.Nombre == _selectorEscuelaNuevaAgregar.PickerEditar.SelectedItem.ToString()) nuevaEscuela = escuela;
                    }
                    _api_bd.AsignarEscuelaAProfesor(_profesorEditar.DNI, nuevaEscuela.Id);
                }

                // Eliminar Profesor de Escuela
                if (_selectorEscuelaViejaEliminar.EstaSeleccionado)
                {
                    foreach (Escuela escuela in _listaEscuelasEliminar)
                    {
                        if (escuela.Nombre == _selectorEscuelaViejaEliminar.PickerEditar.SelectedItem.ToString()) escuelaVieja = escuela;
                    }
                    _api_bd.EliminarProfesorDeEscuela(_profesorEditar.DNI, escuelaVieja.Id);
                }



                Profesores profesor = new Profesores(_eCNombre.Texto, _eCApellidos.Texto, _eCDNI.Texto, _eNivel.Texto, _pSelectorUsuarioNuevo.PickerEditar.SelectedItem.ToString());
                _api_bd.ValidarRepeticionDNIAlumno(profesor.DNI, _escuela.Id);

                _api_bd.ActualizarProfesor(_profesorEditar.DNI, profesor.DNI, profesor.Nombre, profesor.Apellidos, profesor.Nivel, profesor.CorreoProfesor);

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
