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


        public AgregarUsuario() : base()
        {
            GenerarUI();
            _api_bd = new API_BD();

        }

        public override void GenerarUI()
        {
            base.GenerarUI();

            _botonAgregar = GeneracionUI.CrearBoton("Agregar Usuario", "_botonUpdate", ControladorBoton);

            List<string> nombreEscuelas = new List<string>();


            MAIN_VSL.Add(_botonAgregar);
        }

        private void ControladorBoton(object sender, EventArgs e)
        {
            try
            {
                // Prevenir que se lance una excepcion sin controlar
                if (_selectorEscuela.SelectedItem == null) throw new Exception("Seleccione una Escuela");

                foreach (Escuela escuela in _listaEscuelas)
                {
                    if (escuela.Nombre == _selectorEscuela.SelectedItem.ToString())
                    {
                        _escuela = escuela;
                        break;
                    }
                }

                if (_eTipoUsuario.SelectedItem == null) throw new Exception("Seleccione un tipo de usuario");
                Usuario usuario = new Usuario(_eCorreo.Texto, _eNombre.Texto, _eContrasenia.Texto, _eTipoUsuario.SelectedItem.ToString());



                // Insercciones en la Base de datos
                _api_bd.InsertarUsuario(usuario);
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
