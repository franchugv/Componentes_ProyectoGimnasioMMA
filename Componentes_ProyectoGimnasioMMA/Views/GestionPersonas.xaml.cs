using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Persona;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;

namespace Componentes_ProyectoGimnasioMMA.Views;

public partial class GestionPersonas : ContentPage
{
    // Recursos
    TipoUsuario _tipoUsuario;

    Escuela _escuela;
    API_BD api_bd;





    public GestionPersonas(Escuela escuela)
	{
		InitializeComponent();
        _escuela = escuela;

        CargarDatosConstructor();

    }

    // PROPIEDADES



    // EVENTOS
    private void CargarDatosConstructor()
    {
        try
        {
            Shell.SetNavBarIsVisible(this, false);
            api_bd = new API_BD();
        }
        catch (Exception error)
        {
            DisplayAlert("ERROR", error.Message, "OK");
        }
    }

    // Evento Picker
    protected virtual void SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected virtual void controladorBotones(object sender, EventArgs e)
    {

    }
}