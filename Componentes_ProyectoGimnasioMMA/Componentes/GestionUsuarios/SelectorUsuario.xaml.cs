using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes.Funciones;

namespace Componentes_ProyectoGimnasioMMA.Componentes.GestionUsuarios;

public enum ModoFiltroUsuarios : byte { Todos, PorEscuela, SinEscuelas }

public partial class SelectorUsuario : ContentView
{


    // RECURSOS
    API_BD _api_bd;
    List<Usuario> _listaUsuarios;
    Usuario _usuarioElegido;

    // Usuario que ha inidiado sesión

    // Evento que será lanzado al hacer clic en una escuela
    public event Action<Usuario> UsuarioSeleccionadaEvento;


    public SelectorUsuario(Escuela escuela, ModoFiltroUsuarios modoFiltro)
    {
        InitializeComponent();
        // Aquí Instanciamos el Api encargado de la conexión
        _api_bd = new API_BD();
        _listaUsuarios = new List<Usuario>();

        switch (modoFiltro)
        {
            case ModoFiltroUsuarios.Todos:
                _listaUsuarios = _api_bd.ObtenerListaTotalUsuarios();
                break;
            case ModoFiltroUsuarios.PorEscuela:

                if (escuela == null) throw new Exception("no es posible buscar por escuela si esta el nula");
                _listaUsuarios = _api_bd.ObtenerListaUsuarios(escuela.Id);
                break;
            case ModoFiltroUsuarios.SinEscuelas:
                _listaUsuarios = _api_bd.ObtenerListaUsuariosSinEscuela();
                break;
        }



        GenerarInterfaz();

    }

    public Usuario UsuarioElegido
    {
        get
        {
            return _usuarioElegido;
        }
    }




    private void GenerarInterfaz()
    {

        foreach (Usuario usuario in _listaUsuarios)
        {
            VerticalStackLayoutEscuelas.Children.Add(GeneracionUI.CrearCartaUsuario(usuario, CartaClickeada));
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
                        for (int indice = 0; indice < _listaUsuarios.Count; indice++)
                        {
                            if (_listaUsuarios[indice].Correo == nombreLabel.Text)
                            {
                                _usuarioElegido = _listaUsuarios[indice];

                                UsuarioSeleccionadaEvento?.Invoke(_usuarioElegido);

                                // Finalizar el bucle
                                indice = _listaUsuarios.Count;
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
        
