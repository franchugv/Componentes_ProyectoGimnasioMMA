using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using BibliotecaClases_ProyectoGimnasioMMA.Deportes;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Persona;
using BibliotecaClases_ProyectoGimnasioMMA.Personas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes.Funciones;

namespace Componentes_ProyectoGimnasioMMA.Views;

public partial class GestionPersonas : ContentPage
{
    // Recursos
    protected List<Profesores> _listaProfesores;
    protected Profesores _profesorElegido;


    protected List<Alumno> _listaAlumnos;
    protected Alumno _alumnoElegido;

    TipoUsuario _tipoUsuario;

    protected Escuela _escuela;
    protected Usuario _usuario;
    API_BD _api_bd;





    public GestionPersonas(Usuario usuario, Escuela escuela)
	{
		InitializeComponent();
        _escuela = escuela;
        _usuario = usuario;

        CargarDatosConstructor();

    }


    // PROPIEDADES

    public StackLayout MAUINSL
    {
        get
        {
            return VerticalStackLayoutPersonas;
        }
        set
        {
            VerticalStackLayoutPersonas = value;
        }
    }


    // EVENTOS
    private void CargarDatosConstructor()
    {
        try
        {
            Shell.SetNavBarIsVisible(this, false);
            _api_bd = new API_BD();

            // Asignar listas
            _listaAlumnos = _api_bd.ListarAlumnosPorEscuela(_escuela.Id);
            _listaProfesores = _api_bd.ObtenerProfesoresPorEscuela(_escuela.Id);

            generarUI();
        }
        catch (Exception error)
        {
            DisplayAlert("ERROR", error.Message, "OK");
        }
    }




    protected virtual void controladorBotonesProfesor(object sender, EventArgs e)
    {

    }
    protected virtual void controladorBotonesAlumno(object sender, EventArgs e)
    {

    }

    protected virtual void generarUI()
    {



        // Generar Cards de Profesores
        VerticalStackLayoutPersonas.Add(new Label() { Text = "Profesores" , HorizontalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold });


        if(_listaProfesores.Count >= 1)
        {
            foreach (Profesores profesores in _listaProfesores)
            {


                VerticalStackLayoutPersonas.Children.Add(GeneracionUI.CrearCartaProfesorGestor(profesores, CartaClickeadaProfesores, controladorBotonesProfesor, controladorBotonesProfesor, true));

            }
        }
        else
        {
            VerticalStackLayoutPersonas.Children.Add(new Label() { Text = "No hay Profesores Disponibles", TextColor = Colors.Gray });
        }

        // Generar Cards de Alumnos
        VerticalStackLayoutPersonas.Add(new Label() { Text = "Alumnos", HorizontalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold });

        if(_listaAlumnos.Count >= 1)
        {
            foreach (Alumno alumnos in _listaAlumnos)
            {


                VerticalStackLayoutPersonas.Children.Add(GeneracionUI.CrearCartaAlumnoGestor(alumnos, CartaClickeadaAlumnos, controladorBotonesAlumno, controladorBotonesAlumno, true));
            }
        }
        else
        {
            VerticalStackLayoutPersonas.Children.Add(new Label() { Text = "No hay Alumnos Disponibles", TextColor = Colors.Gray });
        }


    }

    protected void recargarUI()
    {

        try
        {
            _listaAlumnos = _api_bd.ListarAlumnosPorEscuela(_escuela.Id);
            _listaProfesores = _api_bd.ObtenerProfesoresPorEscuela(_escuela.Id);

            VerticalStackLayoutPersonas.Clear();
            generarUI();
        }
        catch(Exception error)
        {
            DisplayAlert("ERROR", error.Message, "OK");
        }
        // Recargar listas listas

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

    protected virtual void controladorBotones(object sender, EventArgs e)
    {

    }
}