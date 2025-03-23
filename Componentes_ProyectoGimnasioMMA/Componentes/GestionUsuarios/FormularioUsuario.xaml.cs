using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;

namespace Componentes_ProyectoGimnasioMMA.Componentes.GestionUsuarios;

public partial class FormularioUsuario : ContentView
{
    // Recursos
    protected EntryValidacion _eCorreo;
    protected EntryValidacion _eNombre;
    protected EntryValidacion _eContrasenia;
    protected Picker _eTipoUsuario;
    protected Picker _selectorEscuela;

    protected List<Escuela> _listaEscuelas;

    protected Usuario _usuario;
    API_BD api_bd;
    public FormularioUsuario()
    {
        InitializeComponent();

        // CargarEnConstructor();
        api_bd = new API_BD();
        _listaEscuelas = new List<Escuela>();

        _listaEscuelas = api_bd.ObtenerEscuelas();

    }


    // PROPIEDADES

    public VerticalStackLayout MAIN_VSL
    {
        get
        {
            return UI_VSL;
        }
        set
        {
            UI_VSL = value;
        }
    }
    public string Correo
    {
        get
        {
            return _eCorreo.Texto;
        }
    }
    public string Nombre
    {
        get
        {
            return _eNombre.Texto;
        }
    }

    public string Contrasenia
    {
        get
        {
            return _eContrasenia.Texto;
        }
    }

    public TipoUsuario TipoDeUsuario
    {
        get
        {
            return (TipoUsuario)_eTipoUsuario.SelectedIndex;
        }
    }


    // EVENTOS

    private void CargarEnConstructor()
    {
        try
        {
            GenerarUI();
        }
        catch (Exception error)
        {
            App.Current.MainPage.DisplayAlert("Error", error.Message, "Aceptar");
        }
    }



    protected virtual void entryUnfocus(object sender, FocusEventArgs e)
    {
        Entry entry = (Entry)sender;
        Usuario usuario = null;

        try
        {
            switch (entry.StyleId)
            {
                case "eCorreo":
                    _eCorreo.limpiarError();
                    usuario = new Usuario(_eCorreo.Texto, TipoDato.Correo);
                    break;

                case "eNombre":
                    _eNombre.limpiarError();
                    usuario = new Usuario(_eNombre.Texto, TipoDato.Nombre);
                    break;

                case "eContrasenia":
                    _eContrasenia.limpiarError();
                    usuario = new Usuario(_eContrasenia.Texto, TipoDato.Contrasenia);
                    break;

            }

        }
        catch (Exception error)
        {
            switch (entry.StyleId)
            {
                case "eCorreo":
                    _eCorreo.mostrarError(error.Message);
                    break;
                case "eNombre":
                    _eNombre.mostrarError(error.Message);
                    break;
                case "eContrasenia":
                    _eContrasenia.mostrarError(error.Message);
                    break;


            }
        }

    }

    // Evento a heredar en las clases hijas
    protected virtual void controladorBoton(object sender, EventArgs e)
    {

    }


    protected virtual void LimpiarDatos()
    {
        if (_eNombre != null) _eNombre.EntryEditar.Text = "";
        if (_eCorreo != null) _eCorreo.EntryEditar.Text = "";
        if (_eContrasenia != null) _eContrasenia.EntryEditar.Text = "";
    }

    public virtual void GenerarUI()
    {
        List<string> nombreEscuela = new List<string>();

        foreach (Escuela escuela in _listaEscuelas)
        {
            nombreEscuela.Add(escuela.Nombre);
        }

        // Inctanciar Componentes de la interfaz
        _eCorreo = GeneracionUI.CrearEntryError("Ingrese el Correo", "eCorreo", entryUnfocus);
        _eNombre = GeneracionUI.CrearEntryError("Ingrese el Nombre", "eNombre", entryUnfocus);
        _eContrasenia = GeneracionUI.CrearEntryError("Ingrese la contraseña", "eContrasenia", entryUnfocus);
        _eTipoUsuario = GeneracionUI.CrearPicker("eTipoUsuario", "Seleccione un Tipo de Usuario", Usuario.ObtenerTiposUsuarios, SelectedIndexChanged);
        _selectorEscuela = GeneracionUI.CrearPicker("pEscuela", "Seleccione una Escuela", nombreEscuela, SelectedIndexChanged);

        // Añadir interfaz al vsl
        UI_VSL.Children.Add(
            _eCorreo
        );
        UI_VSL.Children.Add(
            _eNombre
        );
        UI_VSL.Children.Add(
            _eContrasenia
        );
        UI_VSL.Children.Add(
            _eTipoUsuario
        );
        UI_VSL.Children.Add(
            _selectorEscuela
        );


    }

    protected virtual void SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception error)
        {

        }
    }
}