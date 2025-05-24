using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Personas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes.Funciones;
using Componentes_ProyectoGimnasioMMA.Componentes.GestionGimnasios.CV;
using Componentes_ProyectoGimnasioMMA.Componentes.GestionUsuarios;

namespace Componentes_ProyectoGimnasioMMA.Views;

public partial class GestionUsuarios : ContentPage
{
    // Recursos
    protected TipoUsuario _tipoUsuario;
    protected ModoFiltroUsuarios _modoFiltro = new ModoFiltroUsuarios();
    List<Usuario> _listaUsuarios;
    protected Usuario _usuarioElegido;
    protected Escuela _escuela;
    protected API_BD api_bd;
    protected Usuario _usuario;

    // Controles
    protected SelectorEscuelaCV _selectorEscuela;
    protected SelectorUsuario _selectorUsuario;

    // Componentes
    protected AgregarUsuario _agregarUsuario;
    protected EditarUsuario _editarUsuario;

    public GestionUsuarios(TipoUsuario tipoUsuario)
    {
        _tipoUsuario = tipoUsuario;
        InitializeComponent();
        CargarDatosConstructor();
        _listaUsuarios = api_bd.ObtenerListaTotalUsuarios();
        GenerarInterfaz();
    }

    public GestionUsuarios(Usuario usuario, Escuela escuela)
    {
        InitializeComponent();
        _tipoUsuario = usuario.TipoDeUsuario;
        _usuario = usuario;

        CargarDatosConstructor();

        _escuela = escuela;
        _listaUsuarios = api_bd.ObtenerListaUsuarios(_escuela.Id);
        GenerarInterfaz();
    }

    // INICIALIZACIÓN

    private void CargarDatosConstructor()
    {
        try
        {
            Shell.SetNavBarIsVisible(this, false);
            api_bd = new API_BD();
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
            if (_tipoUsuario == TipoUsuario.Administrador)
            {
                _listaUsuarios = api_bd.ObtenerListaTotalUsuarios();
            }
            else
            {
                _listaUsuarios = api_bd.ObtenerListaUsuarios(_escuela.Id);
            }

            VerticalStackLayoutUsuarios.Clear();
            GenerarInterfaz();
        }
        catch (Exception error)
        {
            DisplayAlert("ERROR", error.Message, "OK");
        }
    }

    // EVENTOS

    protected virtual void GenerarInterfazAdministrador(bool todos)
    {
        if (todos)
        {
            _listaUsuarios = api_bd.ObtenerListaTotalUsuarios();
        }
        else
        {
            _listaUsuarios = api_bd.ObtenerListaUsuariosSinEscuela();
        }

        List<Usuario> listaOrdenada = _listaUsuarios.OrderBy(x => x.TipoDeUsuario).ToList();

        foreach (Usuario usuario in listaOrdenada)
        {
            VerticalStackLayoutUsuarios.Children.Add(
                GeneracionUI.CrearCartaUsuarioGestor(
                    usuario,
                    CartaClickeada,
                    ControladorBotones,
                    ControladorBotones,
                    true,
                    true
                )
            );
        }
    }

    protected virtual void GenerarInterfaz()
    {
        VerticalStackLayoutUsuarios.Clear();

        // Agregar buscador
        SearchBar searchBar = new SearchBar
        {
            Placeholder = "Buscar por nombre...",
            Margin = new Thickness(10, 10, 10, 0)
        };
        searchBar.SearchButtonPressed += BusquedaUsuario;
        searchBar.TextChanged += BusquedaUsuarioReset;
        VerticalStackLayoutUsuarios.Children.Add(searchBar);

        bool generarBotonEliminar = true;

        if (_listaUsuarios.Count >= 1)
        {
            List<Usuario> listaOrdenada = _listaUsuarios.OrderBy(x => x.TipoDeUsuario).ToList();

            foreach (Usuario usuario in listaOrdenada)
            {
                if (_tipoUsuario != TipoUsuario.Administrador && usuario.TipoDeUsuario == TipoUsuario.Administrador)
                    continue;

                generarBotonEliminar = !(_tipoUsuario == TipoUsuario.GestorGimnasios &&
                                         usuario.TipoDeUsuario == TipoUsuario.GestorGimnasios);

                VerticalStackLayoutUsuarios.Children.Add(
                    GeneracionUI.CrearCartaUsuarioGestor(
                        usuario,
                        CartaClickeada,
                        ControladorBotones,
                        ControladorBotones,
                        true,
                        generarBotonEliminar
                    )
                );
            }
        }
        else
        {
            VerticalStackLayoutUsuarios.Children.Add(
                new Label() { Text = "No hay Usuarios Disponibles", TextColor = Colors.Gray, HorizontalOptions = LayoutOptions.Center }

            );
        }
    }

