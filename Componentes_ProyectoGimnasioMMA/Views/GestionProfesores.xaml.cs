using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Personas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes.Funciones;

namespace Componentes_ProyectoGimnasioMMA.Views;

public partial class GestionProfesores : ContentPage
{
    // Recursos
    protected List<Profesores> _listaProfesores;
    protected Profesores _profesorElegido;


    protected Escuela _escuela;
    protected Usuario _usuario;
    API_BD _api_bd;

    public GestionProfesores(Usuario usuario, Escuela escuela)
	{
		InitializeComponent();

        _escuela = escuela;
        _usuario = usuario;

        _api_bd = new API_BD();

        CargarDatosConstructor();

    }
    // EVENTOS
    private void CargarDatosConstructor()
    {
        try
        {
            Shell.SetNavBarIsVisible(this, false);
            _api_bd = new API_BD();

            // Asignar listas
            _listaProfesores = _api_bd.ObtenerProfesoresPorEscuela(_escuela.Id);

            generarUI();
        }
        catch (Exception error)
        {
            DisplayAlert("ERROR", error.Message, "OK");
        }
    }
    protected virtual void generarUI()
    {


        // Generar Cards de Profesores
        VerticalStackLayoutProfesores.Add(new Label() { Text = "Profesores", HorizontalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold });

        foreach (Profesores profesores in _listaProfesores)
        {


            VerticalStackLayoutProfesores.Children.Add(GeneracionUI.CrearCartaProfesorGestor(profesores, CartaClickeadaProfesores, controladorBotonesProfesor, controladorBotonesProfesor, true));
        }

       
    }

    protected void recargarUI()
    {

        try
        {
            _listaProfesores = _api_bd.ObtenerProfesoresPorEscuela(_escuela.Id);

            VerticalStackLayoutProfesores.Clear();
            generarUI();
        }
        catch (Exception error)
        {
            DisplayAlert("ERROR", error.Message, "OK");
        }
        // Recargar listas listas

    }

    protected virtual void CartaClickeadaProfesores(object sender, TappedEventArgs e)
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
                    foreach (var profesor in _listaProfesores)
                    {
                        if (profesor.DNI == nombreLabel.Text)
                        {
                            _profesorElegido = profesor;
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

    protected virtual void controladorBotonesProfesor(object sender, EventArgs e)
    {

    }

    protected virtual void controladorBotones(object sender, EventArgs e)
    {

    }



}