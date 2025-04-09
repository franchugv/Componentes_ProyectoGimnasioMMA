using Microsoft.Maui.Controls;

namespace Componentes_ProyectoGimnasioMMA.Componentes;

public partial class EntryConfirmacion : ContentView
{

    public EntryConfirmacion(string nombre)
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
        set
        {
            entryDato.Text = value;
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

    public bool EstaSeleccionado
    {
        get
        {
            bool esValido = true;
            if (string.IsNullOrEmpty(entryDato.Text))
            {
                esValido = false;
            }

            return esValido;
        }
    }

    // EVENTOS

 

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