using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Persona;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;

namespace Componentes_ProyectoGimnasioMMA.Views;

public partial class GestionPersonas : ContentPage
{
    // Recursos
    TipoUsuario _tipoUsuario;

    Escuela _escuela;
    API_BD api_bd;



    public static readonly string[] ACCIONES = { "Agregar", "Editar", "Eliminar", "Listar"};


    public GestionPersonas(Escuela escuela)
	{
		InitializeComponent();
        _escuela = escuela;

        CargarDatosConstructor();

    }

    // PROPIEDADES
    public Picker PickerAccionPropiedad
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
    public Picker PickerSelectorTipoPersonaPropiedad
    {
        get
        {
            return PickerSelectorTipoPersona;
        }
        set
        {
            PickerSelectorTipoPersona = value;
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

        if (PickerSelectorTipoPersona.ItemsSource != null) PickerSelectorTipoPersona.ItemsSource.Clear();
        PickerSelectorTipoPersona.ItemsSource = PickerSelectorTipoPersona.ItemsSource = Persona.ListaPersonas;
    }

    // Evento Picker
    protected virtual async void SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected virtual void controladorBoton(object sender, EventArgs e)
    {

    }
}