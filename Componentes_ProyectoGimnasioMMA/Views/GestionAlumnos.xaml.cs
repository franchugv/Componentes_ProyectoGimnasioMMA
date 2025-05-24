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

        CargarDatosConstructor();
        _listaAlumnos = _api_bd.ListarAlumnosPorEscuela(_escuela.Id);
        GenerarInterfaz();
    }

    // INICIALIZACIÓN
    private void CargarDatosConstructor()
    {
        try
        {
            Shell.SetNavBarIsVisible(this, false);
            _api_bd = new API_BD();
        }
        catch (Exception error)
        {
            DisplayAlert("ERROR", error.Message, "OK");
        }
    }

    protected void recargarUI()
    {
        try
        {
            _listaAlumnos = _api_bd.ListarAlumnosPorEscuela(_escuela.Id);

            VerticalStackLayoutAlumnos.Clear();
            GenerarInterfaz();
        }
        catch (Exception error)
        {
            DisplayAlert("ERROR", error.Message, "OK");
        }
    }

    // EVENTOS

    protected virtual void GenerarInterfaz()
    {
        VerticalStackLayoutAlumnos.Clear();

        // Agregar buscador
        SearchBar searchBar = new SearchBar
        {
            Placeholder = "Buscar por nombre...",
            Margin = new Thickness(10, 10, 10, 0)
        };




        searchBar.SearchButtonPressed += BusquedaAlumno;
        searchBar.TextChanged += BusquedaAlumnoReset;
        
        VerticalStackLayoutAlumnos.Children.Add(searchBar);

        VerticalStackLayoutAlumnos.Children.Add(
            new Label
            {
                Text = "Alumnos",
                HorizontalOptions = LayoutOptions.Center,
                FontAttributes = FontAttributes.Bold
            }
        );

        if (_listaAlumnos.Count > 0)
        {
            foreach (Alumno alumno in _listaAlumnos)
            {
                VerticalStackLayoutAlumnos.Children.Add(
                    GeneracionUI.CrearCartaAlumnoGestor(
                        alumno,
                        CartaClickeadaAlumnos,
                        controladorBotonesAlumno,
                        controladorBotonesAlumno,
                        true
                    )
                );
            }
        }
        else
        {
            VerticalStackLayoutAlumnos.Children.Add(
                new Label() { Text = "No hay Alumnos Disponibles", TextColor = Colors.Gray, HorizontalOptions = LayoutOptions.Center }
            );
        }
    }

    protected virtual void CartaClickeadaAlumnos(object sender, TappedEventArgs e)
    {
        try
        {
            Frame carta = null;
            if (sender is Frame frame)
            {
                carta = frame;
            }
            else if (sender is VisualElement elemento)
            {
                while (elemento != null && !(elemento is Frame))
                {
                    elemento = elemento.Parent as VisualElement;
                }
                carta = elemento as Frame;
            }

            if (carta != null && carta.Content is VerticalStackLayout layout)
            {
                if (layout.Children[0] is Label dniLabel)
                {
                    foreach (var alumno in _listaAlumnos)
                    {
                        if (alumno.DNI == dniLabel.Text)
                        {
                            _alumnoElegido = alumno;
                            break;
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

    // MÉTODOS BUSQUEDA

    private void BusquedaAlumnoReset(object sender, TextChangedEventArgs e)
    {
        try
        {
            SearchBar searchBar = sender as SearchBar;
            string textoBusqueda = searchBar?.Text?.ToLower() ?? "";

            if (string.IsNullOrEmpty(textoBusqueda))
            {
                List<Alumno> listaFiltrada = _listaAlumnos
                .Where(a => a.Nombre.ToLower().Contains(textoBusqueda))
                .ToList();

                VerticalStackLayoutAlumnos.Clear();

                // Reagregar el SearchBar
                VerticalStackLayoutAlumnos.Children.Add(searchBar);

                VerticalStackLayoutAlumnos.Children.Add(
                    new Label
                    {
                        Text = "Alumnos",
                        HorizontalOptions = LayoutOptions.Center,
                        FontAttributes = FontAttributes.Bold
                    }
                );

                if (listaFiltrada.Count > 0)
                {
                    foreach (Alumno alumno in listaFiltrada)
                    {
                        VerticalStackLayoutAlumnos.Children.Add(
                            GeneracionUI.CrearCartaAlumnoGestor(
                                alumno,
                                CartaClickeadaAlumnos,
                                controladorBotonesAlumno,
                                controladorBotonesAlumno,
                                true
                            )
                        );
                    }
                }
                else
                {
                    VerticalStackLayoutAlumnos.Children.Add(
                        new Label
                        {
                            Text = "No se encontraron alumnos.",
                            TextColor = Colors.Gray,
                            HorizontalOptions = LayoutOptions.Center
                        }
                    );
                }
            }


        }
        catch (Exception ex)
        {
            DisplayAlert("Error en búsqueda", ex.Message, "OK");
        }
    }

    protected void BusquedaAlumno(object sender, EventArgs e)
    {
        try
        {
            SearchBar searchBar = sender as SearchBar;
            string textoBusqueda = searchBar?.Text?.ToLower() ?? "";

            List<Alumno> listaFiltrada = _listaAlumnos
                .Where(a => a.Nombre.ToLower().Contains(textoBusqueda))
                .ToList();

            VerticalStackLayoutAlumnos.Clear();

            // Reagregar el SearchBar
            VerticalStackLayoutAlumnos.Children.Add(searchBar);

            VerticalStackLayoutAlumnos.Children.Add(
                new Label
                {
                    Text = "Alumnos",
                    HorizontalOptions = LayoutOptions.Center,
                    FontAttributes = FontAttributes.Bold
                }
            );

            if (listaFiltrada.Count > 0)
            {
                foreach (Alumno alumno in listaFiltrada)
                {
                    VerticalStackLayoutAlumnos.Children.Add(
                        GeneracionUI.CrearCartaAlumnoGestor(
                            alumno,
                            CartaClickeadaAlumnos,
                            controladorBotonesAlumno,
                            controladorBotonesAlumno,
                            true
                        )
                    );
                }
            }
            else
            {
                VerticalStackLayoutAlumnos.Children.Add(
                    new Label
                    {
                        Text = "No se encontraron alumnos.",
                        TextColor = Colors.Gray,
                        HorizontalOptions = LayoutOptions.Center
                    }
                );
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Error en búsqueda", ex.Message, "OK");
        }
    }

    // EVENTOS SIN USAR

    protected virtual void controladorBotones(object sender, EventArgs e)
    {
    }

    protected virtual void controladorBotonesAlumno(object sender, EventArgs e)
    {
    }
}
