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
    protected API_BD _api_bd;

    public GestionProfesores(Usuario usuario, Escuela escuela)
    {
        InitializeComponent();

        _escuela = escuela;
        _usuario = usuario;

        _api_bd = new API_BD();

        CargarDatosConstructor();
    }

    // INICIALIZACIÓN
    private void CargarDatosConstructor()
    {
        try
        {
            Shell.SetNavBarIsVisible(this, false);
            _api_bd = new API_BD();

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
        VerticalStackLayoutProfesores.Clear();

        // Agregar buscador
        SearchBar searchBar = new SearchBar
        {
            Placeholder = "Buscar por nombre...",
            Margin = new Thickness(10, 10, 10, 0)
        };
        searchBar.SearchButtonPressed += BusquedaProfesor;
        searchBar.TextChanged += BusquedaProfesorReset;
        VerticalStackLayoutProfesores.Children.Add(searchBar);

        VerticalStackLayoutProfesores.Children.Add(
            new Label()
            {
                Text = "Profesores",
                HorizontalOptions = LayoutOptions.Center,
                FontAttributes = FontAttributes.Bold
            });

        if (_listaProfesores.Count >= 1)
        {
            foreach (Profesores profesores in _listaProfesores)
            {
                VerticalStackLayoutProfesores.Children.Add(
                    GeneracionUI.CrearCartaProfesorGestor(
                        profesores,
                        CartaClickeadaProfesores,
                        controladorBotonesProfesor,
                        controladorBotonesProfesor,
                        true
                    )
                );
            }
        }
        else
        {
            VerticalStackLayoutProfesores.Children.Add(
                new Label()
                {
                    Text = "No hay Profesores Disponibles",
                    TextColor = Colors.Gray
                }
            );
        }
    }

    // EVENTOS

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
    }

    protected virtual void CartaClickeadaProfesores(object sender, TappedEventArgs e)
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
                if (layout.Children[0] is Label nombreLabel)
                {
                    foreach (var profesor in _listaProfesores)
                    {
                        if (profesor.DNI == nombreLabel.Text)
                        {
                            _profesorElegido = profesor;
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

    // EVENTOS DE BUSQUEDA

    private void BusquedaProfesorReset(object sender, TextChangedEventArgs e)
    {
        try
        {
            SearchBar searchBar = sender as SearchBar;
            string textoBusqueda = searchBar?.Text?.ToLower() ?? "";
            if (string.IsNullOrEmpty(textoBusqueda))
            {

                List<Profesores> listaFiltrada = _listaProfesores
                .Where(p => p.Nombre.ToLower().Contains(textoBusqueda))
                .ToList();

                VerticalStackLayoutProfesores.Clear();

                // Reagregar el SearchBar
                VerticalStackLayoutProfesores.Children.Add(searchBar);

                VerticalStackLayoutProfesores.Children.Add(
                    new Label()
                    {
                        Text = "Profesores",
                        HorizontalOptions = LayoutOptions.Center,
                        FontAttributes = FontAttributes.Bold
                    });

                if (listaFiltrada.Count > 0)
                {
                    foreach (Profesores profesor in listaFiltrada)
                    {
                        VerticalStackLayoutProfesores.Children.Add(
                            GeneracionUI.CrearCartaProfesorGestor(
                                profesor,
                                CartaClickeadaProfesores,
                                controladorBotonesProfesor,
                                controladorBotonesProfesor,
                                true
                            )
                        );
                    }
                }
                else
                {
                    VerticalStackLayoutProfesores.Children.Add(
                        new Label()
                        {
                            Text = "No se encontraron profesores.",
                            TextColor = Colors.Gray,
                            HorizontalOptions = LayoutOptions.Center
                        });
                }
            }

        }
        catch (Exception error)
        {
            DisplayAlert("Error en búsqueda", error.Message, "OK");
        }
    }

    protected void BusquedaProfesor(object sender, EventArgs e)
    {
        try
        {
            SearchBar searchBar = sender as SearchBar;
            string textoBusqueda = searchBar?.Text?.ToLower() ?? "";

            List<Profesores> listaFiltrada = _listaProfesores
                .Where(p => p.Nombre.ToLower().Contains(textoBusqueda))
                .ToList();

            VerticalStackLayoutProfesores.Clear();

            // Reagregar el SearchBar
            VerticalStackLayoutProfesores.Children.Add(searchBar);

            VerticalStackLayoutProfesores.Children.Add(
                new Label()
                {
                    Text = "Profesores",
                    HorizontalOptions = LayoutOptions.Center,
                    FontAttributes = FontAttributes.Bold
                });

            if (listaFiltrada.Count > 0)
            {
                foreach (Profesores profesor in listaFiltrada)
                {
                    VerticalStackLayoutProfesores.Children.Add(
                        GeneracionUI.CrearCartaProfesorGestor(
                            profesor,
                            CartaClickeadaProfesores,
                            controladorBotonesProfesor,
                            controladorBotonesProfesor,
                            true
                        )
                    );
                }
            }
            else
            {
                VerticalStackLayoutProfesores.Children.Add(
                    new Label()
                    {
                        Text = "No se encontraron profesores.",
                        TextColor = Colors.Gray,
                        HorizontalOptions = LayoutOptions.Center
                    });
            }
        }
        catch (Exception error)
        {
            DisplayAlert("Error en búsqueda", error.Message, "OK");
        }
    }

    // EVENTOS SIN USAR

    protected virtual void controladorBotonesProfesor(object sender, EventArgs e)
    { }

    protected virtual void controladorBotones(object sender, EventArgs e)
    { }
}
