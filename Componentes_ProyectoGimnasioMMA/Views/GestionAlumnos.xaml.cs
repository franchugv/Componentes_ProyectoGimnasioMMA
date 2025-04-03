using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Personas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes.Funciones;

namespace Componentes_ProyectoGimnasioMMA.Views;

public partial class GestionAlumnos : ContentPage
{
    // Recursos
    protected List<Alumno> _listaAlumnos;
    protected Alumno _alumnoElegido;


    protected Escuela _escuela;
    protected Usuario _usuario;


    protected API_BD _api_bd;
    public GestionAlumnos(Usuario usuario, Escuela escuela)
	{
		InitializeComponent();

        _escuela = escuela;
        _usuario = usuario;

        _api_bd = new API_BD();

        CargarDatosConstructor();

    }

    private void CargarDatosConstructor()
    {
        try
        {
            Shell.SetNavBarIsVisible(this, false);
            _api_bd = new API_BD();

            // Asignar listas
            _listaAlumnos = _api_bd.ListarAlumnosPorEscuela(_escuela.Id);

            generarUI();
        }
        catch (Exception error)
        {
            DisplayAlert("ERROR", error.Message, "OK");
        }
    }

    protected void generarUI()
    {

        // Generar Cards de Alumnos
        VerticalStackLayoutAlumnos.Add(new Label() { Text = "Alumnos", HorizontalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold });

        foreach (Alumno alumnos in _listaAlumnos)
        {


            VerticalStackLayoutAlumnos.Children.Add(GeneracionUI.CrearCartaAlumnoGestor(alumnos, CartaClickeadaAlumnos, controladorBotonesAlumno, controladorBotonesAlumno, true));
        }
    }

    protected void recargarUI()
    {

        try
        {
            _listaAlumnos = _api_bd.ListarAlumnosPorEscuela(_escuela.Id);

            VerticalStackLayoutAlumnos.Clear();
            generarUI();
        }
        catch (Exception error)
        {
            DisplayAlert("ERROR", error.Message, "OK");
        }
        // Recargar listas listas

    }

    protected virtual void controladorBotones(object sender, EventArgs e)
    {

    }

    protected virtual void controladorBotonesAlumno(object sender, EventArgs e)
    {

    }

    protected virtual void CartaClickeadaAlumnos(object sender, TappedEventArgs e)
    {
        try
        {
            // Buscar el Frame desde el sender
            Frame carta = null;
            if (sender is Frame frame)
            {
                carta = frame; // El sender ya es la carta
            }
            else if (sender is VisualElement elemento)
            {
                // Buscar en la jerarquía de elementos hasta encontrar el Frame
                while (elemento != null && !(elemento is Frame))
                {
                    elemento = elemento.Parent as VisualElement;
                }
                carta = elemento as Frame;
            }

            if (carta != null && carta.Content is VerticalStackLayout layout)
            {
                if (layout.Children[0] is Label nombreLabel)
                {
                    foreach (var alumno in _listaAlumnos)
                    {
                        if (alumno.DNI == nombreLabel.Text)
                        {
                            _alumnoElegido = alumno;
                            break; // Terminar el bucle cuando encontramos el usuario
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