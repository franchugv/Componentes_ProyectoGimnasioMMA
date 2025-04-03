using BibliotecaClases_ProyectoGimnasioMMA.Deportes;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes.Funciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Componentes_ProyectoGimnasioMMA.Componentes.GestionPersonas.Editar
{
    public class EditarProfesor : FormularioPersona
    {
        // TODO: Terminar Editar Profesor
        protected const string SIN_USUARIOS = "No hay Usuarios disponibles";

        protected const string SIN_DEPORTE = "No hay Deportes disponibles";
        // RECURSOS

        protected EntryConfirmacion _eDNI;
        protected EntryConfirmacion _eNombre;
        protected EntryConfirmacion _eApellidos;
        protected PickerConfirmacion _selectorEscuela;
        protected EntryConfirmacion _eNivel;
        protected PickerConfirmacion _pDeporte;
        protected PickerConfirmacion _pCorreoProfesor;
        protected PickerConfirmacion _pSelectorUsuario;
        protected Button _botonInsertar;

        protected List<string> _listaNombreEscuelas;

        protected List<Deporte> _listaDeportes;
        protected List<string> _listaNombreDeportes;

        protected List<Usuario> _listaUsuariosDisponibles;
        protected List<string> _listaNombresUsuariosDisponibles;

        public event Action EventoVolverPaginaPrincipal;


        public EditarProfesor(Escuela escuela, Usuario usuario) : base(escuela, usuario)
        {
            CargarDatosConstructor();


        }


        protected override void GenerarUI()
        {


            // Inctanciar Componentes de la interfaz
            _eDNI = GeneracionUI.CrearEntryConfirmacion("DNI", "eDNI", entryUnfocus);
            _eNombre = GeneracionUI.CrearEntryConfirmacion("Nombre", "eNombre", entryUnfocus);
            _eApellidos = GeneracionUI.CrearEntryConfirmacion("Apellidos", "eApellidos", entryUnfocus);
            _selectorEscuela = GeneracionUI.CrearPickerConfirmacion("sEscuela", "Seleccione una Escuela", _listaNombreEscuelas, pickerFocusChanged);

            _pSelectorUsuario = GeneracionUI.CrearPickerConfirmacion("sUsuario", "Seleccione un Usuario", _listaNombresUsuariosDisponibles, pickerFocusChanged);
            _eNivel = GeneracionUI.CrearEntryConfirmacion("Inserte el nivel del Profesor", "eNivel", entryUnfocus);
            _pDeporte = GeneracionUI.CrearPickerConfirmacion("pDeporte", "Seleccione un deporte para el Profesorado", _listaNombreDeportes, selectedIndexChanged);
            _botonInsertar = GeneracionUI.CrearBoton("Insertar Profesor", "bInsertar", controladorBoton);

            // Añadir interfaz al vsl
            MAIN_VSL.Children.Add(_eDNI);
            MAIN_VSL.Children.Add(_eNombre);
            MAIN_VSL.Children.Add(_eApellidos);
            MAIN_VSL.Children.Add(_eNivel);
            MAIN_VSL.Children.Add(_selectorEscuela);
            MAIN_VSL.Children.Add(_pDeporte);
            MAIN_VSL.Children.Add(_pSelectorUsuario);
            MAIN_VSL.Children.Add(_botonInsertar);



        }

        private void CargarDatosConstructor()
        {
            try
            {
                // Cargar lista de deportes de nuestra escuela
                _listaDeportes = _api_bd.DevolverListaDeportes(_escuela.Id);

                // Asignar los nombres ******************************
                if (_listaDeportes.Count > 0)
                {
                    _listaNombreDeportes = new List<string>();

                    foreach (Deporte deporte in _listaDeportes)
                    {
                        _listaNombreDeportes.Add(deporte.Nombre);
                    }
                }
                else
                {
                    _listaNombreDeportes = new List<string>();
                    _listaNombreDeportes.Add(SIN_DEPORTE);
                }

                // Asignar Usuarios Disponibles ******************************
                _listaUsuariosDisponibles = _api_bd.ObtenerUsuariosTipoProfesorSinProfesorAsignado(_escuela.Id);

                if (_listaUsuariosDisponibles.Count > 0)
                {
                    _listaNombresUsuariosDisponibles = new List<string>();

                    foreach (Usuario usuario in _listaUsuariosDisponibles)
                    {
                        _listaNombresUsuariosDisponibles.Add(usuario.Correo);
                    }
                }
                else
                {
                    _listaNombresUsuariosDisponibles = new List<string>();
                    _listaNombresUsuariosDisponibles.Add(SIN_USUARIOS);
                }

                // Obtener Lista de escuelas ******************************
                _listaNombreEscuelas = new List<string>();

                foreach (Escuela escuela in _escuelaList)
                {
                    _listaNombreEscuelas.Add(escuela.Nombre);
                }

                GenerarUI();

            }
            catch (Exception error)
            {
                Application.Current.MainPage.DisplayAlert("ERROR", error.Message, "Ok");
            }
        }

        private void controladorBoton(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void selectedIndexChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
