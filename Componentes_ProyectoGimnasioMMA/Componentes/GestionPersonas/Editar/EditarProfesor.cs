using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Componentes_ProyectoGimnasioMMA.Componentes.GestionPersonas.Editar
{
    public class EditarProfesor : FormularioPersona
    {
        public EditarProfesor(Escuela escuela, Usuario usuario) : base(escuela, usuario)
        {
        }
    }
}
