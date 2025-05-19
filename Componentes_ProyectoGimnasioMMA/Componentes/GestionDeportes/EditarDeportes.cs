using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using BibliotecaClases_ProyectoGimnasioMMA.Deportes;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Persona;
using BibliotecaClases_ProyectoGimnasioMMA.Personas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes.Funciones;

namespace Componentes_ProyectoGimnasioMMA.Componentes.GestionDeportes;

public class EditarDeportes : ContentView
{
    // Recursos

    API_BD _api_BD;

    Usuario _usuarioApp;
    Escuela _escuela;
    Deporte _deporte;


    // COMPONENTES

    VerticalStackLayout _mainVSL;

    EntryConfirmacion _eNombre;
    EntryConfirmacion _eFederacion;
    
    Button _botonInsertar;


    public event Action EventoVolverPaginaPrincipal;

    // CONSTRUCTOR
    public EditarDeportes(Usuario usuarioAPP, Escuela escuela, Deporte deporte)
    {

        _usuarioApp = usuarioAPP;
        _escuela = escuela;
        _deporte = deporte;
        CargarConstructor();
        Content = _mainVSL;

    }

    // INICIALIZACIÓN
    protected void CargarConstructor()
    {
        _mainVSL = new VerticalStackLayout();
        _api_BD = new API_BD();


        GenerarUI();
    }

    protected void GenerarUI()
    {
        _eNombre = GeneracionUI.CrearEntryConfirmacion("Ingrese el Nombre del deporte a Actualizar", "eNombre", 100, unfocusedEntry);
        _eFederacion = GeneracionUI.CrearEntryConfirmacion("Ingrese la Federación del deporte a Actualizar", "eFederacion", 100, unfocusedEntry);
        _botonInsertar = GeneracionUI.CrearBoton("Actualizar Deporte", "bIDeporte", controladorBotones);
        _botonInsertar.BackgroundColor = Colors.Green;

        _mainVSL.Children.Add(_eNombre);
        _mainVSL.Children.Add(_eFederacion);
        _mainVSL.Children.Add(_botonInsertar);

        AsignarDatos();
    }

    private void AsignarDatos()
    {
        _eNombre.Texto = _deporte.Nombre;
        _eFederacion.Texto = _deporte.Federacion;
       
    }

    // EVENTOS
    private void controladorBotones(object sender, EventArgs e)
    {
        try
        {



            string nombre = null;
            string federación = null;


            if (_eNombre.EstaSeleccionado)
            {
                Deporte deporteN = new Deporte(_eNombre.Texto, TipoValorDeporte.Nombre);
                nombre = deporteN.Nombre;
            }

            if (_eFederacion.EstaSeleccionado)
            {
                Deporte deporteF = new Deporte(_eFederacion.Texto, TipoValorDeporte.Federacion);
                federación = deporteF.Federacion;
            }

           

            _api_BD.EditarDeporte(_deporte.Id, nombre, federación);
            


            // Insertamos el deporte
            EventoVolverPaginaPrincipal?.Invoke();

        }
        catch (Exception error)
        {
            Application.Current.MainPage.DisplayAlert("ERROR", error.Message, "Ok");
        }
    }

    private void unfocusedPicker(object sender, EventArgs e)
    {

    }

    private void unfocusedEntry(object sender, FocusEventArgs e)
    {
        Entry entry = (Entry)sender;
        Deporte deporteValidar = null;

        try
        {
            switch (entry.StyleId)
            {
                case "eNombre":
                    _eNombre.limpiarError();
                    deporteValidar = new Deporte(_eNombre.Texto, TipoValorDeporte.Nombre);
                    break;

                case "eFederacion":
                    _eFederacion.limpiarError();
                    deporteValidar = new Deporte(_eFederacion.Texto, TipoValorDeporte.Federacion);
                    break;





            }

        }
        catch (Exception error)
        {
            switch (entry.StyleId)
            {
                case "eNombre":
                    _eNombre.mostrarError(error.Message);
                    break;
                case "eFederacion":
                    _eFederacion.mostrarError(error.Message);
                    break;


            }
        }
    }
}