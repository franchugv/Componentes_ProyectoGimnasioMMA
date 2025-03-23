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
    public class EditarUsuario : FormularioUsuario
    {
        // Recursos
        Button _botonInsertar;
        API_BD _api_bd;
        ModoFiltroUsuarios _modoFiltro;
        // Componentes
        EntryConfirmacion _eCorreo;
        EntryConfirmacion _eNombre;
        EntryConfirmacion _eContrasenia;
        PickerConfirmacion _selectorTipoUsuario;
        PickerConfirmacion _selectorEscuela;

        // Escuelas
        List<Escuela> _escuelas;
        public Escuela escuela;

        // Constructor
        public EditarUsuario(Usuario usuario, ModoFiltroUsuarios modoFiltro) : base()
        {
            _api_bd = new API_BD();

            _modoFiltro = modoFiltro;

            // Instanciamos la lista de Escuelas
            _escuelas = _api_bd.ObtenerEscuelas();

            GenerarUI();

            //_escuela = escuela;
            _usuario = usuario;
            _modoFiltro = modoFiltro;
        }

        // Metodos
        public override void GenerarUI()
        {


            List<string> listaNombreEscuelas = new List<string>();

            foreach (Escuela escuela in _escuelas)
            {
                listaNombreEscuelas.Add(escuela.Nombre);
            }

            // Inctanciar Componentes de la interfaz
            _eCorreo = GeneracionUI.CrearEntryConfirmacion("Ingrese el nuevo Correo", "_eCorreo", entryUnfocus);
            _eNombre = GeneracionUI.CrearEntryConfirmacion("Ingrese el nuevo Nombre", "_eNombre", entryUnfocus);
            _eContrasenia = GeneracionUI.CrearEntryConfirmacion("Ingrese la nueva Contraseña", "_eContrasenia", entryUnfocus);
            _selectorTipoUsuario = GeneracionUI.CrearPickerConfirmacion("eTipoUsuario", "Seleccione un nuevo Tipo de Usuario", Usuario.ObtenerTiposUsuarios, SelectedIndexChanged);
            _selectorEscuela = GeneracionUI.CrearPickerConfirmacion("eEscuela", "Selecciona la nueva Escuela", listaNombreEscuelas, SelectedIndexChanged);

            // Añadir interfaz al vsl
            MAIN_VSL.Children.Add(
                _eCorreo
            );
            MAIN_VSL.Children.Add(
                _eNombre
            );
            MAIN_VSL.Children.Add(
                _eContrasenia
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


        protected override void entryUnfocus(object sender, FocusEventArgs e)
        {
            Entry entry = (Entry)sender;
            Usuario usuario = null;

            try
            {
                switch (entry.StyleId)
                {
                    case "_eCorreo":
                        _eCorreo.limpiarError();
                        usuario = new Usuario(_eCorreo.Texto, TipoDato.Correo);
                        break;

                    case "eNombre":
                        _eNombre.limpiarError();
                        usuario = new Usuario(_eNombre.Texto, TipoDato.Nombre);
                        break;

                    case "_eContrasenia":
                        _eContrasenia.limpiarError();
                        usuario = new Usuario(_eContrasenia.Texto, TipoDato.Contrasenia);
                        break;

                }

            }
            catch (Exception error)
            {
                switch (entry.StyleId)
                {
                    case "_eCorreo":
                        _eCorreo.mostrarError(error.Message);
                        break;
                    case "eNombre":
                        _eNombre.mostrarError(error.Message);
                        break;
                    case "_eContrasenia":
                        _eContrasenia.mostrarError(error.Message);
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

                if (_eNombre.EstaSeleccionado)
                {
                    // Validar
                    Usuario usuario = new Usuario(_eNombre.Texto, TipoDato.Nombre);
                    nombre = usuario.Nombre;
                }
                if (_eCorreo.EstaSeleccionado)
                {
                    // Validar
                    Usuario usuario = new Usuario(_eCorreo.Texto, TipoDato.Correo);
                    correo = usuario.Correo;
                }
                if (_eContrasenia.EstaSeleccionado)
                {
                    // Validar
                    Usuario usuario = new Usuario(_eContrasenia.Texto, TipoDato.Contrasenia);
                    contrasenia = usuario.Contrasenia;
                }
                if (_selectorTipoUsuario.EstaSeleccionado)
                {
                    if (_selectorTipoUsuario.PickerEditar.SelectedItem == null) throw new Exception("Seleccione un tipo de usuario");

                    tipoUsuario = _selectorTipoUsuario.PickerEditar.SelectedItem.ToString();
                }
                if (_selectorEscuela.EstaSeleccionado)
                {
                    if (_selectorEscuela.PickerEditar.SelectedItem == null) throw new Exception("Seleccione una Escuela");
                    for (int indice = 0; indice < _escuelas.Count; indice++)
                    {
                        if (_selectorEscuela.PickerEditar.SelectedItem.ToString() == _escuelas[indice].Nombre) nuevaEscuela = _escuelas[indice];
                    }
                }

                // Insercciones en la Base de datos
                // Si la escuela no es nula significa que el usuario tiene una escuela, si es así, podremos editar su escuela

                switch (_modoFiltro)
                {

                    case ModoFiltroUsuarios.Todos:
                        if (escuela != null && _selectorEscuela.EstaSeleccionado) _api_bd.
                        EditarRelacionUsuariosEscuelas(_usuario, escuela.Id, nuevaEscuela.Id);
                        break;
                    case ModoFiltroUsuarios.PorEscuela:
                        if (escuela != null && _selectorEscuela.EstaSeleccionado) _api_bd.
                        EditarRelacionUsuariosEscuelas(_usuario, escuela.Id, nuevaEscuela.Id);
                        break;
                    case ModoFiltroUsuarios.SinEscuelas:
                        if (nuevaEscuela != null && _selectorEscuela.EstaSeleccionado) _api_bd.
                        CrearRelacionUsuariosEscuelas(_usuario, nuevaEscuela.Id);
                        break;
                }


                if (correo != null || contrasenia != null || tipoUsuario != null)
                    _api_bd.ActualizarUsuario(_usuario.Correo, correo, nombre, contrasenia, tipoUsuario);

                Application.Current.MainPage.DisplayAlert("Actualización Correcta!!", $"El Usuario {_usuario.Nombre} ha sido actualizado con exito", "Aceptar");


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
