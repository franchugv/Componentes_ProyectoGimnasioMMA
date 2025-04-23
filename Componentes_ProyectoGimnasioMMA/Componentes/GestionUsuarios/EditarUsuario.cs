using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes.Funciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Componentes_ProyectoGimnasioMMA.Componentes.GestionUsuarios.SelectorUsuario;

namespace Componentes_ProyectoGimnasioMMA.Componentes.GestionUsuarios
{
    /// <summary>
    /// Clase que hereda de FormularioUsuario
    /// </summary>
    public class EditarUsuario : FormularioUsuario
    {
        // Recursos

        Button _botonEditar;
        API_BD _api_bd;
        
        
        
        // Componentes
        // Uso componentes diferentes a la clase heredada ya que son especiales, pudiendo elegir si queremos editarlos o no
        EntryConfirmacion _eCCorreo;
        EntryConfirmacion _eCNombre;
        EntryConfirmacion _eCContrasenia;

        // Pickers
        PickerConfirmacion _selectorTipoUsuario;
        PickerConfirmacion _selectorEscuelaAgregar;
        PickerConfirmacion _selectorEscuelaEliminar;

        // Escuelas


        public List<Escuela> _listaEscuelasEliminar;

        public Escuela escuela;
        protected Usuario _usuarioEditar;



        // EVENTOS Action
        public event Action EventoCerrarSesion;

        public event Action EventoVolverPaginaPrincipal;




        // Constructor
        public EditarUsuario(Usuario usuarioEditar, TipoUsuario tipoUsuario) : base(tipoUsuario)
        {
            _api_bd = new API_BD();


            // Instanciamos la lista de Escuelas

            // En caso de que no sea administrador, obtendremos las escuelas por usuario,
            // si no hacemos esto la lista tendra todas las escuelas, definido en el constructor base


            //_escuelas = _api_bd.ObtenerEscuelas();


            //_escuela = escuela;
            _usuarioEditar = usuarioEditar;

            if(_usuarioEditar.TipoDeUsuario != TipoUsuario.Administrador)
            {
                _listaEscuelasAgregar = _api_bd.ListarEscuelasDisponiblesUsuarioAdmin(usuarioEditar.Correo);
                _listaEscuelasEliminar = _api_bd.ObtenerEscuelasDeUsuario(usuarioEditar.Correo);
            }


            GenerarUI();


        }


        public EditarUsuario(Usuario usuarioApp, Usuario usuarioEditar, TipoUsuario tipoUsuario) : base(tipoUsuario)
        {
            _api_bd = new API_BD();


            _usuarioEditar = usuarioEditar;
            _usuario = usuarioApp;


            if (tipoUsuario != TipoUsuario.Administrador)
            {
                _listaEscuelasAgregar = _api_bd.ListarEscuelasDisponiblesUsuarioNoRegistrado(usuarioApp.Correo, usuarioEditar.Correo);
                _listaEscuelasEliminar = _api_bd.ObtenerEscuelasDeUsuario(usuarioEditar.Correo);
            }

            GenerarUI();



        }


        // Metodos
        public override void GenerarUI()
        {


            List<string> listaNombreEscuelas = new List<string>();

            foreach (Escuela escuela in _listaEscuelasAgregar)
            {
                listaNombreEscuelas.Add(escuela.Nombre);
            }

            List<string> listaNombreEscuelasSinUsuario = new List<string>();

            if(_listaEscuelasEliminar != null)
            {
                foreach (Escuela escuela in _listaEscuelasEliminar)
                {
                    listaNombreEscuelasSinUsuario.Add(escuela.Nombre);
                }
            }

            // Inctanciar Componentes de la interfaz
            _eCCorreo = GeneracionUI.CrearEntryConfirmacion("Ingrese el nuevo Correo", "_eCorreo", entryUnfocus);
            _eCNombre = GeneracionUI.CrearEntryConfirmacion("Ingrese el nuevo Nombre", "_eNombre", entryUnfocus);
            _eCContrasenia = GeneracionUI.CrearEntryConfirmacion("Ingrese la nueva Contraseña", "_eContrasenia", entryUnfocus);
            _eCContrasenia.EntryEditar.IsPassword = true;
            
            _selectorTipoUsuario = GeneracionUI.CrearPickerConfirmacion("eTipoUsuario", "Seleccione un nuevo Tipo de Usuario", _tiposUsuariosPicker, SelectedIndexChanged);
            if (_tiposUsuariosPicker.Count <= 0) _selectorTipoUsuario.IsEnabled = false;

            _selectorEscuelaAgregar = GeneracionUI.CrearPickerConfirmacion("eEscuela", "Selecciona la nueva Escuela", listaNombreEscuelas, SelectedIndexChanged);
            if (listaNombreEscuelas.Count <= 0) _selectorEscuelaAgregar.IsEnabled = false;

            _selectorEscuelaEliminar = GeneracionUI.CrearPickerConfirmacion("eEscuela", "Selecciona una Escuela a Eliminar", listaNombreEscuelasSinUsuario, SelectedIndexChanged);
            if(listaNombreEscuelasSinUsuario.Count <= 0) _selectorEscuelaEliminar.IsEnabled = false;

            // Añadir interfaz al vsl
            MAIN_VSL.Children.Add(
                _eCCorreo
            );
            MAIN_VSL.Children.Add(
                _eCNombre
            );
            MAIN_VSL.Children.Add(
                _eCContrasenia
            );

            if(_tipoUsuario != TipoUsuario.GestorGimnasios || _usuarioEditar.TipoDeUsuario != TipoUsuario.GestorGimnasios)
            {
                if(_usuarioEditar.TipoDeUsuario != TipoUsuario.Administrador)
                {
                    MAIN_VSL.Children.Add(
                    _selectorTipoUsuario
                    );
                    MAIN_VSL.Children.Add(
                        _selectorEscuelaAgregar
                    );
                    MAIN_VSL.Children.Add(
                    _selectorEscuelaEliminar
                    );
                }             
            }

            _botonEditar = GeneracionUI.CrearBoton("Editar Usuario", "_botonInsertar", ControladorBoton);

            MAIN_VSL.Add(_botonEditar);

            AsignarDatos();
        }


