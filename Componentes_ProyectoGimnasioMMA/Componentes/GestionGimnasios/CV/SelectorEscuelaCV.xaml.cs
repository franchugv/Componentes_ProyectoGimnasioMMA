using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes.Funciones;

namespace Componentes_ProyectoGimnasioMMA.Componentes.GestionGimnasios.CV;

public partial class SelectorEscuelaCV : ContentView
{
    // RECURSOS

    API_BD _api_bd;
    List<Escuela> _listaEscuelas;
    Usuario _usuario;
    Escuela _escuelaElegida;

    Button _botonSinEscuela;
    Button _botonTodos;


    bool _soloBotones;

    // Evento que será lanzado al hacer clic en una escuela
    public event Action<Escuela> EscuelaSeleccionadaEvento;
    public event EventHandler BotonSeleccionadoEvento;


    public SelectorEscuelaCV(bool generarBotones)
    {
        InitializeComponent();


        _api_bd = new API_BD();
        _listaEscuelas = _api_bd.ObtenerEscuelas();
        _soloBotones = generarBotones;

        GenerarInterfaz();

        //_accion = accion;

    }
    public SelectorEscuelaCV(Usuario usuario, bool generarBotones)
    {
        InitializeComponent();

        _usuario = usuario;

        _api_bd = new API_BD();
        _listaEscuelas = _api_bd.ObtenerEscuelasDeUsuario(_usuario.Correo);
        _soloBotones = generarBotones;

        GenerarInterfaz();
    }

    public Escuela EscuelaSeleccionada
    {
        get
        {
            return _escuelaElegida;
        }
    }



    private void GenerarInterfaz()
    {

        if (_soloBotones)
        {
            _botonSinEscuela = GeneracionUI.CrearBoton("Sin Escuela", "_botonSinEscuela");
            _botonTodos = GeneracionUI.CrearBoton("Todos", "_botonTodos");

            // Asignar el evento cuando se haga clic
            _botonSinEscuela.Clicked += (s, e) => BotonSeleccionadoEvento?.Invoke(s, e);
            _botonTodos.Clicked += (s, e) => BotonSeleccionadoEvento?.Invoke(s, e);

            VerticalStackLayoutEscuelas.Add(_botonSinEscuela);
            VerticalStackLayoutEscuelas.Add(_botonTodos);


        }
        else
        {
            foreach (Escuela escuela in _listaEscuelas)
            {
                VerticalStackLayoutEscuelas.Children.Add(GeneracionUI.CrearCartaEscuela(escuela, CartaClickeada));
            }
        }

    }


    // EVENTOS

    public virtual void CartaClickeada(object sender, TappedEventArgs e)
    {

        try
        {
            if (sender is Frame carta)
            {
                // Obtener el contenido de la carta
                if (carta.Content is VerticalStackLayout layout)
                {
                    if (layout.Children[0] is Label nombreLabel)
                    {
                        for (int indice = 0; indice < _listaEscuelas.Count; indice++)
                        {
                            if (_listaEscuelas[indice].Id == Convert.ToInt32(nombreLabel.Text))
                            {
                                _escuelaElegida = _listaEscuelas[indice];

                                EscuelaSeleccionadaEvento?.Invoke(_escuelaElegida);

                                // Finalizar el bucle
                                indice = _listaEscuelas.Count;
                            }
                        }
                    }
                }


            }
        }
        catch (Exception error)
        {
            Application.Current.MainPage.DisplayAlert("Error", error.Message, "Aceptar");
        }

    }
}