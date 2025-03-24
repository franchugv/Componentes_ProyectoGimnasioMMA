namespace Componentes_ProyectoGimnasioMMA.Componentes;

public partial class PickerConfirmacion : ContentView
{
    // Recursos
	public PickerConfirmacion(string texto)
	{
		InitializeComponent();

        pickerDato.Title = texto;
	}

    public Picker PickerEditar
    {
        get
        {
            return pickerDato;
        }
        set
        {
            pickerDato = value;
        }
    }
    public bool EstaSeleccionado
    {
        get
        {
            return checkBox.IsChecked;
        }
    }



}