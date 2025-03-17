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

namespace Componentes_ProyectoGimnasioMMA.Componentes.GestionGimnasios.Agregar
{
    public class AgregarEscuela : FormularioGimnasio
    {
        
        // Recursos
        protected EntryConfirmacion _eNombre;
        protected EntryConfirmacion _eUbicacion;

        API_BD _api_bd;

        public AgregarEscuela() : base()
        {
            _api_bd = new API_BD();


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

            }
        }


        protected override void entryUnfocus(object sender, FocusEventArgs e)
        {
            base.entryUnfocus(sender, e);
        }


        public override void GenerarUI()
        {
            // Inctanciar Componentes de la interfaz
            _eNombre = GeneracionUI.CrearEntryConfirmacion("DNI", "eDNI", entryUnfocus);
            _eUbicacion = GeneracionUI.CrearEntryConfirmacion("Nombre", "eNombre", entryUnfocus);


            // Añadir interfaz al vsl
            MAIN_VSL.Children.Add(
                _eNombre
            );
            MAIN_VSL.Children.Add(
                _eUbicacion
            );


        }




        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


            }
            catch (Exception error)
            {

            }
        }
    }

}
