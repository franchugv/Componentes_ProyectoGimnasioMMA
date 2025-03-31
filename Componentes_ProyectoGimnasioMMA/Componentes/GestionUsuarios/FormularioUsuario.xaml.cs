using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes.Funciones;

namespace Componentes_ProyectoGimnasioMMA.Componentes.GestionUsuarios;

public partial class FormularioUsuario : ContentView
{
    // Recursos

    protected TipoUsuario _tipoUsuario;
    protected List<string> _tiposUsuariosPicker;

    // Componentes
    protected EntryValidacion _eCorreo;
    protected EntryValidacion _eNombre;
    protected EntryValidacion _eContrasenia;
    protected Picker _eTipoUsuario;
    protected Picker _selectorEscuela;

    protected List<Escuela> _listaEscuelasAgregar;
    protected Usuario _usuario;

    API_BD _api_bd;


    public FormularioUsuario(TipoUsuario tipoUsuario)
    {
        InitializeComponent();

        CargarEnConstructor(tipoUsuario);

    }

    /// <summary>
    /// En este constructor, al pasarle el usuario, filtraremos las escuelas en función del usuario, en vez de todas las escuelas, 
    /// dado a que cada usuario tiene su escuela asignada
    /// </summary>
    /// <param name="usuario"></param>
    /// <param name="tipoUsuario"></param>
    public FormularioUsuario(Usuario usuario, TipoUsuario tipoUsuario) : this(tipoUsuario)
    {
       _listaEscuelasAgregar = _api_bd.ObtenerEscuelasDeUsuario(usuario.Correo);
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




    // EVENTOS

    private void CargarEnConstructor(TipoUsuario tipoUsuario)
    {
        try
        {
            _tipoUsuario = tipoUsuario;
            // Aquí estamos asignando los datos del picker en función del tipo de usuario
            switch (_tipoUsuario)
            {
                case TipoUsuario.Administrador:
                    _tiposUsuariosPicker = Usuario.ObtenerTiposUsuarios;
                    break;
                case TipoUsuario.GestorGimnasios:
                    _tiposUsuariosPicker = Usuario.ObtenerTiposUsuariosGestorGym;
                    break;
            }

            // Instanciar api de base de datos
            _api_bd = new API_BD();

            // Asignar la lista de escuelas torales
            _listaEscuelasAgregar = new List<Escuela>();
            _listaEscuelasAgregar = _api_bd.ObtenerEscuelas();
        }
        catch (Exception error)
        {
            Application.Current.MainPage.DisplayAlert("Error", error.Message, "Aceptar");
        }
    }



    protected virtual void entryUnfocus(object sender, FocusEventArgs e)
    {
        // Recursos
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

        foreach (Escuela escuela in _listaEscuelasAgregar)
        {
            nombreEscuela.Add(escuela.Nombre);
        }

        // Inctanciar Componentes de la interfaz
        _eCorreo = GeneracionUI.CrearEntryError("Ingrese el Correo", "eCorreo", entryUnfocus);
        _eNombre = GeneracionUI.CrearEntryError("Ingrese el Nombre", "eNombre", entryUnfocus);
        _eContrasenia = GeneracionUI.CrearEntryError("Ingrese la contraseña", "eContrasenia", entryUnfocus);
        _eTipoUsuario = GeneracionUI.CrearPicker("eTipoUsuario", "Seleccione un Tipo de Usuario", _tiposUsuariosPicker, SelectedIndexChanged);
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