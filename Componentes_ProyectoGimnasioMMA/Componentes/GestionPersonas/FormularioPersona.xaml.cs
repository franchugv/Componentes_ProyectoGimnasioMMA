using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using BibliotecaClases_ProyectoGimnasioMMA.Deportes;
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
    protected EntryValidacion _eDNI;
    protected EntryValidacion _eNombre;
    protected EntryValidacion _eApellidos;

    protected Picker _selectorEscuela;

    protected API_BD _api_bd;

    protected Usuario _usuario;
    protected Escuela _escuela;

    protected List<Escuela> _escuelaList;

    // CONSTRUCOR
    public FormularioPersona(Escuela escuela, Usuario usuario)
	{
		InitializeComponent();

        CargarEnConstructor(escuela, usuario);

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

    protected virtual void CargarEnConstructor(Escuela escuela, Usuario usuario)
    {
        try
        {
            _escuela = escuela;
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





    protected virtual void GenerarUI()
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

    protected void pickerFocusChanged(object sender, EventArgs e)
    {

    }
}