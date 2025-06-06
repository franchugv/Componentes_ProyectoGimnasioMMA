﻿using BibliotecaClases_ProyectoGimnasioMMA.Deportes;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Personas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes.Funciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Componentes_ProyectoGimnasioMMA.Componentes.GestionPersonas.Agregar
{
    public class AgregaProfesor : FormularioPersona
    {

        protected const string SIN_USUARIOS = "No hay Usuarios disponibles";

        protected const string SIN_DEPORTE = "No hay Deportes disponibles";
        // RECURSOS
        protected EntryValidacion _eNivel;

        // COMPONENTES
        protected Picker _pDeporte;
        protected Picker _pCorreoProfesor;
        protected Picker _pSelectorUsuario;

        protected Button _botonInsertar;

        // Lista de escuelas a agregar
        protected List<string> _listaNombreEscuelas;

        // Lista de deportes a agregar
        protected List<Deporte> _listaDeportes;
        protected List<string> _listaNombreDeportes;

        // Lista de usuarios a agreagar
        protected List<Usuario> _listaUsuariosDisponibles;
        protected List<string> _listaNombresUsuariosDisponibles;

        public event Action EventoVolverPaginaPrincipal;


        // CONSTRUCTORES
        public AgregaProfesor(Escuela escuela, Usuario usuario) : base(escuela, usuario)
        {
            CargarDatosConstructor();            

        }

        // INICIALIZACIÓN
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
                    _listaNombreDeportes= new List<string>();
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
            // Crear grid principal con dos filas
            var gridPrincipal = new Grid
            {
                RowDefinitions =
        {
            new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }, // Scrollable content
            new RowDefinition { Height = GridLength.Auto } // Fixed button
        }
            };

            // StackLayout con los campos dentro del ScrollView
            var stackCampos = new VerticalStackLayout
            {
                Padding = 10,
                Spacing = 10
            };

            // Instanciar componentes de la interfaz
            _eDNI = GeneracionUI.CrearEntryError("DNI", "eDNI", 10, entryUnfocus);
            _eDNI.EntryEditar.TextChanged += textChangedDni;

            _eNombre = GeneracionUI.CrearEntryError("Nombre", "eNombre", 50, entryUnfocus);
            _eApellidos = GeneracionUI.CrearEntryError("Apellidos", "eApellidos", 100, entryUnfocus);
            _selectorEscuela = GeneracionUI.CrearPicker("sEscuela", "Seleccione una Escuela", _listaNombreEscuelas, pickerFocusChanged);
            _pDeporte = GeneracionUI.CrearPicker("pProfesor", "Seleccione un deporte", _listaNombreDeportes, pickerFocusChanged);

            _pSelectorUsuario = GeneracionUI.CrearPicker("sUsuario", "Seleccione un Usuario", _listaNombresUsuariosDisponibles, pickerFocusChanged);

            _eNivel = GeneracionUI.CrearEntryError("Inserte el nivel del Profesor", "eNivel", 50, entryUnfocus);


            _botonInsertar = GeneracionUI.CrearBoton("Insertar Profesor", "bInsertar", controladorBoton);
            _botonInsertar.BackgroundColor = Colors.Green;

            // Añadir elementos al stack
            stackCampos.Children.Add(_eDNI);
            stackCampos.Children.Add(_eNombre);
            stackCampos.Children.Add(_eApellidos);
            stackCampos.Children.Add(_eNivel);
            stackCampos.Children.Add(_selectorEscuela);
            stackCampos.Children.Add(_pDeporte);
            stackCampos.Children.Add(_pSelectorUsuario);

            // ScrollView que contiene los campos
            var scrollView = new ScrollView
            {
                Content = stackCampos
            };

            // Añadir ScrollView y Botón al grid
            gridPrincipal.Add(scrollView, 0, 0);
            VSL_BOTON.Add(_botonInsertar);

            // Asignar el grid al layout principal
            MAIN_VSL.Children.Clear(); // Asegúrate de limpiar antes de añadir uno nuevo
            MAIN_VSL.Children.Add(gridPrincipal);
        }

        // EVENTOS

        #region EVENTOS TEXT CHANGED
        private void textChangedDni(object sender, TextChangedEventArgs e)
       {
            try
            {

                if (_eDNI.EntryEditar.Text.Length == 9 && !_eDNI.EntryEditar.Text.Contains("-"))
                {
                    // entryDni.Text = entryDni.Text + "-";
                    _eDNI.EntryEditar.Text = _eDNI.EntryEditar.Text.Insert(8, "-");
                }


            }
            catch (Exception error)
            {
                Application.Current.MainPage.DisplayAlert("ERROR", error.Message, "Ok");
            }
        }

        #endregion



        private async void controladorBoton(object sender, EventArgs e)
        {
            try
            {

                bool confirmar = await GeneracionUI.MostrarConfirmacion(Application.Current.MainPage, "Ventana confirmación", $"¿Desea Agregar un nuevo Profesor?");

                if (confirmar)
                {

                    AgregarProfesorBD();
                }
            }
            catch (Exception error)
            {
                await Application.Current.MainPage.DisplayAlert("ERROR", error.Message, "Ok");
            }
            
        }

        private void AgregarProfesorBD()
        {

            try
            {
                if (_selectorEscuela.SelectedItem == null) throw new Exception("Debe seleccionar una Escuela para el Usuario");

                // El deporte puede ser null
                Deporte deporteElegido = null;
                string usuarioElegidoCadena = null;
                if (_pSelectorUsuario.SelectedItem != null)
                {
                    usuarioElegidoCadena = _pSelectorUsuario.SelectedItem.ToString();
                }


                if (_pDeporte.SelectedItem != null && _pDeporte.SelectedItem.ToString() != SIN_DEPORTE)
                {
                    foreach (Deporte deporte in _listaDeportes)
                    {
                        if (deporte.Nombre == _pDeporte.SelectedItem.ToString()) deporteElegido = deporte;
                    }
                }


                Profesores profesor = new Profesores(_eNombre.Texto, _eApellidos.Texto, _eDNI.Texto, _eNivel.Texto, deporteElegido, usuarioElegidoCadena);
                _api_bd.ValidarRepeticionDNIAlumno(profesor.DNI, _escuela.Id);

                // Insertar Profesor

                Usuario usuarioElegido = null;
                if (_selectorEscuela.SelectedItem != null && _listaUsuariosDisponibles.Count >= 1)
                {
                    foreach (Usuario usuario in _listaUsuariosDisponibles)
                    {
                        if (usuario.Correo == usuarioElegidoCadena) usuarioElegido = usuario;
                    }
                    _api_bd.AgregarProfesor(profesor, usuarioElegido.Correo);

                }

                // En caso de que no tengo ningún usuario seleccionado
                if (_pSelectorUsuario.SelectedItem == null || _pSelectorUsuario.SelectedItem.ToString() == "")
                {
                    foreach (Usuario usuario in _listaUsuariosDisponibles)
                    {
                        if (usuario.Correo == usuarioElegidoCadena) usuarioElegido = usuario;
                    }
                    _api_bd.AgregarProfesor(profesor, null);

                }


                // Asignar a un profesor una escuela

                Escuela escuelaElegida = null;
                if (_selectorEscuela.SelectedItem != null)
                {
                    foreach (Escuela escuela in _escuelaList)
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
            base.entryUnfocus(sender, e); // Reutilizo el de la clase padre
        }
    }
}
