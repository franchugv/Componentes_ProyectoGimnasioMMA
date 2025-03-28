using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Personas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes.Funciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Componentes_ProyectoGimnasioMMA.Componentes.GestionPersonas
{
    public class EditarAlumnos : FormularioPersona
    {
        // RECURSOS
        protected EntryConfirmacion _eDNI;
        protected EntryConfirmacion _eNombre;
        protected EntryConfirmacion _eApellidos;
        protected Picker _pickerCategoriaEdad;

        public EditarAlumnos(Escuela escuela, Usuario usuario) : base(escuela, usuario)
        {
        }


        protected override void GenerarUI()
        {
            List<string> listaNombresEscuelas = new List<string>();

            foreach (Escuela escuela in _escuelaList)
            {
                listaNombresEscuelas.Add(escuela.Nombre);
            }

            // Inctanciar Componentes de la interfaz
            _eDNI = GeneracionUI.CrearEntryConfirmacion("DNI", "eDNI", entryUnfocus);
            _eNombre = GeneracionUI.CrearEntryConfirmacion("Nombre", "eNombre", entryUnfocus);
            _eApellidos = GeneracionUI.CrearEntryConfirmacion("Apellidos", "eApellidos", entryUnfocus);
            _selectorEscuela = GeneracionUI.CrearPicker("sEscuela", "Seleccione una Escuela", listaNombresEscuelas, pickerFocusChanged);
            _pickerCategoriaEdad = GeneracionUI.CrearPicker("pCategoriaEdad", "Seleccione su categoría de Edad", Alumno.ObtenerCategoriasEdad, pickerFocusChanged);
            // Añadir interfaz al vsl
            MAIN_VSL.Children.Add(
                _eDNI
            );
            MAIN_VSL.Children.Add(
                _eNombre
            );
            MAIN_VSL.Children.Add(
                _eApellidos
            );
            MAIN_VSL.Children.Add(
                _selectorEscuela
            );
        }
    }
}
