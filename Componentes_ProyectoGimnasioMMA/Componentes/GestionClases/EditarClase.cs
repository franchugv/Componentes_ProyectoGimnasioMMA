using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using BibliotecaClases_ProyectoGimnasioMMA.Deportes;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Personas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes.Funciones;

namespace Componentes_ProyectoGimnasioMMA.Componentes.GestionClases;

public class EditarClase : ContentView
{
    // RECURSOS

    protected Horario _claseEditar;
    protected VerticalStackLayout MainVSL;

    // Selectores para la fecha
    protected TimePicker _tpHoraInicio;
    protected TimePicker _tpHoraFin;
    protected PickerConfirmacion _selectorDia;

    protected PickerConfirmacion _selectorNuevoProfesor;

    protected Button _botonInsertar;

    // Listas
    List<Profesores> _listaProfesores;
    List<string> _listaNombreProfesores;
    Profesores _profesorElegido;

    API_BD _api_bd;

    Escuela _escuela;
    Usuario _usuario;

    public event Action EventoVolverPaginaPrincipal;

    // CONSTRUCTOR
    public EditarClase(Horario clase ,Escuela escuela, Usuario usuario)
	{
        _claseEditar = clase;
        _escuela = escuela;
        _usuario = usuario;

        CargarConstructor();

        Content = MainVSL;
    }

    // INICIALIZACIÓN
    private void CargarConstructor()
    {
        _api_bd = new API_BD();


    
        // Asignar Lista Profesores

        _listaProfesores = new List<Profesores>();
        _listaProfesores = _api_bd.ObtenerProfesoresPorEscuela(_escuela.Id);
        _listaNombreProfesores = new List<string>();

        foreach (Profesores profesor in _listaProfesores)
        {
            _listaNombreProfesores.Add($"{profesor.DNI}, {profesor.Nombre} {profesor.Apellidos}");
        }

        GenerarUI();
    }

    private void GenerarUI()
    {
        ScrollView scroll = new ScrollView();
        _tpHoraInicio = GeneracionUI.CrearTimePicker("tpHoraInicio", tpUnfocused);
        _tpHoraFin = GeneracionUI.CrearTimePicker("tpHoraFin", tpUnfocused);

        _selectorDia = GeneracionUI.CrearPickerConfirmacion("selectorDia", "Seleccione un nuevo Día de la Semana", Horario.DiasSemanaString, pUnfocused);
        _selectorNuevoProfesor = GeneracionUI.CrearPickerConfirmacion("selectorProfesor", "Seleccione un nuevo Profesor", _listaNombreProfesores, pUnfocused);

        _botonInsertar = GeneracionUI.CrearBoton("Actualizar Clase", "bInsertar", controladorBotones);
        _botonInsertar.BackgroundColor = Colors.Green;
        _botonInsertar.FontAttributes = FontAttributes.Bold;

        VerticalStackLayout contenidoScroll = new VerticalStackLayout()
        {
            Children =
            {
                new Label(){Text = "Hora de Inicio", Margin = new Thickness(10, 2, 2, 2)},
                _tpHoraInicio,

                 new Label(){Text = "Hora de Fin", Margin = new Thickness(10, 2, 2, 2)},
                _tpHoraFin,
                _selectorDia,
                _selectorNuevoProfesor
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

        AsignarDatos();
    }

    private void AsignarDatos()
    {

        _tpHoraInicio.Time = _claseEditar.HoraInicio;
        _tpHoraFin.Time = _claseEditar.HoraFin;
        _selectorDia.PickerEditar.SelectedItem = _claseEditar.Dia.ToString();
        _selectorNuevoProfesor.PickerEditar.SelectedItem = _claseEditar.ProfesorDni.ToString();
    }

    // VALIDACIÓN

    private void validarTimePickers()
    {
        if (_tpHoraInicio == null || _tpHoraFin == null)
            throw new Exception("Los selesctores de hora deben estar inicializados");

        DateTime inicio = DateTime.Today.Add(_tpHoraInicio.Time);
        DateTime fin = DateTime.Today.Add(_tpHoraFin.Time);

        if (fin <= inicio)
            fin = fin.AddDays(1); // Sumamos un día si la hora de fin es técnicamente después de medianoche

        TimeSpan duracion = fin - inicio;

        if (duracion.TotalMinutes <= 0 || duracion.TotalHours > 12)
            throw new Exception("Duración inválida. La clase no puede durar más de 12 horas.");

    }

    // EVENTOS

    private void controladorBotones(object sender, EventArgs e)
    {
        try
        {
            // validarTimePickers();

            // Asinar el profesor
            if (_selectorNuevoProfesor == null || _selectorNuevoProfesor.PickerEditar.SelectedItem == null) throw new Exception("Debe seleccionar un Profesor");

            string dni = _selectorNuevoProfesor.PickerEditar.SelectedItem.ToString().Split(", ")[0];
            for (int indice = 0; indice < _listaProfesores.Count; indice++)
            {
                if (_listaProfesores[indice].DNI == dni) _profesorElegido = _listaProfesores[indice];
            }

         

            Horario horario = new Horario
                (_tpHoraInicio.Time, _tpHoraFin.Time, Horario.ConvertirStringADiaSemana(_selectorDia.PickerEditar.SelectedItem.ToString()), _profesorElegido.DNI, _claseEditar.DeporteId, _claseEditar.IdEscuela);

            // Hacer Update
            _api_bd.ActualizarClase(_claseEditar ,horario);
            

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
                    // Se genera un bucle infinito, colocar en el evento del botón
                    //validarTimePickers();

                    break;


            }
        }
        catch (Exception error)
        {
            Application.Current.MainPage.DisplayAlert("ERROR", error.Message, "Ok");
        }
    }
    private void pUnfocused(object sender, EventArgs e)
    {

    }
}