    public virtual void CartaClickeada(object sender, TappedEventArgs e)
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
                    foreach (var usuario in _listaUsuarios)
                    {
                        if (usuario.Correo == nombreLabel.Text)
                        {
                            _usuarioElegido = usuario;
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
        finally
        {
            recargarUI();
        }
    }

    // MÉTODOS BUSQUEDA

    private void BusquedaUsuarioReset(object sender, TextChangedEventArgs e)
    {
        try
        {
            SearchBar searchBar = sender as SearchBar;
            string textoBusqueda = searchBar?.Text?.ToLower() ?? "";


            if (string.IsNullOrEmpty(textoBusqueda))
            {
                List<Usuario> listaFiltrada = _listaUsuarios
                .Where(u => u.Nombre.ToLower().Contains(textoBusqueda))
                .OrderBy(u => u.TipoDeUsuario)
                .ToList();

                VerticalStackLayoutUsuarios.Clear();

                // Reagregar el SearchBar
                VerticalStackLayoutUsuarios.Children.Add(searchBar);

                if (listaFiltrada.Count > 0)
                {
                    foreach (Usuario usuario in listaFiltrada)
                    {
                        if (_tipoUsuario != TipoUsuario.Administrador && usuario.TipoDeUsuario == TipoUsuario.Administrador)
                            continue;

                        bool generarBotonEliminar = !(_tipoUsuario == TipoUsuario.GestorGimnasios &&
                                                       usuario.TipoDeUsuario == TipoUsuario.GestorGimnasios);

                        VerticalStackLayoutUsuarios.Children.Add(
                            GeneracionUI.CrearCartaUsuarioGestor(
                                usuario,
                                CartaClickeada,
                                ControladorBotones,
                                ControladorBotones,
                                true,
                                generarBotonEliminar
                            )
                        );
                    }
                }
                else
                {
                    VerticalStackLayoutUsuarios.Children.Add(
                        new Label
                        {
                            Text = "No se encontraron usuarios.",
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

    protected void BusquedaUsuario(object sender, EventArgs e)
    {
        try
        {
            SearchBar searchBar = sender as SearchBar;
            string textoBusqueda = searchBar?.Text?.ToLower() ?? "";

            List<Usuario> listaFiltrada = _listaUsuarios
                .Where(u => u.Nombre.ToLower().Contains(textoBusqueda))
                .OrderBy(u => u.TipoDeUsuario)
                .ToList();

            VerticalStackLayoutUsuarios.Clear();

            // Reagregar el SearchBar
            VerticalStackLayoutUsuarios.Children.Add(searchBar);

            if (listaFiltrada.Count > 0)
            {
                foreach (Usuario usuario in listaFiltrada)
                {
                    if (_tipoUsuario != TipoUsuario.Administrador && usuario.TipoDeUsuario == TipoUsuario.Administrador)
                        continue;

                    bool generarBotonEliminar = !(_tipoUsuario == TipoUsuario.GestorGimnasios &&
                                                   usuario.TipoDeUsuario == TipoUsuario.GestorGimnasios);

                    VerticalStackLayoutUsuarios.Children.Add(
                        GeneracionUI.CrearCartaUsuarioGestor(
                            usuario,
                            CartaClickeada,
                            ControladorBotones,
                            ControladorBotones,
                            true,
                            generarBotonEliminar
                        )
                    );
                }
            }
            else
            {
                VerticalStackLayoutUsuarios.Children.Add(
                    new Label
                    {
                        Text = "No se encontraron usuarios.",
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

    protected virtual void ControladorCardsSelectorEscuela(Escuela escuela)
    {
    }

    protected virtual void ControladorCardsSelectorUsuario(Usuario usuario)
    {
    }

    protected virtual void ControladorBotones(object sender, EventArgs e)
    {
    }

    protected virtual void ControladorBotonSelector(object sender, EventArgs e)
    {
    }

}
