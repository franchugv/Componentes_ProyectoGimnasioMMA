using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes.Funciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Componentes_ProyectoGimnasioMMA.Componentes.GestionUsuarios
{
    public class AgregarUsuario : FormularioUsuario
    {
        Button _botonAgregar;
        API_BD _api_bd;
        Escuela _escuela;


        public AgregarUsuario(TipoUsuario tipoUsuario) : base(tipoUsuario)
        {
            GenerarUI();
            _api_bd = new API_BD();

        }

        public AgregarUsuario(TipoUsuario tipoUsuario, Usuario usuario) : base(usuario, tipoUsuario)
        {
            GenerarUI();
            _api_bd = new API_BD();
        }

        public override void GenerarUI()
        {
            base.GenerarUI();

            _botonAgregar = GeneracionUI.CrearBoton("Agregar Usuario", "_botonUpdate", ControladorBoton);


            MAIN_VSL.Add(_botonAgregar);
        }

        protected virtual void ControladorBoton(object sender, EventArgs e)
        {
            try
            {

                // Haciendo esto podremos añadir un usuario sin escuela simplemente dejando vacio el picker
                if (_selectorEscuela.SelectedItem != null)
                {
                    foreach (Escuela escuela in _listaEscuelas)
                    {
                        if (escuela.Nombre == _selectorEscuela.SelectedItem.ToString())
                        {
                            _escuela = escuela;
                            break;
                        }
                    }
                }



                if (_eTipoUsuario.SelectedItem == null) throw new Exception("Seleccione un tipo de usuario");
                Usuario usuario = new Usuario(_eCorreo.Texto, _eNombre.Texto, _eContrasenia.Texto, _eTipoUsuario.SelectedItem.ToString());



                // Insercciones en la Base de datos
                _api_bd.InsertarUsuario(usuario);

                // Exactamente lo mismo que al principio
                if (_selectorEscuela.SelectedItem != null)
                    _api_bd.CrearRelacionUsuariosEscuelas(usuario, _escuela.Id);
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
