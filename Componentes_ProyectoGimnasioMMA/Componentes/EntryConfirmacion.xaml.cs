using Microsoft.Maui.Controls;

namespace Componentes_ProyectoGimnasioMMA.Componentes;

public partial class EntryConfirmacion : ContentView
{
    private bool _cambiandoEstado = false;

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
            return checkBox.IsChecked;
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