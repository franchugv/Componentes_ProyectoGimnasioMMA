using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using Componentes_ProyectoGimnasioMMA.Componentes.GestionPersonas.Agregar;

namespace Componentes_ProyectoGimnasioMMA.GestionUsuarios;

public partial class GestionUsuarios : ContentPage
{
    // MIEMBROS

    private Usuario _usuario;
    private AgregarAlumno _viewAgregarAlumno;
    private Escuela _escuela;

    // Lista permisos en función del tipo de usuario

    // LISTA_USUARIOS_ADMINISTRADOR: Administrador, Profesor, Alumno, Trabajador, Director

    // LISTA_USUARIOS_DIRECTOR: Profesor, Alumno, Trabajador

    // LISTA_USUARIOS_PROFESOR: Alumno, Trabajador



    // Lista de opciones a realizar
    public static readonly string[] OPCIONES = { "Listar", "Agregar", "Editar", "Eliminar" };

    public GestionUsuarios(Usuario usuario, Escuela escuela)
    {
        InitializeComponent();

        _usuario = usuario;
        _escuela = escuela;

        CargarDatosConstructor();

    }

    // EVENTOS
    private void CargarDatosConstructor()
    {
        try
        {
            AsignarOpcionesPicker();
            AsignarUsuariosPicker();
        }
        catch (Exception error)
        {
            DisplayAlert("ERROR", error.Message, "OK");
        }
    }




    private void SelectedIndexChanged(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;
        try
        {

            switch (picker.StyleId)
            {
                case "PickerOpcion":
                    break;
                case "PickerUsuario":

                    switch (PickerOpcion.SelectedItem)
                    {
                        case "Agregar":
                            _viewAgregarAlumno = new AgregarAlumno(_usuario, _escuela);

                            VerticalStackLayoutUsuarios.Add(_viewAgregarAlumno);

                            break;
                    }

                    break;
            }


        }
        catch (Exception error)
        {
            DisplayAlert("ERROR", error.Message, "OK");
        }
    }



    private void AsignarOpcionesPicker()
    {
        if (PickerOpcion.ItemsSource != null) PickerOpcion.ItemsSource.Clear();

        PickerOpcion.ItemsSource = OPCIONES;



    }

    private void AsignarUsuariosPicker()
    {
        if (PickerUsuario.ItemsSource != null) PickerUsuario.ItemsSource.Clear();

        switch (_usuario.TipoDeUsuario)
        {
            case TipoUsuario.Administrador:
                PickerUsuario.ItemsSource = Usuario.LISTA_USUARIOS_ADMINISTRADOR;
                break;
            case TipoUsuario.GestorGimnasios:
                PickerUsuario.ItemsSource = Usuario.LISTA_USUARIOS_GESTOR_GIMNASIOS;
                break;
        }

    }
}