using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Componentes_ProyectoGimnasioMMA.Componentes.GestionPersonas.Agregar
{
    public class AgregaProfesor : FormularioPersona
    {
        public AgregaProfesor(Escuela escuela, Usuario usuario) : base(escuela, usuario)
        {
        }
    }
}
