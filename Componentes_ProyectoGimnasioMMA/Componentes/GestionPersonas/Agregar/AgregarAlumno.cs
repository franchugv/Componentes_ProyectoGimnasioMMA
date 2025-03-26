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
        protected Picker _pickerDeporte;
        protected Picker _pickerCategoriaEdad;
        protected Button _botonInsertar;
        protected API_BD _api_bd;

        protected Escuela _escuela;

        protected List<Deporte> _listaDeportes;
        protected List<string> _listaNombreDeportes;
        public AgregarAlumno(Usuario usuario, Escuela escuela) : base(usuario)
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
                
                // TODO: Hacer conexión con la base de datos para conseguir la lista de deportes y asignarla a la clase Deportes
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


        public override void GenerarUI()
        {
            base.GenerarUI();

            _pickerCategoriaEdad = GeneracionUI.CrearPicker("ePicker", "Seleccione una Categoría", Alumno.ObtenerCategoriasEdad, SelectedIndexChanged);
            _botonInsertar = GeneracionUI.CrearBoton("Insertar Alumno", "eBoton", controladorBoton);


            _listaDeportes = new List<Deporte>();
            _listaNombreDeportes = new List<string>();

            _listaDeportes = _api_bd.DevolverListaDeportes(_escuela.Id);

            for (int indice = 0; indice < _listaDeportes.Count; indice++)
            {
                _listaNombreDeportes.Add(_listaDeportes[indice].Nombre);

            }

            _pickerDeporte = GeneracionUI.CrearPicker("ePicker", "Seleccione un deporte", _listaNombreDeportes, SelectedIndexChanged);

            MAIN_VSL.Add(_pickerCategoriaEdad);
            MAIN_VSL.Add(_pickerDeporte);

        }

        public virtual void controladorBoton(object sender, EventArgs e)
        {
            // Recursos
            Escuela nuevaEscuela = null;

            try
            {
                if (_selectorEscuela.SelectedItem == null) throw new Exception("Seleccione una escuels");
                foreach (Escuela escuela in _escuelaList)
                {
                    // En caso de coincidir los nombres, asignaremos la escuela
                    if(escuela.Nombre == _selectorEscuela.SelectedItem.ToString())
                    {
                        nuevaEscuela = escuela;
                    }
                }



                Alumno alumno = new Alumno(_eDNI.Texto, _eApellidos, _pickerCategoriaEdad, nuevaEscuela, );
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
