namespace Componentes_ProyectoGimnasioMMA.Componentes;

public partial class PickerConfirmacion : ContentView
{
    // Recursos
    int _contador;
	public PickerConfirmacion(string texto)
	{
		InitializeComponent();

        pickerDato.Title = texto;
        _contador = 0;
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