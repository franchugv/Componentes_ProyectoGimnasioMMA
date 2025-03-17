using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes.Funciones;

namespace Componentes_ProyectoGimnasioMMA.Componentes.GestionGimnasios;

public partial class SelectorEscuelaCV : ContentView
{
    // RECURSOS

    API_BD _api_bd;
    List<Escuela> _listaEscuelas;
    Usuario _usuario;
    Escuela _escuelaElegida;
    public SelectorEscuelaCV(Usuario usuario)
    {
        InitializeComponent();

        _usuario = usuario;

        _api_bd = new API_BD();
        _listaEscuelas = _api_bd.ObtenerEscuelasDeUsuario(_usuario.Correo);

        GenerarInterfaz();

    }

    public SelectorEscuelaCV()
    {
        InitializeComponent();


        _api_bd = new API_BD();
        _listaEscuelas = _api_bd.ObtenerEscuelas();

        GenerarInterfaz();

        //_accion = accion;

    }




    private void GenerarInterfaz()
    {
        foreach (Escuela escuela in _listaEscuelas)
        {
            VerticalStackLayoutEscuelas.Children.Add(GeneracionUI.CrearCarta(escuela, CartaClickeada));
        }
    }


    // EVENTOS

    protected virtual void CartaClickeada(object sender, TappedEventArgs e)
    {
        Escuela escuela = null;

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
                            }
                        }
                    }
                }





            }
        }
        catch (Exception error)
        {
        }


    }

}