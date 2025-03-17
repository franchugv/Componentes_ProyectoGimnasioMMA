using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes;
using Componentes_ProyectoGimnasioMMA.Componentes.Funciones;
using MySqlConnector;

namespace Componentes_ProyectoGimnasioMMA.Views;

public partial class Login : ContentPage
{
    // MIEMBROS
    protected EntryValidacion _entryCorreo;
    protected EntryValidacion _entryContrasenia;
    protected Button _botonLogin;
    protected API_BD _api_bd;
    protected Usuario _usuario;

    public Login()
    {
        InitializeComponent();

        _api_bd = new API_BD();

        cargarConstructor();
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

    private void controladorBoton(object sender, EventArgs e)
    {
        Button boton = (Button)sender;
        try
        {
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
    protected override async void OnAppearing()
    {
        base.OnAppearing();
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
        // Instanciar
        _entryCorreo = GeneracionUI.CrearEntryError("Correo", "eCorreo", entryUnfocus);
        _entryContrasenia = GeneracionUI.CrearEntryError("Contraseña", "eContrasenia", entryUnfocus);
        _botonLogin = GeneracionUI.CrearBoton("Login", "bLogin", controladorBoton);

        _entryContrasenia.EntryEditar.IsPassword = true;

        // Añadir al StackLayout
        mainVSL.Add(_entryCorreo);
        mainVSL.Add(_entryContrasenia);
        mainVSL.Add(_botonLogin);
    }

    protected virtual void llamadaLogin()
    {
        _usuario = _api_bd.DevolverUsuario(_entryCorreo.Texto, _entryContrasenia.Texto);

    }

    protected void FuncionLogin(TipoUsuario tipoUsuario, ContentPage contentPage)
    {

        if (_usuario.TipoDeUsuario == tipoUsuario)
        {
            Navigation.PushAsync(contentPage);
        }
    }
}