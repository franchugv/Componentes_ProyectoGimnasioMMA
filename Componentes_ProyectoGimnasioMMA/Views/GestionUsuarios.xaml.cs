using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
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
    protected Escuela _escuela;
    protected API_BD api_bd;
    protected Usuario _usuario;
    // Controles
    protected SelectorEscuelaCV _selectorEscuela;
    protected SelectorUsuario _selectorUsuario;

    // Componentes
    protected AgregarUsuario _agregarUsuario;
    protected EditarUsuario _editarUsuario;



    // Acciones disponibles
    public static readonly string[] ACCIONES = { "Agregar", "Editar", "Eliminar" };


    public GestionUsuarios()
    {
        InitializeComponent();

        CargarDatosConstructor();
    }

    // Constructores
   public GestionUsuarios(Usuario usuario, Escuela escuela) : this()
    {
        _escuela = escuela;
        _usuario = usuario;
    } 

    // PROPIEDADES
    protected Picker PickerAccionPropiedad
    {
        get
        {
            return PickerAccion;
        }
        set
        {
            PickerAccion = value;
        }
    }

    public StackLayout MAINVSL
    {
        get
        {
            return VerticalStackLayoutUsuarios;
        }
        set
        {
            VerticalStackLayoutUsuarios = value;
        }
    }

    // EVENTOS
    private void CargarDatosConstructor()
    {
        try
        {
            Shell.SetNavBarIsVisible(this, false);
            api_bd = new API_BD();
            AsignarOpcionesPicker();
        }
        catch (Exception error)
        {
            DisplayAlert("ERROR", error.Message, "OK");
        }
    }

    private void AsignarOpcionesPicker()
    {
        // Asignamos las opciones al picker
        if (PickerAccion.ItemsSource != null) PickerAccion.ItemsSource.Clear();

        PickerAccion.ItemsSource = ACCIONES;

    }

    protected virtual void InstanciarSlelectorEscuela()
    {
        // Instanciar el selector de escuela y sus eventos
        _selectorEscuela = new SelectorEscuelaCV(true);
        _selectorEscuela.EscuelaSeleccionadaEvento += ControladorCardsSelectorEscuela;
        _selectorEscuela.BotonSeleccionadoEvento += ControladorBoton;
    }

    // Evento Picker
    protected virtual async void SelectedIndexChanged(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;
        try
        {

            // Instanciar el content view agregar usuario
            // En caso de no ser administrador, le pasaremos el usuario,
            // si hacemos esto, este filtrará las escuelas por usuario en vez de mostrarnos todas las escuelas
            if(_tipoUsuario != TipoUsuario.Administrador)
                _agregarUsuario = new AgregarUsuario(_tipoUsuario, _usuario);
            else
                _agregarUsuario = new AgregarUsuario(_tipoUsuario);

            InstanciarSlelectorEscuela();



            switch (picker.StyleId)
            {
                case "PickerAccion":
                    switch (PickerAccion.SelectedIndex)
                    {
                        case 0: // Agregar

                            VerticalStackLayoutUsuarios.Clear();

                            VerticalStackLayoutUsuarios.Add(_agregarUsuario);

                            // Cuendo elijamos la opción, se limpirará el picker
                            PickerAccion.SelectedItem = null;


                            break;


                        case 1: // Editar
                        case 2: // Eliminar
                            GenerarCV_SelectorEscuelas();
                            break;
                    }

                    break;

            }


        }
        catch (Exception error)
        {
            await DisplayAlert("ERROR", error.Message, "OK");
        }

    }

    private void GenerarCV_SelectorEscuelas()
    {
        VerticalStackLayoutUsuarios.Clear();

        VerticalStackLayoutUsuarios.Add(_selectorEscuela);


    }

    // Controlador para los eventos que generan los botones Todos y Sin Escuela
    private async void ControladorBoton(object sender, EventArgs e)
    {
        try
        {

            Button boton = (Button)sender;

            _modoFiltro = new ModoFiltroUsuarios();

            switch (boton.StyleId)
            {
                case "_botonSinEscuela":
                    _modoFiltro = ModoFiltroUsuarios.SinEscuelas;
                    break;
                case "_botonTodos":
                    _modoFiltro = ModoFiltroUsuarios.Todos;
                    break;
            }

            switch (PickerAccion.SelectedIndex)
            {
                case 1: // Editar
                    VerticalStackLayoutUsuarios.Clear();


                    _selectorUsuario = new SelectorUsuario(_escuela, _modoFiltro);
                    VerticalStackLayoutUsuarios.Add(_selectorUsuario);
                    _selectorUsuario.UsuarioSeleccionadaEvento += ControladorCardsSelectorUsuario;

                    break;
                case 2: // Eliminar
                    VerticalStackLayoutUsuarios.Clear();


                    _selectorUsuario = new SelectorUsuario(_escuela, _modoFiltro);
                    VerticalStackLayoutUsuarios.Add(_selectorUsuario);
                    _selectorUsuario.UsuarioSeleccionadaEvento += ControladorCardsSelectorUsuario;

                    break;
            }
        }
        catch (Exception error)
        {
            await DisplayAlert("ERROR", error.Message, "OK");
        }

    }

    // EVENTOS

    // Controlador para el evento generado por las cards de Escuelas, el cual pasa al selector de usuarios
    protected async void ControladorCardsSelectorEscuela(Escuela escuela)
    {
        try
        {
            _escuela = escuela;

            switch (PickerAccion.SelectedIndex)
            {
                case 1: // Editar
                case 2: // Eliminar
                    VerticalStackLayoutUsuarios.Clear();

                    _modoFiltro = ModoFiltroUsuarios.PorEscuela;
                    _selectorUsuario = new SelectorUsuario(escuela, ModoFiltroUsuarios.PorEscuela);
                    VerticalStackLayoutUsuarios.Add(_selectorUsuario);
                    _selectorUsuario.UsuarioSeleccionadaEvento += ControladorCardsSelectorUsuario;

                    break;
            }
        }
        catch (Exception error)
        {
            await DisplayAlert("ERROR", error.Message, "OK");
        }
    }

    // Controlador para el evento generado por las cards de usuarios
    protected virtual async void ControladorCardsSelectorUsuario(Usuario usuario)
    {
        try
        {

            switch (PickerAccion.SelectedIndex)
            {
                case 1: // Editar
                    VerticalStackLayoutUsuarios.Clear();

                    _editarUsuario = new EditarUsuario(_usuario, usuario, _modoFiltro, _tipoUsuario);
                    if (_escuela != null) _editarUsuario.escuela = _escuela;


                    VerticalStackLayoutUsuarios.Add(_editarUsuario);

                    break;
                case 2: // Eliminar

                    bool confirmar = await GeneracionUI.MostrarConfirmacion(Application.Current.MainPage, "Ventana confirmación", $"¿Desea Eliminar el Usuario {usuario.Nombre}?");
                    
                    if (confirmar)
                    {
                        VerticalStackLayoutUsuarios.Clear();

                        api_bd.EliminarUsuario(usuario.Correo);

                        await DisplayAlert("Escuela eliminada con éxito", $"Escuela: {usuario.Correo}\nUbicación: {usuario.Nombre}", "OK");


                    }



                    break;
            }
        }
        catch (Exception error)
        {
            await DisplayAlert("ERROR", error.Message, "OK");
        }
        finally
        {
            PickerAccion.SelectedItem = null;
        }
    }




}