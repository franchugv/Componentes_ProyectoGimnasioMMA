using BibliotecaClases_ProyectoGimnasioMMA.Deportes;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Personas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes.Funciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Componentes_ProyectoGimnasioMMA.Componentes.GestionPersonas.Agregar
{
    public class AgregaProfesor : FormularioPersona
    {

        protected const string SIN_USUARIOS = "No hay Usuarios disponibles";

        protected const string SIN_DEPORTE = "No hay Deportes disponibles";
        // RECURSOS
        protected EntryValidacion _eNivel;
        protected Picker _pDeporte;
        protected Picker _pCorreoProfesor;
        protected Picker _pSelectorUsuario;
        protected Button _botonInsertar;

        protected List<string> _listaNombreEscuelas;

        protected List<Deporte> _listaDeportes;
        protected List<string> _listaNombreDeportes;

        protected List<Usuario> _listaUsuariosDisponibles;
        protected List<string> _listaNombresUsuariosDisponibles;

        public event Action EventoVolverPaginaPrincipal;


        // CONSTRUCTORES
        public AgregaProfesor(Escuela escuela, Usuario usuario) : base(escuela, usuario)
        {
            CargarDatosConstructor();            

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

                if(_listaUsuariosDisponibles.Count > 0)
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
            catch(Exception error)
            {
                Application.Current.MainPage.DisplayAlert("ERROR", error.Message, "Ok");
            }
        }

        protected override void GenerarUI()
        {


            // Inctanciar Componentes de la interfaz
            _eDNI = GeneracionUI.CrearEntryError("DNI", "eDNI", entryUnfocus);
            _eNombre = GeneracionUI.CrearEntryError("Nombre", "eNombre", entryUnfocus);
            _eApellidos = GeneracionUI.CrearEntryError("Apellidos", "eApellidos", entryUnfocus);
            _selectorEscuela = GeneracionUI.CrearPicker("sEscuela", "Seleccione una Escuela", _listaNombreEscuelas, pickerFocusChanged);

            _pSelectorUsuario = GeneracionUI.CrearPicker("sUsuario", "Seleccione un Usuario", _listaNombresUsuariosDisponibles, pickerFocusChanged);
            _eNivel = GeneracionUI.CrearEntryError("Inserte el nivel del Profesor", "eNivel", entryUnfocus);
            _pDeporte = GeneracionUI.CrearPicker("pDeporte", "Seleccione un deporte para el Profesorado", _listaNombreDeportes, selectedIndexChanged);
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

        private void controladorBoton(object sender, EventArgs e)
        {
            try
            {
                // El deporte puede ser null
                Deporte deporteElegido = null;



                if(_pDeporte.SelectedItem != null && _pDeporte.SelectedItem.ToString() != SIN_DEPORTE)
                {
                    foreach (Deporte deporte in _listaDeportes)
                    {
                        if (deporte.Nombre == _pDeporte.SelectedItem.ToString()) deporteElegido = deporte;
                    }
                }


                Profesores profesor = new Profesores(_eNombre.Texto, _eApellidos.Texto, _eDNI.Texto, _eNivel.Texto, deporteElegido, _pSelectorUsuario.SelectedItem.ToString());

                // Insertar Profesor

                Usuario usuarioElegido = null;
                if (_selectorEscuela.SelectedItem != null)
                {
                    foreach (Usuario usuario in _listaUsuariosDisponibles)
                    {
                        if (usuario.Correo == _pSelectorUsuario.SelectedItem.ToString()) usuarioElegido = usuario;
                    }
                    _api_bd.AgregarProfesor(profesor, usuarioElegido.Correo);

                }
                // TODO: Hacer que se pueda añadir un profesor sun usuario


                // Asignar a un profesor una escuela

                Escuela escuelaElegida = null;
                if(_selectorEscuela.SelectedItem != null)
                {
                    foreach(Escuela escuela in _escuelaList)
                    {
                        if (escuela.Nombre == _selectorEscuela.SelectedItem.ToString()) escuelaElegida = escuela;
                    }
                    _api_bd.AsignarEscuelaAProfesor(profesor.DNI, escuelaElegida.Id);
                }


                // Asignar a un profesor un deporte
                if (deporteElegido != null)
                    _api_bd.AsignarDeporteAProfesor(profesor.DNI, deporteElegido.Id);

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

        protected override void entryUnfocus(object sender, FocusEventArgs e)
        {
            base.entryUnfocus(sender, e);
        }
    }
}
