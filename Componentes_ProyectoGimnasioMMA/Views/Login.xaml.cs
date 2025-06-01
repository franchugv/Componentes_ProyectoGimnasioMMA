using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes;
using Componentes_ProyectoGimnasioMMA.Componentes.Funciones;
using MySqlConnector;

namespace Componentes_ProyectoGimnasioMMA.Views;

public partial class Login : ContentPage
{
    // MIEMBROS
    protected string _imagenPortadaClaro;
    protected string _imagenPortadaOscuro;

    protected EntryValidacion _entryCorreo;
    protected EntryValidacion _entryContrasenia;
    protected Button _botonLogin;
    protected API_BD _api_bd;
    protected Usuario _usuario;

    public Login(string imgPortadaClaro, string imgPortadaOscuro ,string titulo)
    {
        InitializeComponent();

        Titulo.Text = titulo;

        _imagenPortadaClaro = imgPortadaClaro;
        _imagenPortadaOscuro = imgPortadaOscuro;

        Shell.SetNavBarIsVisible(this, false);

        _api_bd = new API_BD();

        cargarConstructor();


    }

    // PROPIEDADES
    public Usuario usuarioElegido
    {
        get
        {
            return _usuario;
        }
    }

    public Label TituloLogin
    {
        get
        {
            return Titulo;
        }
        set
        {
            Titulo = value;
        }
    }

    // EVENTOS
    private void cargarConstructor()
    {
        try
        {
            GenerarInterfaz();

        }
        catch (Exception error)
        {
            DisplayAlert("ERROR", error.Message, "OK");
        }
    }

    private async void entryCompletado(object sender, EventArgs e)
    {
        try
        {
            llamadaLogin();
        }
        catch(Exception error)
        {
            await DisplayAlert("Error", error.Message, "Ok");
        }

    }

    private void controladorBoton(object sender, EventArgs e)
    {
        Button boton = (Button)sender;
        try
        {
            _botonLogin.IsEnabled = false;
            switch (boton.StyleId)
            {
                case "bLogin":
                    llamadaLogin();
                    break;
            }

        }
        catch (Exception error)
        {
            DisplayAlert("ERROR", error.Message, "Ok");
        }
        finally
        {
            _botonLogin.IsEnabled = true;
        }
    }

    private void entryUnfocus(object sender, FocusEventArgs e)
    {
        Entry entry = (Entry)sender;

        try
        {
            switch (entry.StyleId)
            {
                case "eCorreo":
                    _entryCorreo.limpiarError();
                    Usuario usuario = new Usuario(_entryCorreo.Texto, Usuario.CONTRASENIA_DEF);
                    break;

                case "eContrasenia":
                    _entryContrasenia.limpiarError();
                    Usuario usuario2 = new Usuario(Usuario.EMAIL_DEF, _entryContrasenia.Texto);
                    break;

            }

        }
        catch (Exception error)
        {
            switch (entry.StyleId)
            {
                case "eCorreo":
                    _entryCorreo.mostrarError(error.Message);
                    break;
                case "eContrasenia":
                    _entryContrasenia.mostrarError(error.Message);
                    break;

            }
        }

    }

    // Evento encargado de que, al cargar el programa, este aparezca en el centro y con un tamaño predeterminado
    protected override void OnAppearing()
    {
        base.OnAppearing();

        AjustarTamanioPantalla();

        if (Application.Current.RequestedTheme == AppTheme.Dark)
        {
            imagenPortada.Source = _imagenPortadaOscuro;
        }
        else
        {
            imagenPortada.Source =_imagenPortadaClaro;
        }

    }

