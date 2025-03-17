using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Componentes_ProyectoGimnasioMMA.Componentes.GestionGimnasios
{
    public enum Accion : byte { Editar, Eliminar }

    public class SelectorEscuelaEditar_Eliminar : SelectorEscuelaCV
    {
        Accion _accion;

        public SelectorEscuelaEditar_Eliminar(Accion accion) : base() 
        { 
            _accion = accion;
        }

        protected override void CartaClickeada(object sender, TappedEventArgs e)
        {
            base.CartaClickeada(sender, e);

            switch (_accion)
            {
                case Accion.Editar:
                    break;
                case Accion.Eliminar:
                    break;
            }
        }
    }
}
