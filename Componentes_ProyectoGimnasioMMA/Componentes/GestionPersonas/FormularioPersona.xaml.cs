using BibliotecaClases_ProyectoGimnasioMMA.Persona;
using BibliotecaClases_ProyectoGimnasioMMA.Personas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes.Funciones;

namespace Componentes_ProyectoGimnasioMMA.Componentes.GestionPersonas;

public partial class FormularioPersona : ContentView
{
    // Recursos
    private EntryValidacion _eDNI;
    private EntryValidacion _eNombre;
    private EntryValidacion _eApellidos;
    private EntryValidacion _eID_Escuela;

    public FormularioPersona()
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
    public string DNI
    {
        get
        {
            return _eDNI.Texto;
        }
    }
    public string Nombre
    {
        get
        {
            return _eNombre.Texto;
        }
    }
    public string Apellidos
    {
        get
        {
            return _eApellidos.Texto;
        }
    }
    public int ID_Escuela
    {
        get
        {
            return Convert.ToInt32(_eID_Escuela.Texto);
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
                case "eDNI":
                    _eDNI.limpiarError();
                    persona = new PersonaValidacion(_eDNI.Texto, TipoMiembro.DNI);
                    break;

                case "eNombre":
                    _eNombre.limpiarError();
                    persona = new PersonaValidacion(_eNombre.Texto, TipoMiembro.Nombre);
                    break;

                case "eApellidos":
                    _eApellidos.limpiarError();
                    persona = new PersonaValidacion(_eApellidos.Texto, TipoMiembro.Apellidos);
                    break;

                case "eID_Escuela":
                    _eID_Escuela.limpiarError();
                    persona = new PersonaValidacion(_eID_Escuela.Texto, TipoMiembro.ID_Escuela);
                    break;

            }

        }
        catch (Exception error)
        {
            switch (entry.StyleId)
            {
                case "eDNI":
                    _eDNI.mostrarError(error.Message);
                    break;
                case "eNombre":
                    _eNombre.mostrarError(error.Message);
                    break;
                case "eApellidos":
                    _eApellidos.mostrarError(error.Message);
                    break;
                case "eID_Escuela":
                    _eID_Escuela.mostrarError(error.Message);
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
        _eDNI = GeneracionUI.CrearEntryError("DNI", "eDNI", entryUnfocus);
        _eNombre = GeneracionUI.CrearEntryError("Nombre", "eNombre", entryUnfocus);
        _eApellidos = GeneracionUI.CrearEntryError("Apellidos", "eApellidos", entryUnfocus);
        _eID_Escuela = GeneracionUI.CrearEntryError("ID Escuela", "eID_Escuela", entryUnfocus);

        // Añadir interfaz al vsl
        UI_VSL.Children.Add(
            _eDNI
        );
        UI_VSL.Children.Add(
            _eNombre
        );
        UI_VSL.Children.Add(
            _eApellidos
        );
        UI_VSL.Children.Add(
            _eID_Escuela
        );


    }

}