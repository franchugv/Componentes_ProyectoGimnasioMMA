using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using BibliotecaClases_ProyectoGimnasioMMA.Deportes;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Personas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes.Funciones;

namespace Componentes_ProyectoGimnasioMMA.Componentes.GestionClases;

public class AgregarClase : ContentView
{
	// RECURSOS
	protected VerticalStackLayout MainVSL;

    // Contoles
    // TimePickes
	protected TimePicker _tpHoraInicio;
	protected TimePicker _tpHoraFin;

    // Pickers
	protected Picker _selectorDia;
	//protected Picker _selectorEscuela;
	protected Picker _selectorDeporte;
	protected Picker _selectorProfesor;

    // Botones
	protected Button _botonInsertar;

	// Listas Escuelas
	//List<Escuela> _listaEscuelas;
	//List<string> _listaNombreEscuelas;
    //Escuela _escuelaElegida;

    // Listas depores
    List<Deporte> _listaDeportes;
	List<string> _listaNombreDeportes;
    Deporte _deporteElegido;

    // Listas Profesores
    List<Profesores> _listaProfesores;
	List<string> _listaNombreProfesores;
    Profesores _profesorElegido;


    API_BD _api_bd;

	Escuela _escuela;
	Usuario _usuario;

    public event Action EventoVolverPaginaPrincipal;

    public AgregarClase(Escuela escuela, Usuario usuario)
	{
		_escuela = escuela;
		_usuario = usuario;

		CargarConstructor();

		Content = MainVSL;

	}

    // INICIALIZACI�N
	private void CargarConstructor()
	{
		_api_bd = new API_BD();


		// Asignar Lista Escuelas
		//_listaEscuelas = new List<Escuela>();
		//_listaEscuelas = _api_bd.ObtenerEscuelasAsignadasAProfesorYUsuario(_usuario.Correo, );
		//_listaNombreEscuelas = new List<string>();

		//foreach(Escuela escuela in _listaEscuelas)
		//{
		//	_listaNombreEscuelas.Add(escuela.Nombre);
		//}

		// Asignar Lista Deportes
		_listaDeportes = new List<Deporte>();
		_listaDeportes = _api_bd.DevolverListaDeportes(_escuela.Id);
        _listaNombreDeportes = new List<string>();

		foreach(Deporte deporte in _listaDeportes)
		{
			_listaNombreDeportes.Add(deporte.Nombre);
		}

		// Asignar Lista Profesores

		_listaProfesores = new List<Profesores>();
		_listaProfesores = _api_bd.ObtenerProfesoresPorEscuela(_escuela.Id);
		_listaNombreProfesores = new List<string>();

		foreach(Profesores profesor in _listaProfesores)
		{
			_listaNombreProfesores.Add($"{profesor.DNI}, {profesor.Nombre} {profesor.Apellidos}");
		}

		GenerarUI();

        if (_api_bd.ObtenerProfesoresPorEscuela(_escuela.Id).Count <= 0) Application.Current.MainPage.DisplayAlert("Notificaci�n", "No se puede Agregar una Clase sin Profesores, Agregue un Profesor", "Ok");

    }

    private void GenerarUI()
    {
        ScrollView scroll = new ScrollView();
        _tpHoraInicio = GeneracionUI.CrearTimePicker("tpHoraInicio", tpUnfocused);
        _tpHoraFin = GeneracionUI.CrearTimePicker("tpHoraFin", tpUnfocused);

        _selectorDia = GeneracionUI.CrearPicker("selectorDia", "Seleccione un D�a de la Semana", Horario.DiasSemanaString, pUnfocused);
        //_selectorEscuela = GeneracionUI.CrearPicker("selectorEscuela", "Seleccione una Escuela", _listaNombreEscuelas, pUnfocused);
        _selectorDeporte = GeneracionUI.CrearPicker("selectorDia", "Seleccione un Deporte para la Clase", _listaNombreDeportes, pUnfocused);
        if (_listaNombreDeportes.Count <= 0) _selectorDeporte.IsEnabled = false;

        _selectorProfesor = GeneracionUI.CrearPicker("selectorProfesor", "Seleccione un Profesor para el Deporte", _listaNombreProfesores, pUnfocused);

        _botonInsertar = GeneracionUI.CrearBoton("Insertar Clase", "bInsertar", controladorBotones);
        _botonInsertar.BackgroundColor = Colors.Green;
        _botonInsertar.FontAttributes = FontAttributes.Bold;

        VerticalStackLayout contenidoScroll = new VerticalStackLayout()
        {
            HorizontalOptions = LayoutOptions.Start,
            Children =
            {
                new Label(){Text = "Hora de Inicio", Margin = new Thickness(10, 2, 2, 2)},
                _tpHoraInicio,

                 new Label(){Text = "Hora de Fin", Margin = new Thickness(10, 2, 2, 2)},
                _tpHoraFin,
                _selectorDia,
                //_selectorEscuela,
                _selectorDeporte,
                _selectorProfesor
            }
            
        };

        scroll.Content = contenidoScroll;

        MainVSL = new VerticalStackLayout()
        {
            Children =
            {
                contenidoScroll,
                _botonInsertar
            }
        };


    }

