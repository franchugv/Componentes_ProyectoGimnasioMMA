using BibliotecaClases_ProyectoGimnasioMMA.Persona;
using BibliotecaClases_ProyectoGimnasioMMA.Personas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes.Funciones;

namespace Componentes_ProyectoGimnasioMMA.Componentes.GestionGimnasios;

public partial class FormularioGimnasio : ContentView
{
    // Recursos
    protected EntryValidacion _eNombre;
    protected EntryValidacion _eUbicacion;

    public FormularioGimnasio()
    {
        InitializeComponent();

        // CargarEnConstructor();

    }


    // PROPIEDADES

    public VerticalStackLayout MAIN_VSL
    {
        get
        {
            return UI_VSL;
        }
        set
        {
            UI_VSL = value;
        }
    }
    public string Nombre
    {
        get
        {
            return _eNombre.Texto;
        }
    }
    public string Ubicacion
    {
        get
        {
            return _eUbicacion.Texto;
        }
    }
 


    // EVENTOS

    private void CargarEnConstructor()
    {
        try
        {
            GenerarUI();
        }
        catch (Exception error)
        {

        }
    }



    protected virtual void entryUnfocus(object sender, FocusEventArgs e)
    {
        Entry entry = (Entry)sender;
        PersonaValidacion persona = null;

        try
        {
            switch (entry.StyleId)
            {
                case "_eNombre":
                    _eNombre.limpiarError();
                    persona = new PersonaValidacion(_eNombre.Texto, TipoMiembro.DNI);
                    break;

                case "_eUbicacion":
                    _eUbicacion.limpiarError();
                    persona = new PersonaValidacion(_eNombre.Texto, TipoMiembro.Nombre);
                    break;

           

            }

        }
        catch (Exception error)
        {
            switch (entry.StyleId)
            {
                case "_eNombre":
                    _eNombre.mostrarError(error.Message);
                    break;
                case "_eUbicacion":
                    _eUbicacion.mostrarError(error.Message);
                    break;

            }
        }

    }

    // Evento a heredar en las clases hijas
    public virtual void controladorBoton(object sender, EventArgs e)
    {

    }



    public virtual void GenerarUI()
    {
        // Inctanciar Componentes de la interfaz
        _eNombre = GeneracionUI.CrearEntryError("DNI", "eDNI", entryUnfocus);
        _eUbicacion = GeneracionUI.CrearEntryError("Nombre", "eNombre", entryUnfocus);


        // Añadir interfaz al vsl
        UI_VSL.Children.Add(
            _eNombre
        );
        UI_VSL.Children.Add(
            _eUbicacion
        );



    }
}