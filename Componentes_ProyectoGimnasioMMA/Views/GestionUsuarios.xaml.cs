using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Personas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes.Funciones;
using Componentes_ProyectoGimnasioMMA.Componentes.GestionGimnasios.CV;
using Componentes_ProyectoGimnasioMMA.Componentes.GestionUsuarios;

namespace Componentes_ProyectoGimnasioMMA.Views;

public partial class GestionUsuarios : ContentPage
{
    // Recursos

    List<Usuario> _listaUsuarios;
    protected Usuario _usuarioElegido;


    protected TipoUsuario _tipoUsuario;

    protected ModoFiltroUsuarios _modoFiltro = new ModoFiltroUsuarios();
    protected Escuela _escuela;
    protected API_BD api_bd;
    protected Usuario _usuario;
    // Controles
    protected SelectorEscuelaCV _selectorEscuela;
    protected SelectorUsuario _selectorUsuario;

    // Componentes
    protected AgregarUsuario _agregarUsuario;
    protected EditarUsuario _editarUsuario;






    public GestionUsuarios(TipoUsuario tipoUsuario)
    {
        _tipoUsuario = tipoUsuario;

        InitializeComponent();
        //_usuario = usuario;

        CargarDatosConstructor();
        _listaUsuarios = api_bd.ObtenerListaTotalUsuarios();

        GenerarInterfaz();
    }

    // Constructores
   public GestionUsuarios(Usuario usuario, Escuela escuela) 
    {
        _tipoUsuario = usuario.TipoDeUsuario;

        InitializeComponent();
        _usuario = usuario;

        CargarDatosConstructor();

        _escuela = escuela;
        _listaUsuarios = api_bd.ObtenerListaUsuarios(_escuela.Id);

        GenerarInterfaz();

    }

    // PROPIEDADES



    // EVENTOS
    private void CargarDatosConstructor()
    {
        try
        {
            Shell.SetNavBarIsVisible(this, false);
            api_bd = new API_BD();


            //AsignarOpcionesPicker();
        }
        catch (Exception error)
        {
            DisplayAlert("ERROR", error.Message, "OK");
        }
    }



    // Controlador para los eventos que generan los botones Todos y Sin Escuela
    protected virtual void ControladorBotonSelector(object sender, EventArgs e)
    {
        

    }

    protected void recargarUI()
    {
        try
        {
            if(_tipoUsuario == TipoUsuario.Administrador)
            {
                _listaUsuarios = api_bd.ObtenerListaTotalUsuarios();
            }
            else
            {
                _listaUsuarios = api_bd.ObtenerListaUsuarios(_escuela.Id);
            }

            VerticalStackLayoutUsuarios.Clear();
            GenerarInterfaz();
        }
        catch (Exception error)
        {
            DisplayAlert("ERROR", error.Message, "OK");
        }
    }


    protected virtual void GenerarInterfazAdministrador(bool todos)
    {
        if (todos)
        {
            _listaUsuarios = api_bd.ObtenerListaTotalUsuarios();
        }
        else
        {
            _listaUsuarios = api_bd.ObtenerListaUsuariosSinEscuela();
        }

        foreach (Usuario usuario in _listaUsuarios)
        {
          

            VerticalStackLayoutUsuarios.Children.Add(GeneracionUI.CrearCartaUsuarioGestor(usuario, CartaClickeada, ControladorBotones, ControladorBotones, true, true));
        }

    }

    protected virtual void GenerarInterfaz()
    {
        bool generarBotonEliminar = true;

        if (_listaUsuarios.Count >= 1)
        {
            foreach (Usuario usuario in _listaUsuarios)
            {
                // Si el usuario actual NO es administrador y el usuario en la lista SÍ lo es, no generamos la carta
                if (_tipoUsuario != TipoUsuario.Administrador && usuario.TipoDeUsuario == TipoUsuario.Administrador)
                {
                    continue; // Saltar este usuario
                }

                // Lógica para decidir si se muestra el botón eliminar
                if (_tipoUsuario == TipoUsuario.GestorGimnasios && usuario.TipoDeUsuario == TipoUsuario.GestorGimnasios)
                {
                    generarBotonEliminar = false;
                }
                else
                {
                    generarBotonEliminar = true;
                }

                VerticalStackLayoutUsuarios.Children.Add(
                    GeneracionUI.CrearCartaUsuarioGestor(
                        usuario,
                        CartaClickeada,
                        ControladorBotones,
                        ControladorBotones,
                        true,
                        generarBotonEliminar
                    )
                );
            }
        }
        else
        {
            VerticalStackLayoutUsuarios.Children.Add(
                new Label()
                {
                    Text = "No hay Usuarios Disponibles",
                    TextColor = Colors.Gray
                }
            );
        }
    }

    public virtual void CartaClickeada(object sender, TappedEventArgs e)
    {
        try
        {
            // Buscar el Frame desde el sender
            Frame carta = null;
            if (sender is Frame frame)
            {
                carta = frame; // El sender ya es la carta
            }
            else if (sender is VisualElement elemento)
            {
                // Buscar en la jerarquía de elementos hasta encontrar el Frame
                while (elemento != null && !(elemento is Frame))
                {
                    elemento = elemento.Parent as VisualElement;
                }
                carta = elemento as Frame;
            }

            if (carta != null && carta.Content is VerticalStackLayout layout)
            {
                if (layout.Children[0] is Label nombreLabel)
                {
                    foreach (var usuario in _listaUsuarios)
                    {
                        if (usuario.Correo == nombreLabel.Text)
                        {
                            _usuarioElegido = usuario;
                            break; // Terminar el bucle cuando encontramos el usuario
                        }
                    }
                }
            }
        }
        catch (Exception error)
        {
            Application.Current.MainPage.DisplayAlert("Error", error.Message, "Aceptar");
        }
        finally
        {
            recargarUI();
        }
    }




    // EVENTOS

    // Controlador para el evento generado por las cards de Escuelas, el cual pasa al selector de usuarios
    protected virtual void ControladorCardsSelectorEscuela(Escuela escuela)
    {

    }

    // Controlador para el evento generado por las cards de usuarios
    protected virtual void ControladorCardsSelectorUsuario(Usuario usuario)
    {

    }

    protected virtual void ControladorBotones(object sender, EventArgs e)
    {

    }
}