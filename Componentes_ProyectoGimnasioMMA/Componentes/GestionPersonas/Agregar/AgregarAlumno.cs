using BibliotecaClases_ProyectoGimnasioMMA.APIs;
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

        //protected List<Deporte> _listaDeportes;
        //protected List<string> _listaNombreDeportes;
        public AgregarAlumno(Usuario usuario, Escuela escuela) : base(escuela, usuario)
        {
            _api_bd = new API_BD();
            _escuela = escuela;

            cargarConstructor();
        }

        // EVENTOS
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


        protected override void entryUnfocus(object sender, FocusEventArgs e)
        {
            base.entryUnfocus(sender, e);
        }


        protected override void GenerarUI()
        {
            base.GenerarUI();

            _pickerCategoriaEdad = GeneracionUI.CrearPicker("ePicker", "Seleccione una Categoría", Alumno.ObtenerCategoriasEdad, SelectedIndexChanged);
            _botonInsertar = GeneracionUI.CrearBoton("Insertar Alumno", "eBoton", controladorBoton);


            MAIN_VSL.Add(_pickerCategoriaEdad);
            MAIN_VSL.Add(_botonInsertar);

        }

        public async virtual void controladorBoton(object sender, EventArgs e)
        {
            // Recursos
            Escuela nuevaEscuela = null;

            try
            {
                if (_selectorEscuela.SelectedItem == null) throw new Exception("Seleccione una escuela");
                foreach (Escuela escuela in _escuelaList)
                {
                    // En caso de coincidir los nombres, asignaremos la escuela
                    if(escuela.Nombre == _selectorEscuela.SelectedItem.ToString())
                    {
                        nuevaEscuela = escuela;
                    }
                }

                // Validar Alumno
                Alumno alumno = new Alumno(_eDNI.Texto, _eNombre.Texto, _eApellidos.Texto, Alumno.StringToCategoriaEdad(_pickerCategoriaEdad.SelectedItem.ToString()));

                _api_bd.InsertarAlumno(alumno);
                _api_bd.CrearRelacionEscuelaAlumno(alumno, nuevaEscuela.Id);


                EventoVolverPaginaPrincipal?.Invoke();

            }
            catch (Exception error)
            {
                await Application.Current.MainPage.DisplayAlert("Error", error.Message, "Ok");
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
