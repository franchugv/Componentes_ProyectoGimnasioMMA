using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using BibliotecaClases_ProyectoGimnasioMMA.Deportes;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
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

    List<Escuela> _listaEscuelas;
    List<string> _listaNombreEscuelas;
    Escuela _escuelaElegida;
    // COMPONENTES

    VerticalStackLayout _mainVSL;

    EntryConfirmacion _eNombre;
    EntryConfirmacion _eFederacion;

    PickerConfirmacion _selectorEscuela;
    
    Button _botonInsertar;


    public event Action EventoVolverPaginaPrincipal;

    public EditarDeportes(Usuario usuarioAPP, Escuela escuela, Deporte deporte)
    {

        _usuarioApp = usuarioAPP;
        _escuela = escuela;
        _deporte = deporte;
        CargarConstructor();
        Content = _mainVSL;

    }
    protected void CargarConstructor()
    {
        _mainVSL = new VerticalStackLayout();
        _api_BD = new API_BD();

        _listaEscuelas = new List<Escuela>();
        _listaNombreEscuelas = new List<string>();
        _listaEscuelas = _api_BD.ObtenerEscuelasDeUsuario(_usuarioApp.Correo);

        foreach (Escuela escuela in _listaEscuelas)
        {
            _listaNombreEscuelas.Add(escuela.Nombre);
        }
        GenerarUI();
    }
    protected void GenerarUI()
    {
        _eNombre = GeneracionUI.CrearEntryConfirmacion("Ingrese el Nombre del deporte a agregar", "eDeporte", unfocusedEntry);
        _eFederacion = GeneracionUI.CrearEntryConfirmacion("Ingrese la Federación del deporte a agregar", "eFederacion", unfocusedEntry);
        _selectorEscuela = GeneracionUI.CrearPickerConfirmacion("pEscuela", "Seleccione una Escuela para a Cambiar", _listaNombreEscuelas, unfocusedPicker);
        _botonInsertar = GeneracionUI.CrearBoton("Insertar Deporte", "bIDeporte", controladorBotones);

        _mainVSL.Children.Add(_eNombre);
        _mainVSL.Children.Add(_eFederacion);
        _mainVSL.Children.Add(_selectorEscuela);
        _mainVSL.Children.Add(_botonInsertar);
    }

    private void controladorBotones(object sender, EventArgs e)
    {
        try
        {



            string nombre = null;
            string federación = null;
            int nuevaEscuelaID = -1;


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

            if (_selectorEscuela.EstaSeleccionado)
            {
                if (_selectorEscuela.PickerEditar.SelectedItem == null) throw new Exception("Debe seleccionar una Escuela");

                for (int indice = 0; indice < _listaEscuelas.Count; indice++)
                {
                    if (_selectorEscuela.PickerEditar.SelectedItem.ToString() == _listaEscuelas[indice].Nombre) _escuelaElegida = _listaEscuelas[indice];
                }

                nuevaEscuelaID = _escuelaElegida.Id;
            }

            _api_BD.EditarDeporte(_deporte.Id, nombre, federación, nuevaEscuelaID);
            


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

    }
}