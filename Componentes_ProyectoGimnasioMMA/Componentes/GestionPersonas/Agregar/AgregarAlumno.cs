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

        protected API_BD _api_bd;

        protected Escuela _escuela;

        protected List<Deporte> _listaDeportes;
        protected List<string> _listaNombreDeportes;
        public AgregarAlumno(Escuela escuela) : base()
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


        public override void GenerarUI()
        {
            base.GenerarUI();

            _pickerCategoriaEdad = GeneracionUI.CrearPicker("ePicker", "Seleccione una Categoría", Alumno.ObtenerCategoriasEdad, SelectedIndexChanged);

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
