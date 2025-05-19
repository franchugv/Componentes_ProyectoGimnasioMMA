using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes.Funciones;

namespace Componentes_ProyectoGimnasioMMA.Componentes.GestionUsuarios;

public class ListarUsuarios : ContentPage
{
    // Recursos


    // RECURSOS

    VerticalStackLayout _mailVSL;
    API_BD _api_bd;
    List<Usuario> _listaUsuarios;
    Usuario _usuarioElegido;
    Usuario _usuarioApp;     // Usuario que ha inidiado sesión

    // Evento que será lanzado al hacer clic en una escuela
    public event Action<Usuario> UsuarioSeleccionadaEvento;

    /// <summary>
    /// Constructor de Selector de Usuarios, podremos elegir el modo de filtro, si queremos por una escuela en concreto, de todos, o de los usuarios sin escuelas
    /// </summary>
    /// <param name="escuela"></param>
    /// <param name="modoFiltro"></param>
    /// <exception cref="Exception"></exception>
    public ListarUsuarios(Escuela escuela, ModoFiltroUsuarios modoFiltro)
    {
        CargarDatosConstructor(escuela, modoFiltro);
    }

    // INICIALIZACIÓN

    private void CargarDatosConstructor(Escuela escuela, ModoFiltroUsuarios modoFiltro)
    {

        try
        {


            _mailVSL = new VerticalStackLayout();


            ScrollView scrollView = new ScrollView
            {
                Margin = new Thickness(20),
            };
            scrollView.Content = _mailVSL;


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

            Content = scrollView;
        }
        catch (Exception error)
        {
            Application.Current.MainPage.DisplayAlert("ERROR", error.Message, "Ok");
        }

    }

    private void GenerarInterfaz()
    {

        foreach (Usuario usuario in _listaUsuarios)
        {
            _mailVSL.Children.Add(GeneracionUI.CrearCartaUsuario(usuario, CartaClickeada));
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

                                // Invocar a nuestro action, para que, desde otra clase podamos hacer un evento junto al usuario cliqueado
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