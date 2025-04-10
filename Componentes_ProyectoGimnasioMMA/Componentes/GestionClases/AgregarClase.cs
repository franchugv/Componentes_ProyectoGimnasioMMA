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

	protected TimePicker _tpHoraInicio;
	protected TimePicker _tpHoraFin;

	protected Picker _selectorDia;
	protected Picker _selectorEscuela;
	protected Picker _selectorDeporte;
	protected Picker _selectorProfesor;

	protected Button _botonInsertar;
	// Listas
	List<Escuela> _listaEscuelas;
	List<string> _listaNombreEscuelas;
    Escuela _escuelaElegida;


    List<Deporte> _listaDeportes;
	List<string> _listaNombreDeportes;
    Deporte _deporteElegido;


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

	private void CargarConstructor()
	{
		_api_bd = new API_BD();


		// Asignar Lista Escuelas
		_listaEscuelas = new List<Escuela>();
		_listaEscuelas = _api_bd.ObtenerEscuelasDeUsuario(_usuario.Correo);
		_listaNombreEscuelas = new List<string>();

		foreach(Escuela escuela in _listaEscuelas)
		{
			_listaNombreEscuelas.Add(escuela.Nombre);
		}

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
	}



    private void GenerarUI()
    {
        _tpHoraInicio = GeneracionUI.CrearTimePicker("tpHoraInicio", tpUnfocused);
        _tpHoraFin = GeneracionUI.CrearTimePicker("tpHoraFin", tpUnfocused);

        _selectorDia = GeneracionUI.CrearPicker("selectorDia", "Seleccione un Día de la Semana", Horario.DiasSemanaString, pUnfocused);
        _selectorEscuela = GeneracionUI.CrearPicker("selectorEscuela", "Seleccione una Escuela", _listaNombreEscuelas, pUnfocused);
        _selectorDeporte = GeneracionUI.CrearPicker("selectorDeporte", "Seleccione un Deporte para la Clase", _listaNombreDeportes, pUnfocused);
        _selectorProfesor = GeneracionUI.CrearPicker("selectorProfesor", "Seleccione un Profesor para el Deporte", _listaNombreProfesores, pUnfocused);

        _botonInsertar = GeneracionUI.CrearBoton("Insertar Clase", "bInsertar", controladorBotones);

        // Contenido del ScrollView
        VerticalStackLayout contenidoScroll = new VerticalStackLayout
        {
            Padding = new Thickness(10),
            Spacing = 8,
            Children =
        {
            new Label { Text = "Hora de Inicio", Margin = new Thickness(10, 2, 0, 2) },
            _tpHoraInicio,

            new Label { Text = "Hora de Fin", Margin = new Thickness(10, 2, 0, 2) },
            _tpHoraFin,

            _selectorDia,
            _selectorEscuela,
            _selectorDeporte,
            _selectorProfesor
        }
        };

        // Crear el ScrollView
        ScrollView scroll = new ScrollView
        {
            Content = contenidoScroll
        };

        // Crear Grid
        Grid grid = new Grid
        {
            RowDefinitions =
        {
            new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }, // Para el ScrollView
            new RowDefinition { Height = GridLength.Auto } // Para el botón
        }
        };

        // Añadir el ScrollView en la primera fila
        grid.Children.Add(scroll);
        Grid.SetRow(scroll, 0);

        // Añadir el botón en la segunda fila
        grid.Children.Add(_botonInsertar);
        Grid.SetRow(_botonInsertar, 1);

        // Asignar el Grid al MainVSL
        MainVSL = new VerticalStackLayout
        {
            Children =
        {
            grid
        }
        };
    }


    // EVENTOS

    private void controladorBotones(object sender, EventArgs e)
    {
		try
		{
            string dni = _selectorProfesor.SelectedItem.ToString().Split(", ")[0];
            for (int indice = 0; indice < _listaProfesores.Count; indice++)
            {
                if (_listaProfesores[indice].DNI == dni) _profesorElegido = _listaProfesores[indice];
            }

            Horario horario = new Horario
                (_tpHoraInicio.Time, _tpHoraFin.Time, Horario.ConvertirStringADiaSemana(_selectorDia.SelectedItem.ToString()),_profesorElegido.DNI, _deporteElegido.Id, _escuelaElegida.Id);

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

    }
    private void pUnfocused(object sender, EventArgs e)
    {

    }
}