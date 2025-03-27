using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Personas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes.Funciones;

namespace Componentes_ProyectoGimnasioMMA.Componentes.GestionPersonas;

public partial class SelectorAlumno : ContentView
{

    // RECURSOS
    API_BD _api_bd;
    List<Alumno> _listaAlumnos;
    Alumno _alumnoElegido;

    // Usuario que ha inidiado sesión

    // Evento que será lanzado al hacer clic en una escuela
    public event Action<Alumno> UsuarioSeleccionadaEvento;

    /// <summary>
    /// Constructor de Selector de Alumno, 
    /// </summary>
    /// <param name="escuela"></param>
    /// <param name="modoFiltro"></param>
    /// <exception cref="Exception"></exception>
    public SelectorAlumno(Escuela escuela)
    {
        InitializeComponent();

        CargarDatosConstructor(escuela);

    }

    public Alumno AlumnoElegido
    {
        get
        {
            return _alumnoElegido;
        }
    }


    private void CargarDatosConstructor(Escuela escuela)
    {

        try
        {
            // Aquí Instanciamos el Api encargado de la conexión
            _api_bd = new API_BD();
            _listaAlumnos = new List<Alumno>();

            _listaAlumnos = _api_bd.ListarAlumnosPorEscuela(escuela.Id);

            GenerarInterfaz();
        }
        catch (Exception error)
        {
            Application.Current.MainPage.DisplayAlert("ERROR", error.Message, "Ok");
        }

    }

    private void GenerarInterfaz()
    {

        foreach (Alumno alumno in _listaAlumnos)
        {
            VerticalStackLayoutEscuelas.Children.Add(GeneracionUI.CrearCartaAlumno(alumno, CartaClickeada));
        }

    }




    // EVENTOS

    public virtual void CartaClickeada(object sender, TappedEventArgs e)
    {

        try
        {
            if (sender is Frame carta)
            {
                // Obtener el contenido de la carta
                if (carta.Content is VerticalStackLayout layout)
                {
                    if (layout.Children[0] is Label nombreLabel)
                    {
                        for (int indice = 0; indice < _listaAlumnos.Count; indice++)
                        {
                            if (_listaAlumnos[indice].DNI == nombreLabel.Text)
                            {
                                _alumnoElegido = _listaAlumnos[indice];

                                // Invocar a nuestro action, para que, desde otra clase podamos hacer un evento junto al usuario cliqueado
                                UsuarioSeleccionadaEvento?.Invoke(_alumnoElegido);

                                // Finalizar el bucle
                                indice = _listaAlumnos.Count;
                            }
                        }
                    }
                }


            }
        }
        catch (Exception error)
        {
            Application.Current.MainPage.DisplayAlert("Error", error.Message, "Aceptar");
        }

    }

}