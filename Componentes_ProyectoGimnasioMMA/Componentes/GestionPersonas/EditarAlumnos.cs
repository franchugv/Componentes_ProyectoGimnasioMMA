using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Persona;
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
        protected PickerConfirmacion _selectorEscuela;
        protected PickerConfirmacion _pickerCategoriaEdad;

        protected Button _botonEditar;
        public EditarAlumnos(Escuela escuela, Usuario usuario) : base(escuela, usuario)
        {
            GenerarUI();
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
            _selectorEscuela = GeneracionUI.CrearPickerConfirmacion("sEscuela", "Seleccione una Escuela", listaNombresEscuelas, pickerFocusChanged);
            _pickerCategoriaEdad = GeneracionUI.CrearPickerConfirmacion("pCategoriaEdad", "Seleccione su categoría de Edad", Alumno.ObtenerCategoriasEdad, pickerFocusChanged);

            _botonEditar = GeneracionUI.CrearBoton("Editar Alumno", "bEditar", controladorBotones);
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

        private void controladorBotones(object sender, EventArgs e)
        {
            try
            {
                // Variables locales
                string dni;
                string nombre;
                string apellidos;
                string escuelaSeleccionada;
                string categoriaEdad;

                if (_eDNI.EstaSeleccionado)
                {
                    Alumno alumno = new Alumno(_eDNI.Texto, TipoMiembro.DNI);
                    dni = _eDNI.Texto;
                }
                if (_eNombre.EstaSeleccionado)
                {
                    Alumno alumno = new Alumno(_eNombre.Texto, TipoMiembro.Nombre);
                    nombre = _eNombre.Texto;
                }
                if (_eApellidos.EstaSeleccionado)
                {
                    Alumno alumno = new Alumno(_eApellidos.Texto, TipoMiembro.Apellidos);
                    apellidos = _eApellidos.Texto;
                }
                if (_selectorEscuela.EstaSeleccionado)
                {

                }
                if (_pickerCategoriaEdad.EstaSeleccionado)
                {

                }

                // TODO: Actualizar los pickers de escuelas, tengo que mostrar las escuelas en las cueles, los alumnos no esén apuntados, y otro para mostrar en los que si están apuntados, para poder eliminarlos
                // TODO: Hacer update con la base de datos

            }
            catch(Exception error)
            {
                Application.Current.MainPage.DisplayAlert("Error", error.Message, "Ok");
            }

        }
    }
}
