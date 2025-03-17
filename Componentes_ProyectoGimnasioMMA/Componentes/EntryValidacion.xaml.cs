using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Componentes_ProyectoGimnasioMMA.Componentes;

public partial class EntryValidacion : ContentView
{
    public EntryValidacion(string nombre)
    {
        InitializeComponent();

        entryDato.Placeholder = nombre;
        entryDato.StyleId = nombre;
    }


    // PROPIEDADES 

    public string Texto
    {
        get
        {
            return entryDato.Text;
        }
    }

    public Entry EntryEditar
    {
        get
        {
            return entryDato;
        }
        set
        {
            entryDato = value;
        }
    }


    // MÉTODOS

    public void mostrarError(string error)
    {
        lError.Text = "ERROR: " + error;
    }

    public void limpiarError()
    {
        lError.Text = "";
    }
}