        private void AsignarDatos()
        {
            _eCCorreo.Texto = _usuarioEditar.Correo;
            _eCNombre.Texto = _usuarioEditar.Nombre;
            //_eCContrasenia.Texto = _usuarioEditar.Contrasenia;
        }

        // Evento a sobreescribir

        protected override void entryUnfocus(object sender, FocusEventArgs e)
        {
            Entry entry = (Entry)sender;
            Usuario usuario = null;

            try
            {
                switch (entry.StyleId)
                {
                    case "_eCorreo":
                        _eCCorreo.limpiarError();
                        usuario = new Usuario(_eCCorreo.Texto, TipoDato.Correo);
                        break;

                    case "_eNombre":
                        _eCNombre.limpiarError();
                        usuario = new Usuario(_eCNombre.Texto, TipoDato.Nombre);
                        break;

                    case "_eContrasenia":
                        _eCContrasenia.limpiarError();
                        usuario = new Usuario(_eCContrasenia.Texto, TipoDato.Contrasenia);
                        break;

                }

            }
            catch (Exception error)
            {
                switch (entry.StyleId)
                {
                    case "_eCorreo":
                        _eCCorreo.mostrarError(error.Message);
                        break;
                    case "_eNombre":
                        _eCNombre.mostrarError(error.Message);
                        break;
                    case "_eContrasenia":
                        _eCContrasenia.mostrarError(error.Message);
                        break;
                }
            }

        }

        private void ControladorBoton(object sender, EventArgs e)
        {
            try
            {
                // Recursos

                Escuela nuevaEscuela = null;
                Escuela escuelaEliminar = null;

                if ((_selectorTipoUsuario.EstaSeleccionado) && (_selectorTipoUsuario.PickerEditar.SelectedItem == null)) throw new Exception("Seleccione un tipo de usuario");

                // Escuelas a Agregar
                if (_selectorEscuelaAgregar.EstaSeleccionado)
                {
                    if (_selectorEscuelaAgregar.PickerEditar.SelectedItem == null) throw new Exception("Seleccione una Escuela a Agregar");
                    for (int indice = 0; indice < _listaEscuelasAgregar.Count; indice++)
                    {
                        if (_selectorEscuelaAgregar.PickerEditar.SelectedItem.ToString() == _listaEscuelasAgregar[indice].Nombre) nuevaEscuela = _listaEscuelasAgregar[indice];
                    }


                }

                // Escuelas a Eliminar
                if (_selectorEscuelaEliminar.EstaSeleccionado)
                {
                    if (_selectorEscuelaEliminar.PickerEditar.SelectedItem == null) throw new Exception("Seleccione una Escuela a Eliminar");
                    for (int indice = 0; indice < _listaEscuelasEliminar.Count; indice++)
                    {
                        if (_selectorEscuelaEliminar.PickerEditar.SelectedItem.ToString() == _listaEscuelasEliminar[indice].Nombre) escuelaEliminar = _listaEscuelasEliminar[indice];
                    }
                }

                // Insercciones en la Base de datos
                if (nuevaEscuela != null && _selectorEscuelaAgregar.EstaSeleccionado) _api_bd.
                CrearRelacionUsuariosEscuelas(_usuarioEditar, nuevaEscuela.Id);


                if (escuela != null && _selectorEscuelaEliminar.EstaSeleccionado)
                {
                    if (_listaEscuelasEliminar.Count <= 1) throw new Exception("No puedes dejar un usuario sin Escuelas");
                    _api_bd.EliminarRelacionUsuariosEscuelas(_usuarioEditar, escuelaEliminar.Id);
                }



                Usuario usuario = new Usuario(_eCCorreo.Texto, _eCNombre.Texto, _eCContrasenia.Texto, _selectorTipoUsuario.PickerEditar.SelectedItem.ToString());

                {
                    _api_bd.ActualizarUsuario(_usuarioEditar.Correo, usuario.Correo, usuario.Nombre, usuario.Contrasenia, _selectorTipoUsuario.PickerEditar.SelectedItem.ToString());

                    // En caso de actualizar el correo del usuario actual, se reiniciará la aplicación

                    if (_usuario != null)
                    {
                        if (_usuario.Correo == _usuarioEditar.Correo)
                        {
                            EventoCerrarSesion?.Invoke();
                        }
                        else
                        {
                            EventoVolverPaginaPrincipal?.Invoke();
                        }
                    }
                    else
                    {
                        EventoVolverPaginaPrincipal?.Invoke();
                    }
                }
            }
            catch (Exception error)
            {
                Application.Current.MainPage.DisplayAlert("Error", error.Message, "Aceptar");

            }
            finally
            {
                LimpiarDatos();
            }
        }


    }
}
