using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using BibliotecaClases_ProyectoGimnasioMMA.Deportes;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Personas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes.Funciones;

namespace Componentes_ProyectoGimnasioMMA.Views;

public partial class GestionClases : ContentPage
{
    // Recursos
    protected List<Horario> _listaHorarios;
    protected Horario _horarioElegido;


    protected Escuela _escuela;
    protected Usuario _usuario;

    protected API_BD _api_bd;

    public GestionClases(Escuela escuela, Usuario usuario)
	{
		InitializeComponent();

        CargarDatosConstructor(escuela, usuario);

    }

	// CONTROLADORES
    protected virtual void ControladorBotones(object sender, EventArgs e)
    {

    }


    // PROPIEDADES
    public StackLayout MAUINSL
    {
        get
        {
            return VerticalStackLayoutClases;
        }
        set
        {
            VerticalStackLayoutClases = value;
        }
    }

    // INICIALIZACIÓN
    private void CargarDatosConstructor(Escuela escuela, Usuario usuario)
    {
        try
        {
            Shell.SetNavBarIsVisible(this, false);
            _api_bd = new API_BD();

            _escuela = escuela;
            _usuario = usuario;

            // Asignar listas
            _listaHorarios = _api_bd.ObtenerClasesPorEscuela(_escuela.Id);

            generarUI();
        }
        catch (Exception error)
        {
            DisplayAlert("ERROR", error.Message, "OK");
        }
    }

    protected virtual void generarUI()
    {



        // Generar Cards de Profesores
        VerticalStackLayoutClases.Add(new Label() { Text = "Clases", HorizontalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold });

        // Ordenamos la lista según los día de la semana
        List<Horario> listaOrdenada = _listaHorarios.OrderBy(x => x.Dia).ToList();

        if (_listaHorarios.Count >= 1)
        {
            foreach (Horario horario in listaOrdenada)
            {
                VerticalStackLayoutClases.Children.Add(GeneracionUI.CrearCartaClases(horario, CartaClickeada, ControladorBotones, ControladorBotones, ControladorBotones));
            }
        }
        else
        {
            VerticalStackLayoutClases.Children.Add(new Label() { Text = "No hay Clases Disponibles", TextColor = Colors.Gray , HorizontalOptions = LayoutOptions.Center});
        }

        
    }

    // EVENTOS
    protected virtual void CartaClickeada(object sender, TappedEventArgs e)
    {
        try
        {
            // Buscar el Frame desde el sender
            Frame carta = null;
            if (sender is Frame frame)
            {
                carta = frame;
            }
            else if (sender is VisualElement elemento)
            {
                while (elemento != null && !(elemento is Frame))
                {
                    elemento = elemento.Parent as VisualElement;
                }
                carta = elemento as Frame;
            }

            if (carta != null && carta.Content is VerticalStackLayout layout && layout.Children.Count >= 3)
            {
                string deporteIdTexto = _api_bd.ObtenerIdDeportePorNombre(((Label)layout.Children[0]).Text, _escuela.Id).ToString();
                string profesorDni = _api_bd.ObtenerDniPorNombreProfesor(((Label)layout.Children[1]).Text, _escuela.Id);
                string dia = ((Label)layout.Children[2]).Text.Replace("Día: ", "");
                string horaInicioTexto = ((Label)layout.Children[3]).Text.Replace("Hora Inicio: ", "");
                if (int.TryParse(deporteIdTexto, out int deporteId) && TimeSpan.TryParse(horaInicioTexto, out TimeSpan horaInicio))
                {
                    foreach (Horario horario in _listaHorarios)
                    {
                        if (horario.ProfesorDni == profesorDni &&
                            horario.DeporteId == deporteId &&
                            horario.HoraInicio == horaInicio &&
                            horario.Dia.ToString() == dia)
                        {
                            _horarioElegido = horario;
                            break;
                        }
                    }
                }
                else
                {
                    throw new Exception("Formato incorrecto en los datos de la carta." );
                }
            }
            else
            {
                throw new Exception("La carta no tiene los elementos necesarios.");
            }
        }
        catch (Exception error)
        {
            Application.Current.MainPage.DisplayAlert("Error", error.Message, "Aceptar");
        }
    }

    protected void recargarUI()
    {

        try
        {
            _listaHorarios = _api_bd.ObtenerClasesPorEscuela(_escuela.Id);

            VerticalStackLayoutClases.Clear();
            generarUI();
        }
        catch (Exception error)
        {
            DisplayAlert("ERROR", error.Message, "OK");
        }
        // Recargar listas listas

    }



}