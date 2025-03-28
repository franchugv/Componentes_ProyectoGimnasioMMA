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
    public static readonly string[] ACCIONES = { "Agregar", "Editar", "Eliminar", "Listar"};


    public GestionUsuarios()
    {
        InitializeComponent();
        //_usuario = usuario;

        CargarDatosConstructor();
    }

    // Constructores
   public GestionUsuarios(Usuario usuario, Escuela escuela) : this()
    {
        _escuela = escuela;
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


    // Evento Picker
    protected virtual void SelectedIndexChanged(object sender, EventArgs e)
    {
       

    }



    // Controlador para los eventos que generan los botones Todos y Sin Escuela
    protected virtual void ControladorBotonSelector(object sender, EventArgs e)
    {
        

    }

    // EVENTOS

    // Controlador para el evento generado por las cards de Escuelas, el cual pasa al selector de usuarios
    protected virtual void ControladorCardsSelectorEscuela(Escuela escuela)
    {

    }

    // Controlador para el evento generado por las cards de usuarios
    protected virtual void ControladorCardsSelectorUsuario(Usuario usuario)
    {

    }

    protected virtual void ControladorBotonXAML(object sender, EventArgs e)
    {
        
    }
}