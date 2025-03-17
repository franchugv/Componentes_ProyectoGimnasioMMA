using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes.GestionGimnasios.Agregar;
using Componentes_ProyectoGimnasioMMA.Componentes.GestionGimnasios.Editar;

namespace Componentes_ProyectoGimnasioMMA.Views;

public partial class GestionEscuelas : ContentPage
{


    AgregarEscuela _agregarEscuela;
    EditarEscuela _editarEscuela;
    SelectorEscuela _selectorEscuela;

    public static readonly string[] OPCIONES = { "Agregar Nuevo Gimnasio", "Agregar Nuevo Gestor De Gimnasios" };

    public static readonly string[] ACCIONES = { "Agregar", "Editar", "Eliminar" };

    public GestionEscuelas()
	{
		InitializeComponent();

        CargarDatosConstructor();

        _selectorEscuela = new SelectorEscuela();
    }

    


    // EVENTOS
    private void CargarDatosConstructor()
    {
        try
        {
            AsignarOpcionesPicker();
        }
        catch (Exception error)
        {
            DisplayAlert("ERROR", error.Message, "OK");
        }
    }




    private void SelectedIndexChanged(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;
        try
        {

            switch (picker.StyleId)
            {
                case "PickerOpcion":
                    switch (PickerOpcion.SelectedIndex)
                    {
                        case 0:


                            break;
                        case 1:


                            break;
                    }
                    break;
                case "PickerAccion":
                    switch (PickerAccion.SelectedIndex)
                    {
                        case 0: // Agregar
                            _agregarEscuela = new AgregarEscuela();

                            VerticalStackLayoutUsuarios.Add(_agregarEscuela);

                            break;
                        case 1: // Editar
                            Navigation.PushAsync();

                            VerticalStackLayoutUsuarios.Add();


                            break;
                        case 2:
                           Navigation.PushAsync(new SelectorEscuela());

                            break;
                    }

                    break;

            }


        }
        catch (Exception error)
        {
            DisplayAlert("ERROR", error.Message, "OK");
        }
    }



    private void AsignarOpcionesPicker()
    {
        if (PickerOpcion.ItemsSource != null) PickerOpcion.ItemsSource.Clear();

        PickerOpcion.ItemsSource = OPCIONES;

        if (PickerAccion.ItemsSource != null) PickerAccion.ItemsSource.Clear();

        PickerAccion.ItemsSource = ACCIONES;

    }



    
}