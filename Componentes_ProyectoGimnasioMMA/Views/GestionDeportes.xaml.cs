using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using BibliotecaClases_ProyectoGimnasioMMA.Deportes;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes.Funciones;

namespace Componentes_ProyectoGimnasioMMA.Views;

public partial class GestionDeportes : ContentPage
{
    // Recursos

    protected Escuela _escuela;
    protected API_BD api_bd;
    protected Usuario _usuario;

    protected List<Deporte> _listaDeportes;
    protected Deporte _deporteElegido;







    // Constructores
    public GestionDeportes(Usuario usuario, Escuela escuela)
    {
        InitializeComponent();


        CargarDatosConstructor(usuario, escuela);


    }

    // PROPIEDADES



    // EVENTOS
    private void CargarDatosConstructor(Usuario usuario, Escuela escuela)
    {
        try
        {
            Shell.SetNavBarIsVisible(this, false);

            api_bd = new API_BD();

            _usuario = usuario;
            _escuela = escuela;

            // Asignar deportes filtrando por nuestra escuela elegida
            _listaDeportes = api_bd.DevolverListaDeportes(_escuela.Id);

            GenerarInterfaz();
        }
        catch (Exception error)
        {
            DisplayAlert("ERROR", error.Message, "OK");
        }
    }



    protected void recargarUI()
    {
        try
        {
            _listaDeportes = api_bd.DevolverListaDeportes(_escuela.Id);

            VerticalStackLayoutPersonas.Clear();
            GenerarInterfaz();
        }
        catch (Exception error)
        {
            DisplayAlert("ERROR", error.Message, "OK");
        }
    }




    protected virtual void GenerarInterfaz()
    {

        if(_listaDeportes.Count >= 1)
        {
            foreach (Deporte deporte in _listaDeportes)
            {
                VerticalStackLayoutPersonas.Children.Add(GeneracionUI.CrearCartaDeporteGestor(deporte, CartaClickeada, ControladorBotones, ControladorBotones));
            }
        }
        else
        {
            VerticalStackLayoutPersonas.Children.Add(new Label() { Text = "No hay Deportes Disponibles", TextColor = Colors.Gray });
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
                    foreach (Deporte deporte in _listaDeportes)
                    {
                        if (deporte.Id.ToString() == nombreLabel.Text)
                        {
                            _deporteElegido = deporte;
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

    protected virtual void ControladorBotones(object sender, EventArgs e)
    {

    }
}