using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Persona;
using BibliotecaClases_ProyectoGimnasioMMA.Personas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes.Funciones;

namespace Componentes_ProyectoGimnasioMMA.Componentes.GestionPersonas;

/// <summary>
/// Esta Aplicación esta pensada solo para los Gestores de Gimnasios
/// </summary>
public partial class FormularioPersona : ContentView
{
    // Recursos
    private EntryValidacion _eDNI;
    private EntryValidacion _eNombre;
    private EntryValidacion _eApellidos;

    private Picker _selectorEscuela;

    API_BD _api_bd;

    Usuario _usuario;
    Escuela _escuela;
    List<Escuela> _escuelaList;

    // CONSTRUCOR
    public FormularioPersona(Usuario usuario)
	{
		InitializeComponent();

        CargarEnConstructor(usuario);

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
    


    // EVENTOS

    protected virtual void CargarEnConstructor(Usuario usuario)
    {
        try
        {
            _usuario = usuario;
            _api_bd = new API_BD();

            // Asignamos esta lista para el picker
            _escuelaList = _api_bd.ObtenerEscuelasDeUsuario(_usuario.Correo);
        }
        catch (Exception error)
        {
            Application.Current.MainPage.DisplayAlert("ERROR", error.Message, "Ok");
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

            }
        }

    }

    // Evento a heredar en las clases hijas
    public virtual void controladorBoton(object sender, EventArgs e)
    {

    }



    public virtual void GenerarUI()
    {
        List<string> listaNombresEscuelas = new List<string>();

        foreach(Escuela escuela in _escuelaList)
        {
            listaNombresEscuelas.Add(escuela.Nombre);
        }

        // Inctanciar Componentes de la interfaz
        _eDNI = GeneracionUI.CrearEntryError("DNI", "eDNI", entryUnfocus);
        _eNombre = GeneracionUI.CrearEntryError("Nombre", "eNombre", entryUnfocus);
        _eApellidos = GeneracionUI.CrearEntryError("Apellidos", "eApellidos", entryUnfocus);
        _selectorEscuela = GeneracionUI.CrearPicker("sEscuela", "Seleccione una Escuela", listaNombresEscuelas, pickerFocusChanged);

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
            _selectorEscuela
        );


    }

    private void pickerFocusChanged(object sender, EventArgs e)
    {

    }
}