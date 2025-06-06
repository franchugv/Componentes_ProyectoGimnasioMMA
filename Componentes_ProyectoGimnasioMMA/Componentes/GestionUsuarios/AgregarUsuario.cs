﻿using BibliotecaClases_ProyectoGimnasioMMA.APIs;
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



    /// <summary>
    /// Esta clase hereda de FormularioUsuario
    /// </summary>
    public class AgregarUsuario : FormularioUsuario
    {
        Button _botonAgregar;
        API_BD _api_bd;
        Escuela _escuela;

        public event Action EventoVolverPaginaPrincipal;

        public AgregarUsuario(TipoUsuario tipoUsuario) : base(tipoUsuario)
        {
            GenerarUI();
            _api_bd = new API_BD();

        }

        /// <summary>
        /// En este constructor, si le pasamos el usuario, solo nos mostrará las escuelas de dicho usuario
        /// </summary>
        /// <param name="tipoUsuario"></param>
        /// <param name="usuario"></param>
        public AgregarUsuario(TipoUsuario tipoUsuario, Usuario usuario) : base(usuario, tipoUsuario)
        {
            GenerarUI();
            _api_bd = new API_BD();
        }

        // INICIALIZACIÓN
        public override void GenerarUI()
        {
            base.GenerarUI();

            _botonAgregar = GeneracionUI.CrearBoton("Agregar Usuario", "_botonUpdate", ControladorBoton);
            _botonAgregar.BackgroundColor = Colors.Green;

            MAIN_VSL.Add(_botonAgregar);
        }

        // EVENTOS
        protected async virtual void ControladorBoton(object sender, EventArgs e)
        {
            try
            {

                bool confirmar = await GeneracionUI.MostrarConfirmacion(Application.Current.MainPage, "Ventana confirmación", $"¿Desea Agregar un nuevo Usuario?");

                if (confirmar)
                {

                    AgregarUsuarioBD();
                }
                

            }
            catch (Exception error)
            {
                await Application.Current.MainPage.DisplayAlert("Error", error.Message, "Aceptar");

            }

        }

        private void AgregarUsuarioBD()
        {
            // Obnener la escuela en caso de haber una seleccionada
            if (_selectorEscuela.SelectedItem != null)
            {
                foreach (Escuela escuela in _listaEscuelasAgregar)
                {
                    if (escuela.Nombre == _selectorEscuela.SelectedItem.ToString())
                    {
                        _escuela = escuela;
                        break;
                    }
                }
            }
            else
            {
                throw new Exception("Debe seleccionar una Escuela");
            }

            // En caso de que no haya un tipo seleccionado dará error
            if (_eTipoUsuario.SelectedItem == null) throw new Exception("Seleccione un tipo de usuario");
            Usuario usuario = new Usuario(_eCorreo.Texto, _eNombre.Texto, _eContrasenia.Texto, _eTipoUsuario.SelectedItem.ToString());



            // Insercciones en la Base de datos
            _api_bd.InsertarUsuario(usuario);

            // Exactamente lo mismo que al principio
            if (_selectorEscuela.SelectedItem != null)
                _api_bd.CrearRelacionUsuariosEscuelas(usuario, _escuela.Id);

            EventoVolverPaginaPrincipal?.Invoke();
        }
    }
}
