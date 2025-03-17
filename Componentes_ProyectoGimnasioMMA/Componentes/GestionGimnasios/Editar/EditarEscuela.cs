using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using Componentes_ProyectoGimnasioMMA.Componentes.GestionGimnasios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Componentes_ProyectoGimnasioMMA.Componentes.GestionGimnasios.Editar
{
    public class EditarEscuela : FormularioGimnasio
    {
        // Recursos


        API_BD _api_bd;

        public EditarEscuela() : base()
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
            base.GenerarUI();

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