    private async void AjustarTamanioPantalla()
    {
        await Task.Delay(100);

        var displayInfo = DeviceDisplay.Current.MainDisplayInfo;
        var screenWidth = displayInfo.Width / displayInfo.Density;
        var screenHeight = displayInfo.Height / displayInfo.Density;

        var windowWidth = 450;
        var windowHeight = 700;

        var window = Application.Current?.Windows.FirstOrDefault();
        if (window != null)
        {
            window.Width = windowWidth;
            window.Height = windowHeight;

            var x = (screenWidth - windowWidth) / 2;
            var y = (screenHeight - windowHeight) / 2;

            window.X = x;
            window.Y = y;
        }
    }
    // FUNCIONES
    private void GenerarInterfaz()
    {
        // Detectar tema actual
        var esModoOscuro = Application.Current.RequestedTheme == AppTheme.Dark;
        ScrollView scrollView;
        // Colores adaptativos
        Color fondoEntry = esModoOscuro ? Color.FromArgb("#1E1E1E") : Colors.White;
        Color textoEntry = esModoOscuro ? Colors.White : Colors.Black;
        Color fondoFrame = esModoOscuro ? Color.FromArgb("#2C2C2C") : Colors.White;

        // Estilo del Frame contenedor
        Frame frameLogin = new Frame
        {
            CornerRadius = 16,
            Padding = new Thickness(20),
            Margin = new Thickness(20),
            BackgroundColor = fondoFrame,
            HasShadow = true,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            WidthRequest = 320
        };

        // Layout interno del formulario
        VerticalStackLayout layoutInterno = new VerticalStackLayout
        {
            Spacing = 16,
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Center
        };

        // Crear los Entries con márgenes y colores adaptativos
        _entryCorreo = GeneracionUI.CrearEntryError("Correo electrónico", "eCorreo", 100, entryUnfocus);
        _entryCorreo.Margin = new Thickness(15, 2); // 15 izq/der, 8 arriba/abajo
        _entryCorreo.EntryEditar.BackgroundColor = fondoEntry;
        _entryCorreo.EntryEditar.TextColor = textoEntry;

        _entryContrasenia = GeneracionUI.CrearEntryError("Contraseña", "eContrasenia", 64, entryUnfocus);
        _entryContrasenia.Margin = new Thickness(15, 2);
        _entryContrasenia.EntryEditar.IsPassword = true;
        _entryContrasenia.EntryEditar.Completed += entryCompletado;
        _entryContrasenia.EntryEditar.BackgroundColor = fondoEntry;
        _entryContrasenia.EntryEditar.TextColor = textoEntry;

        // Crear el botón
        _botonLogin = GeneracionUI.CrearBoton("Iniciar sesión", "bLogin", controladorBoton);
        _botonLogin.BackgroundColor = Color.FromArgb("#4CAF50");
        _botonLogin.TextColor = Colors.White;
        _botonLogin.CornerRadius = 10;
        _botonLogin.HeightRequest = 40;
        _botonLogin.FontAttributes = FontAttributes.Bold;

        // Agregar controles al layout
        layoutInterno.Add(_entryCorreo);
        layoutInterno.Add(_entryContrasenia);
        layoutInterno.Add(_botonLogin);

        // Agregar layout al frame
        frameLogin.Content = layoutInterno;

        // Agregar frame al layout principal
        mainVSL.Clear();
        mainVSL.HorizontalOptions = LayoutOptions.Center;
        mainVSL.VerticalOptions = LayoutOptions.Center;

        scrollView = new ScrollView
        {
            Content = frameLogin
        };
        mainVSL.Add(scrollView);
    }



    /// <summary>
    /// MÉTODO PARA obtener al usuario
    /// </summary>    
    protected virtual void llamadaLogin()
    {
        _usuario = _api_bd.DevolverUsuario(_entryCorreo.Texto, _entryContrasenia.Texto);
    }

    /// <summary>
    /// Método para validar si el usuario puede entrar o no
    /// </summary>
    /// <param name="tipoUsuario"></param>
    /// <exception cref="Exception"></exception>
    protected void FuncionLogin(TipoUsuario tipoUsuario)
    {

        if (_usuario.TipoDeUsuario != tipoUsuario)
        {
            throw new Exception($"Tipo de usuario incorrecto, se solicita un {tipoUsuario}");
        }

    }



}