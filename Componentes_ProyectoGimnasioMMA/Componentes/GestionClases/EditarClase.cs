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
    public EditarClase(Horario clase ,Escuela escuela, Usuario usuario)
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

        foreach (Escuela escuela in _listaEscuelas)
        {
            _listaNombreEscuelas.Add(escuela.Nombre);
        }

        // Asignar Lista Deportes
        _listaDeportes = new List<Deporte>();
        _listaDeportes = _api_bd.DevolverListaDeportes(_escuela.Id);
        _listaNombreDeportes = new List<string>();

        foreach (Deporte deporte in _listaDeportes)
        {
            _listaNombreDeportes.Add(deporte.Nombre);
        }

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

        _selectorDia = GeneracionUI.CrearPicker("selectorDia", "Seleccione un Día de la Semana", Horario.DiasSemanaString, pUnfocused);
        _selectorEscuela = GeneracionUI.CrearPicker("selectorEscuela", "Seleccione una Escuela", _listaNombreEscuelas, pUnfocused);
        _selectorDeporte = GeneracionUI.CrearPicker("selectorDia", "Seleccione un Deporte para la Clase", _listaNombreDeportes, pUnfocused);
        _selectorProfesor = GeneracionUI.CrearPicker("selectorProfesor", "Seleccione un Profesor para el Deporte", _listaNombreProfesores, pUnfocused);

        _botonInsertar = GeneracionUI.CrearBoton("Insertar Clase", "bInsertar", controladorBotones);
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
                _selectorEscuela,
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
            if (_selectorEscuela == null || _selectorEscuela.SelectedItem == null) throw new Exception("Debe seleccionar una Escuela");

            string escuelaNombre = _selectorEscuela.SelectedItem.ToString();
            for (int indice = 0; indice < _listaEscuelas.Count; indice++)
            {
                if (_listaDeportes[indice].Nombre == nombreDeporte) _escuelaElegida = _listaEscuelas[indice];
            }

            Horario horario = new Horario
                (_tpHoraInicio.Time, _tpHoraFin.Time, Horario.ConvertirStringADiaSemana(_selectorDia.SelectedItem.ToString()), _profesorElegido.DNI, _deporteElegido.Id, _escuelaElegida.Id);

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