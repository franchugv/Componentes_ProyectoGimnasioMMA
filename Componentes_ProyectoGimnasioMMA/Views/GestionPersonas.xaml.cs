using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;

namespace Componentes_ProyectoGimnasioMMA.Views;

public partial class GestionPersonas : ContentPage
{
    // Recursos
    TipoUsuario _tipoUsuario;

    Escuela _escuela;
    API_BD api_bd;

    // Controles

    public GestionPersonas()
	{
		InitializeComponent();
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


    }

    // Evento Picker
    protected virtual async void SelectedIndexChanged(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;
        try
        {

            // Instanciar el content view agregar usuario



            switch (picker.StyleId)
            {
                case "PickerAccion":
                    switch (PickerAccion.SelectedIndex)
                    {
                        case 0: // Agregar

                          



                            break;


                        case 1: // Editar
                        case 2: // Eliminar
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

    private void GenerarCV_SelectorEscuelas() { 



    }

    // Controlador para los eventos que generan los botones Todos y Sin Escuela
    private async void ControladorBoton(object sender, EventArgs e)
    {


    }

    // EVENTOS

    // Controlador para el evento generado por las cards de Escuelas
    private async void ControladorCardsSelectorEscuela(Escuela escuela)
    {
        try
        {
            _escuela = escuela;

            switch (PickerAccion.SelectedIndex)
            {
                case 1: // Editar
                case 2: // Eliminar
                  

                    break;
            }
        }
        catch (Exception error)
        {
            await DisplayAlert("ERROR", error.Message, "OK");
        }
    }

    // Controlador para el evento generado por las cards de usuarios
    private async void ControladorCardsSelectorUsuario(Usuario usuario)
    {
        try
        {

            switch (PickerAccion.SelectedIndex)
            {
                case 1: // Editar
                 

                    break;
                case 2: // Eliminar

                 



                    break;
            }
        }
        catch (Exception error)
        {
            await DisplayAlert("ERROR", error.Message, "OK");
        }
    }

}