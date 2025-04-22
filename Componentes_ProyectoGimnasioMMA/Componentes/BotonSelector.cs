
namespace Componentes_ProyectoGimnasioMMA.Componentes;

public class BotonSelector : ContentView
{
    private Color _colorNormal;
    private Color _colorSeleccionado;

    private string _textoNormal;
    private string _textoAlternativo;

    private Button _boton;
    private bool _seleccionado;

    public event EventHandler<bool> EstadoCambiado;

    public bool Seleccionado
    {
        get => _seleccionado;
        private set
        {
            if (_seleccionado != value)
            {
                _seleccionado = value;
                EstadoCambiado?.Invoke(this, _seleccionado);
            }
        }
    }

    public BotonSelector(string texto, string textoAlternativo, Color colorNormal, Color colorSeleccionado)
    {
        _colorNormal = colorNormal;
        _colorSeleccionado = colorSeleccionado;
        _textoNormal = texto;
        _textoAlternativo = textoAlternativo;
        _boton = new Button
        {
            Text = texto,
            BackgroundColor = _colorNormal,
            TextColor = Colors.White
        };

        _boton.Clicked += BotonClickeado;

        Content = _boton;
    }

    private void BotonClickeado(object sender, EventArgs e)
    {
        try
        {
            Seleccionado = !Seleccionado;

            if (Seleccionado)
            {
                _boton.BackgroundColor = _colorSeleccionado;
                _boton.Text = _textoAlternativo;
            }
            else
            {
                _boton.BackgroundColor = _colorNormal;
                _boton.Text = _textoNormal;
            }
        }
        catch (Exception error)
        {
            Application.Current.MainPage.DisplayAlert("ERROR", error.Message, "Ok");
        }
    }
}