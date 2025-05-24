using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using BibliotecaClases_ProyectoGimnasioMMA.Deportes;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes.Funciones;

namespace Componentes_ProyectoGimnasioMMA.Componentes.GestionDeportes;

public class AgregarDeporte : ContentView
{
	// Recursos

	API_BD _api_BD;

	Usuario _usuarioApp;
	Escuela _escuela;


	List<Escuela> _listaEscuelas;
	List<string> _listaNombreEscuelas;
	Escuela _escuelaElegida;
	// COMPONENTES

	VerticalStackLayout _mainVSL;

	EntryValidacion _eNombre;
	EntryValidacion _eFederacion;
	Picker _selectorEscuela;
	Button _botonInsertar;

    public event Action EventoVolverPaginaPrincipal;

    // CONSTRUCTOR
    public AgregarDeporte(Usuario usuarioAPP, Escuela escuela)
	{

		_usuarioApp = usuarioAPP;
		_escuela = escuela;
		CargarConstructor();
        Content = _mainVSL;

    }
	
    // INICIALIZACIÓN
    protected void CargarConstructor()
	{
        _mainVSL = new VerticalStackLayout();
        _api_BD = new API_BD();

		_listaEscuelas = new List<Escuela>();
		_listaNombreEscuelas = new List<string>();
		_listaEscuelas = _api_BD.ObtenerEscuelasDeUsuario(_usuarioApp.Correo);

		foreach(Escuela escuela in _listaEscuelas)
		{
			_listaNombreEscuelas.Add(escuela.Nombre);
		}
		GenerarUI();
    }

	protected void GenerarUI()
	{
		_eNombre = GeneracionUI.CrearEntryError("Ingrese el Nombre del deporte a agregar", "eNombre", 100, unfocusedEntry);
		_eFederacion = GeneracionUI.CrearEntryError("Ingrese la Federación del deporte a agregar", "eFederacion",100, unfocusedEntry);
		_selectorEscuela = GeneracionUI.CrearPicker("pEscuela", "Seleccione una Escuela para el Deporte", _listaNombreEscuelas, unfocusedPicker);
		_botonInsertar = GeneracionUI.CrearBoton("Insertar Deporte", "bIDeporte", controladorBotones);

		_mainVSL.Children.Add(_eNombre);
        _mainVSL.Children.Add(_eFederacion);
        _mainVSL.Children.Add(_selectorEscuela);

        _botonInsertar.BackgroundColor = Colors.Green;
        _mainVSL.Children.Add(_botonInsertar);
    }

    // EVENTOS
    private async void controladorBotones(object sender, EventArgs e)
    {
		try
		{
            bool confirmar = await GeneracionUI.MostrarConfirmacion(Application.Current.MainPage, "Ventana confirmación", $"¿Desea Agregar un nuevo Deporte?");

            if (confirmar)
            {

                AgregarDeporteBD();
            }
        }
        catch (Exception error)
		{
			await Application.Current.MainPage.DisplayAlert("ERROR", error.Message, "Ok");
		}
    }

    private void AgregarDeporteBD()
    {

        try
        {
            if (_selectorEscuela.SelectedItem == null) throw new Exception("Debe seleccionar una Escuela");

            for (int indice = 0; indice < _listaEscuelas.Count; indice++)
            {
                if (_selectorEscuela.SelectedItem.ToString() == _listaEscuelas[indice].Nombre) _escuelaElegida = _listaEscuelas[indice];
            }

            Deporte deporte = new Deporte(_eNombre.Texto, _eFederacion.Texto, _escuelaElegida.Id);

            // Insertamos el deporte
            _api_BD.InsertarDeporte(deporte);
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