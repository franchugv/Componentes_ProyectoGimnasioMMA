﻿using BibliotecaClases_ProyectoGimnasioMMA.APIs;
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
    public class AgregarAlumno : FormularioPersona
    {
        // Recursos
        protected Picker _pickerCategoriaEdad;
        protected Button _botonInsertar;
        protected API_BD _api_bd;

        protected Escuela _escuela;
        public event Action EventoVolverPaginaPrincipal;
    
        public AgregarAlumno(Usuario usuario, Escuela escuela) : base(escuela, usuario)
        {
            _api_bd = new API_BD();
            _escuela = escuela;

            cargarConstructor();
        }

        // INICIALIZACIÓN
        private void cargarConstructor()
        {
            try
            {
                GenerarUI();           
            }
            catch (Exception error)
            {
                Application.Current.MainPage.DisplayAlert("Error", error.Message, "Ok");
            }
        }

        protected override void GenerarUI()
        {
            base.GenerarUI();
            _eDNI.EntryEditar.TextChanged += textChangedDni;

            _pickerCategoriaEdad = GeneracionUI.CrearPicker("ePicker", "Seleccione una Categoría", Alumno.ObtenerCategoriasEdad, SelectedIndexChanged);
            _botonInsertar = GeneracionUI.CrearBoton("Insertar Alumno", "eBoton", controladorBoton);
            _botonInsertar.BackgroundColor = Colors.Green;


            MAIN_VSL.Add(_pickerCategoriaEdad);
            VSL_BOTON.Add(_botonInsertar);

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


        protected override void entryUnfocus(object sender, FocusEventArgs e)
        {
            base.entryUnfocus(sender, e);
        }

        public async virtual void controladorBoton(object sender, EventArgs e)
        {

 
            try
            {

                bool confirmar = await GeneracionUI.MostrarConfirmacion(Application.Current.MainPage, "Ventana confirmación", $"¿Desea Agregar un nuevo Alumno?");

                if (confirmar)
                {

                    AgregarAlumnoBD();
                }

            }
            catch (Exception error)
            {
                await Application.Current.MainPage.DisplayAlert("Error", error.Message, "Ok");
            }
            
        }

        private void AgregarAlumnoBD()
        {
            // Recursos
            Escuela nuevaEscuela = null;
            try
            {
                // Validaciones
                if (_selectorEscuela.SelectedItem == null) throw new Exception("Seleccione una escuela para el Alumno");

                if (_pickerCategoriaEdad.SelectedItem == null) throw new Exception("Seleccione una categoria de Edad para el Alumno");

                // Seleccionar escuela
                foreach (Escuela escuela in _escuelaList)
                {
                    // En caso de coincidir los nombres, asignaremos la escuela
                    if (escuela.Nombre == _selectorEscuela.SelectedItem.ToString())
                    {
                        nuevaEscuela = escuela;
                    }
                }

                // Validar Alumno
                Alumno alumno = new Alumno(_eDNI.Texto, _eNombre.Texto, _eApellidos.Texto, Alumno.StringToCategoriaEdad(_pickerCategoriaEdad.SelectedItem.ToString()));
                _api_bd.ValidarRepeticionDNIProfesor(alumno.DNI, _escuela.Id);

                // Insertar Alumno
                _api_bd.InsertarAlumno(alumno);
                // Agregar al alumno a la Escuela
                _api_bd.CrearRelacionEscuelaAlumno(alumno, nuevaEscuela.Id);


                EventoVolverPaginaPrincipal?.Invoke();
            }
            catch(Exception error)
            {
                Application.Current.MainPage.DisplayAlert("Error", error.Message, "Ok");
            }
        }

        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


            }
            catch (Exception error)
            {
                Application.Current.MainPage.DisplayAlert("ERROR", error.Message, "Ok");
            }
        }
    }
}