    // VALIDACI�N
    private void validarTimePickers()
    {
        if (_tpHoraInicio == null || _tpHoraFin == null)
            throw new Exception("Los selesctores de hora deben estar inicializados");

        DateTime inicio = DateTime.Today.Add(_tpHoraInicio.Time);
        DateTime fin = DateTime.Today.Add(_tpHoraFin.Time);

        if (fin <= inicio)
            fin = fin.AddDays(1); // Sumamos un d�a si la hora de fin es t�cnicamente despu�s de medianoche

        TimeSpan duracion = fin - inicio;

        if (duracion.TotalMinutes <= 0 || duracion.TotalHours > 12)
            throw new Exception("Duraci�n inv�lida. La clase no puede durar m�s de 12 horas.");

    }

    // EVENTOS
    private async void controladorBotones(object sender, EventArgs e)
    {
		try
		{

            bool confirmar = await GeneracionUI.MostrarConfirmacion(Application.Current.MainPage, "Ventana confirmaci�n", $"�Desea una nueva Clase?");

            if (confirmar)
            {
                AgregarClaseBD();
            }
        }
        catch (Exception error)
		{
			await Application.Current.MainPage.DisplayAlert("ERROR", error.Message, "Ok");
		}
    }

    private void AgregarClaseBD()
    {

        try
        {
            // Asinar el profesor
            if (_selectorProfesor == null || _selectorProfesor.SelectedItem == null) throw new Exception("Debe seleccionar un Profesor");

            string dni = _selectorProfesor.SelectedItem.ToString().Split(", ")[0];
            for (int indice = 0; indice < _listaProfesores.Count; indice++)
            {
                if (_listaProfesores[indice].DNI == dni) _profesorElegido = _listaProfesores[indice];
            }

            // Asignar el deporte
            if (_selectorDeporte == null || _selectorDeporte.SelectedItem == null) throw new Exception("Debe seleccionar un Deporte");

            string nombreDeporte = _selectorDeporte.SelectedItem.ToString();
            for (int indice = 0; indice < _listaDeportes.Count; indice++)
            {
                if (_listaDeportes[indice].Nombre == nombreDeporte) _deporteElegido = _listaDeportes[indice];
            }

            // Asignar la escuela
            //if (_selectorEscuela == null || _selectorEscuela.SelectedItem == null) throw new Exception("Debe seleccionar una Escuela");

            //string escuelaNombre = _selectorEscuela.SelectedItem.ToString();
            //for (int indice = 0; indice < _listaEscuelas.Count; indice++)
            //{
            //    if (_listaEscuelas[indice].Nombre == escuelaNombre) _escuelaElegida = _listaEscuelas[indice];
            //}

            // Validar que tengamos seleccionado el d�a
            if (_selectorDia == null || _selectorDia.SelectedItem == null) throw new Exception("Debe seleccionar un D�a");

            // He quitado el selector de escuelas, ya que si agregas una clase en una escuela en la cual no est� el profesor asignado no tiene mucho sentido, asi que le asigno la escuela en la que est� 
            Horario horario = new Horario
                (_tpHoraInicio.Time, _tpHoraFin.Time, Horario.ConvertirStringADiaSemana(_selectorDia.SelectedItem.ToString()), _profesorElegido.DNI, _deporteElegido.Id, _escuela.Id);

            _api_bd.InsertarHorario(horario);

            EventoVolverPaginaPrincipal?.Invoke();
        }
        catch (Exception error)
        {
            Application.Current.MainPage.DisplayAlert("ERROR", error.Message, "Ok");
        }
    }

    private void tpUnfocused(object sender, FocusEventArgs e)
    {
        TimePicker timePicker = (TimePicker)sender;
        try
        {
            switch (timePicker.StyleId)
            {
                case "tpHoraInicio":

                    break;
                case "tpHoraFin":
                    // Se genera un bucle infinito, colocar en el evento del bot�n
                    //validarTimePickers();

                    break;


            }
        }
        catch(Exception error)
        {
            Application.Current.MainPage.DisplayAlert("ERROR", error.Message, "Ok");
        }
    }

    private void pUnfocused(object sender, EventArgs e)
    {

    }
}