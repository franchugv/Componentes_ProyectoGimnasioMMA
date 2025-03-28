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

        Button _botonInsertar;
        API_BD _api_bd;
        
        
        ModoFiltroUsuarios _modoFiltro;
        
        // Componentes
        // Uso componentes diferentes a la clase heredada ya que son especiales, pudiendo elegir si queremos editarlos o no
        EntryConfirmacion _eCCorreo;
        EntryConfirmacion _eCNombre;
        EntryConfirmacion _eCContrasenia;
        PickerConfirmacion _selectorTipoUsuario;
        PickerConfirmacion _selectorEscuela;

        // Escuelas
        List<Escuela> _escuelas;
        public Escuela escuela;
        protected Usuario _usuarioEditar;


        public event Action EventoCerrarSesion;

        public event Action EventoVolverPaginaPrincipal;


        // Constructor
        public EditarUsuario(Usuario usuarioEditar, ModoFiltroUsuarios modoFiltro, TipoUsuario tipoUsuario) : base(tipoUsuario)
        {
            _api_bd = new API_BD();

            _modoFiltro = modoFiltro;

            // Instanciamos la lista de Escuelas

            // En caso de que no sea administrador, obtendremos las escuelas por usuario,
            // si no hacemos esto la lista tendra todas las escuelas, definido en el constructor base


            //_escuelas = _api_bd.ObtenerEscuelas();

            GenerarUI();

            //_escuela = escuela;
            _usuarioEditar = usuarioEditar;
        }
        public EditarUsuario(Usuario usuarioApp, Usuario usuarioEditar, ModoFiltroUsuarios modoFiltro, TipoUsuario tipoUsuario) : this(usuarioEditar, modoFiltro, tipoUsuario)
        {
            if (tipoUsuario != TipoUsuario.Administrador)
            {
                _listaEscuelas = _api_bd.ObtenerEscuelasDeUsuario(usuarioApp.Correo);
            }
            _usuario = usuarioApp;
        }


        // Metodos
        public override void GenerarUI()
        {


            List<string> listaNombreEscuelas = new List<string>();

            foreach (Escuela escuela in _listaEscuelas)
            {
                listaNombreEscuelas.Add(escuela.Nombre);
            }

            // Inctanciar Componentes de la interfaz
            _eCCorreo = GeneracionUI.CrearEntryConfirmacion("Ingrese el nuevo Correo", "_eCorreo", entryUnfocus);
            _eCNombre = GeneracionUI.CrearEntryConfirmacion("Ingrese el nuevo Nombre", "_eNombre", entryUnfocus);
            _eCContrasenia = GeneracionUI.CrearEntryConfirmacion("Ingrese la nueva Contraseña", "_eContrasenia", entryUnfocus);
            _eCContrasenia.EntryEditar.IsPassword = true;
            _selectorTipoUsuario = GeneracionUI.CrearPickerConfirmacion("eTipoUsuario", "Seleccione un nuevo Tipo de Usuario", _tiposUsuariosPicker, SelectedIndexChanged);
            _selectorEscuela = GeneracionUI.CrearPickerConfirmacion("eEscuela", "Selecciona la nueva Escuela", listaNombreEscuelas, SelectedIndexChanged);

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
            MAIN_VSL.Children.Add(
                _selectorTipoUsuario
            );
            MAIN_VSL.Children.Add(
                _selectorEscuela
            );

            _botonInsertar = GeneracionUI.CrearBoton("Editar Usuario", "_botonInsertar", ControladorBoton);

            MAIN_VSL.Add(_botonInsertar);
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
                string correo = null;
                string nombre = null;
                string contrasenia = null;
                string tipoUsuario = null;
                Escuela nuevaEscuela = null;

                // En caso de estar seleccionado el checkbox, editaremos el valor

                if (_eCNombre.EstaSeleccionado)
                {
                    // Validar
                    Usuario usuario = new Usuario(_eCNombre.Texto, TipoDato.Nombre);
                    nombre = usuario.Nombre;
                }
                if (_eCCorreo.EstaSeleccionado)
                {
                    // Validar
                    Usuario usuario = new Usuario(_eCCorreo.Texto, TipoDato.Correo);
                    correo = usuario.Correo;
                }
                if (_eCContrasenia.EstaSeleccionado)
                {
                    // Validar
                    Usuario usuario = new Usuario(_eCContrasenia.Texto, TipoDato.Contrasenia);
                    contrasenia = usuario.Contrasenia;
                }



                if (_selectorTipoUsuario.EstaSeleccionado)
                {
                    // Evitar que enviemos un dato null
                    if (_selectorTipoUsuario.PickerEditar.SelectedItem == null) throw new Exception("Seleccione un tipo de usuario");

                    // Asignar a la variable el dato elegido
                    tipoUsuario = _selectorTipoUsuario.PickerEditar.SelectedItem.ToString();
                }

                if (_selectorEscuela.EstaSeleccionado)
                {
                    if (_selectorEscuela.PickerEditar.SelectedItem == null) throw new Exception("Seleccione una Escuela");
                    for (int indice = 0; indice < _listaEscuelas.Count; indice++)
                    {
                        if (_selectorEscuela.PickerEditar.SelectedItem.ToString() == _listaEscuelas[indice].Nombre) nuevaEscuela = _listaEscuelas[indice];
                    }
                }

                // Insercciones en la Base de datos
                // En caso de que la casilla este seleccionada y el picker sea null

                switch (_modoFiltro)
                {

                    case ModoFiltroUsuarios.Todos:
                        if (escuela != null && _selectorEscuela.EstaSeleccionado) _api_bd.
                        EditarRelacionUsuariosEscuelas(_usuarioEditar, escuela.Id, nuevaEscuela.Id);
                        break;
                    case ModoFiltroUsuarios.PorEscuela:
                        if (escuela != null && _selectorEscuela.EstaSeleccionado) _api_bd.
                        EditarRelacionUsuariosEscuelas(_usuarioEditar, escuela.Id, nuevaEscuela.Id);
                        break;
                    case ModoFiltroUsuarios.SinEscuelas:
                        if (nuevaEscuela != null && _selectorEscuela.EstaSeleccionado) _api_bd.
                        CrearRelacionUsuariosEscuelas(_usuarioEditar, nuevaEscuela.Id);
                        break;
                }


                // En caso de dejar solo un valor a editar, este se actualizará
                if (correo != null || contrasenia != null || tipoUsuario != null || nombre != null)
                {
                    _api_bd.ActualizarUsuario(_usuarioEditar.Correo, correo, nombre, contrasenia, tipoUsuario);
                    Application.Current.MainPage.DisplayAlert("Actualización Correcta!!", $"El Usuario {_usuarioEditar.Nombre} ha sido actualizado con exito", "Aceptar");

                    // En caso de actualizar el correo del usuario actual, se reiniciará la aplicación

                    if(_usuario != null)
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
