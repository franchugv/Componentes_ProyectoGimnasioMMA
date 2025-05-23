using BibliotecaClases_ProyectoGimnasioMMA.APIs;
using BibliotecaClases_ProyectoGimnasioMMA.Deportes;
using BibliotecaClases_ProyectoGimnasioMMA.Escuelas;
using BibliotecaClases_ProyectoGimnasioMMA.Personas;
using BibliotecaClases_ProyectoGimnasioMMA.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Componentes_ProyectoGimnasioMMA.Componentes.Funciones
{
    public static class GeneracionUI
    {


        #region Crear Entry Personalizados
        public static EntryValidacion CrearEntryError(string texto, string styleId, int numCarateres, EventHandler<FocusEventArgs> evento)
        {
            EntryValidacion entry = new EntryValidacion(texto);
            entry.EntryEditar.StyleId = styleId;

            entry.EntryEditar.BackgroundColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.Black : Colors.White;
            entry.EntryEditar.TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.White : Colors.Black;
            entry.EntryEditar.PlaceholderColor = Colors.Gray;
            entry.EntryEditar.Margin = new Thickness(10);

            // Tamaño fijo
            entry.EntryEditar.WidthRequest = 300;
            entry.EntryEditar.HeightRequest = 50;

            entry.EntryEditar.MaxLength = numCarateres;

            entry.EntryEditar.Unfocused += evento;

            return entry;
        }

        public static EntryConfirmacion CrearEntryConfirmacion(string texto, string styleId, int numCarateres, EventHandler<FocusEventArgs> evento)
        {
            EntryConfirmacion entry = new EntryConfirmacion(texto);
            entry.EntryEditar.StyleId = styleId;

            entry.EntryEditar.BackgroundColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.Black : Colors.White;
            entry.EntryEditar.TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.White : Colors.Black;
            entry.EntryEditar.PlaceholderColor = Colors.Gray;
            entry.EntryEditar.Margin = new Thickness(10);

            // Tamaño fijo
            entry.EntryEditar.WidthRequest = 300;
            entry.EntryEditar.HeightRequest = 50;
            entry.EntryEditar.MaxLength = numCarateres;
            entry.EntryEditar.Unfocused += evento;

            return entry;
        }

        #endregion

        #region Crear Botones Personalizados
        public static Button CrearBoton(string texto, string styleId, EventHandler? evento = null)
        {
            Button boton = new Button()
            {
                Text = texto,
                StyleId = styleId,
                BackgroundColor = Application.Current.RequestedTheme == AppTheme.Dark ? Color.FromArgb("#3700B3") : Color.FromArgb("#6200EE"),
                TextColor = Colors.White,
                FontSize = 16,
                CornerRadius = 10,
                Margin = new Thickness(10),
                Padding = new Thickness(10)
            };
            if (evento != null) boton.Clicked += evento;

            return boton;
        }
        #endregion

        #region Crear Pickers Personalizados
        public static Picker CrearPicker(string styleId, string titulo, List<string> items, EventHandler evento)
        {
            if(items == null) items = new List<string>();

            Picker picker = new Picker
            {
                StyleId = styleId,
                BackgroundColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.Black : Colors.White,
                TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.White : Colors.Black,
                Title = titulo,
                Margin = new Thickness(10),
                HorizontalOptions = LayoutOptions.Start,

                // Tamaño fijo
                WidthRequest = 300,
            };

            foreach (var item in items)
            {
                picker.Items.Add(item);
            }

            picker.SelectedIndexChanged += evento;

            if (items.Count <= 0) picker.IsEnabled = false;

            return picker;
        }

        public static PickerConfirmacion CrearPickerConfirmacion(string styleId, string titulo, List<string> items, EventHandler evento)
        {
            PickerConfirmacion picker = new PickerConfirmacion(titulo);

            picker.PickerEditar.StyleId = styleId;
            picker.PickerEditar.BackgroundColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.Black : Colors.White;
            picker.PickerEditar.TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.White : Colors.Black;
            picker.PickerEditar.Title = titulo;
            picker.PickerEditar.Margin = new Thickness(10);

            // Tamaño fijo
            picker.PickerEditar.WidthRequest = 300;

            foreach (var item in items)
            {
                picker.PickerEditar.Items.Add(item);
            }

            picker.PickerEditar.SelectedIndexChanged += evento;

            if(items.Count <= 0) picker.PickerEditar.IsEnabled = false;

            return picker;
        }

        #endregion

        #region Crear Time Pickers Personalizados

        public static TimePicker CrearTimePicker(string styleId, EventHandler<FocusEventArgs> eventoUnfocused)
        {
            TimePicker timePicker = new TimePicker
            {
                StyleId = styleId,
                BackgroundColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.Black : Colors.White,
                TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.White : Colors.Black,
                Margin = new Thickness(10),
                Format = "HH:mm" // Formato de 24 horas
            };

            timePicker.Unfocused += eventoUnfocused;

            return timePicker;
        }


        #endregion
        #region Crear Cards Personalizados


        public static Label CrearLabelPantallas(string texto, bool esTitulo = false)
        {
            return new Label
            {
                Text = texto,
                FontSize = esTitulo ? 20 : Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                FontAttributes = esTitulo ? FontAttributes.Bold : FontAttributes.None,
                HorizontalOptions = esTitulo ? LayoutOptions.Center : LayoutOptions.Start
            };
        }


        public static Label CrearLabel(string texto, int size, Color colorClaro, Color colorOscuro)
        {
            Label label = null;
            if (!string.IsNullOrEmpty(texto))
            {
                label = new Label
                {
                    Text = texto,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = size,
                    TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? colorClaro : colorOscuro
                };
            }
            else
            {
                label = new Label { Text = "Sin datos" };
            }
           

            return label;
        }
        public static Frame CrearCarta()
        {

            Frame carta = new Frame
            {
                CornerRadius = 20,
                BorderColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.DarkGray : Colors.LightGray,
                BackgroundColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.Black : Color.FromArgb("#F5F5F5"),
                Margin = new Thickness(16),
                Padding = new Thickness(24),
                Shadow = new Shadow
                {
                    Brush = new SolidColorBrush(Application.Current.RequestedTheme == AppTheme.Dark ? Colors.White : Colors.Black),
                    Offset = new Point(2, 2),
                    Opacity = Application.Current.RequestedTheme == AppTheme.Dark ? 0.3f : 0.15f,
                    Radius = 6
                },
                Content = new VerticalStackLayout
                {
                    Spacing = 10
                }
            };

            return carta;
        }
        public static Frame CrearCartaMensaje(string mensaje)
        {
            Frame carta = CrearCarta();

            carta.Content = new VerticalStackLayout
            {
                Spacing = 10,
                Children =
            {
                new Label
                {
                    Text = mensaje,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 22,
                    TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.LightBlue : Color.FromArgb("#1E88E5")
                }
                }
            };


            return carta;
        }

        // ESCUELAS
        public static Frame CrearCartaEscuela(Escuela escuela, EventHandler<TappedEventArgs> evento)
        {
            Frame carta = CrearCarta();

            carta.Content = new VerticalStackLayout
            {
                CrearLabel(escuela.Id.ToString(), 16,  Colors.LightGray, Colors.Gray),
                CrearLabel(escuela.Nombre, 16, Colors.LightGray, Colors.Gray),
                CrearLabel(escuela.Ubicacion, 22, Colors.LightBlue, Color.FromArgb("#1E88E5")),
            };

            // Agregar el gesto de toque (tap)
            TapGestureRecognizer tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += evento; // Se asigna el evento recibido como parámetro

            carta.GestureRecognizers.Add(tapGesture);

            return carta;
        }
        public static Frame CrearCartaEscuelaGestor(Escuela escuela, EventHandler<TappedEventArgs> evento, EventHandler editar, EventHandler eliminar, bool generarBotones)
        {
            // Crear el gesto de toque (tap)
            TapGestureRecognizer tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += evento; // Se asigna el evento recibido como parámetro

            // Crear la lista de elementos de la carta
            var elementos = new List<View>
    {
        CrearLabel(escuela.Id.ToString(), 16,  Colors.LightGray, Colors.Gray),
        CrearLabel(escuela.Nombre, 16, Colors.LightGray, Colors.Gray),
        CrearLabel(escuela.Ubicacion, 22, Colors.LightBlue, Color.FromArgb("#1E88E5")),

    };

            // Si se deben generar los botones, los agregamos a la lista
            if (generarBotones)
            {
                Button btnEditar = new Button
                {
                    Text = "Editar",
                    BackgroundColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.MidnightBlue : Colors.Blue,
                    TextColor = Colors.White,
                    FontSize = 16,
                    StyleId = "btnEditar"
                };
                btnEditar.Clicked += (sender, args) =>
                {
                    evento.Invoke(sender, new TappedEventArgs(null)); // Disparar evento Tapped
                    editar.Invoke(sender, args); // Luego ejecutar el evento de edición
                };

                Button btnEliminar = new Button
                {
                    Text = "Eliminar",
                    BackgroundColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.DarkRed : Colors.Red,
                    TextColor = Colors.White,
                    FontSize = 16,
                    StyleId = "btnEliminar"
                };
                btnEliminar.Clicked += (sender, args) =>
                {
                    evento.Invoke(sender, new TappedEventArgs(null)); // Disparar evento Tapped
                    eliminar.Invoke(sender, args); // Luego ejecutar el evento de eliminación
                };

                elementos.Add(btnEditar);
                elementos.Add(btnEliminar);
            }

            // Crear el Frame (carta)
            Frame carta = CrearCarta();

            // Agregar los elementos a la carta
            foreach (var elemento in elementos)
            {
                ((VerticalStackLayout)carta.Content).Children.Add(elemento);
            }

            // Agregar el gesto de toque (tap)
            carta.GestureRecognizers.Add(tapGesture);

            return carta;
        }

        // USUARIOS
        public static Frame CrearCartaUsuario(Usuario usuario, EventHandler<TappedEventArgs> evento)
        {
            Frame carta = CrearCarta();

            carta.Content = new VerticalStackLayout
            {
                Spacing = 10,
                Children =
            {

                CrearLabel(usuario.Correo, 16, Colors.LightGray, Colors.Gray),
                CrearLabel(usuario.Nombre, 16, Colors.LightGray, Colors.Gray),
                CrearLabel(usuario.OntenerTipoUsuario, 16, Colors.OrangeRed, Colors.Red),
            }
            };

            // Agregar el gesto de toque (tap)
            TapGestureRecognizer tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += evento; // Se asigna el evento recibido como parámetro

            carta.GestureRecognizers.Add(tapGesture);

            return carta;
        }
        public static Frame CrearCartaUsuarioGestor(Usuario usuario, EventHandler<TappedEventArgs> evento, EventHandler editar, EventHandler eliminar, bool mostrarEditar, bool mostrarEliminar)
        {
            // Crear el gesto de toque (tap)
            TapGestureRecognizer tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += evento;

            // Crear la lista de elementos de la carta
            var elementos = new List<View>
    {
                CrearLabel(usuario.Correo, 16, Colors.LightGray, Colors.Gray),
                CrearLabel(usuario.Nombre, 16, Colors.LightGray, Colors.Gray),
                CrearLabel(usuario.OntenerTipoUsuario, 16, Colors.OrangeRed, Colors.Red),
    };

            // Botón Editar si se indica
            if (mostrarEditar)
            {
                Button btnEditar = new Button
                {
                    Text = "Editar",
                    BackgroundColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.MidnightBlue : Colors.Blue,
                    TextColor = Colors.White,
                    FontSize = 16,
                    StyleId = "btnEditar"
                };
                btnEditar.Clicked += (sender, args) =>
                {
                    evento.Invoke(sender, new TappedEventArgs(null));
                    editar.Invoke(sender, args);
                };
                elementos.Add(btnEditar);
            }

            // Botón Eliminar si se indica
            if (mostrarEliminar)
            {
                Button btnEliminar = new Button
                {
                    Text = "Eliminar",
                    BackgroundColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.DarkRed : Colors.Red,
                    TextColor = Colors.White,
                    FontSize = 16,
                    StyleId = "btnEliminar"
                };
                btnEliminar.Clicked += (sender, args) =>
                {
                    evento.Invoke(sender, new TappedEventArgs(null));
                    eliminar.Invoke(sender, args);
                };
                elementos.Add(btnEliminar);
            }

            // Crear el Frame
            Frame carta = CrearCarta();

            // Agregar los elementos al contenido
            foreach (var elemento in elementos)
            {
                ((VerticalStackLayout)carta.Content).Children.Add(elemento);
            }

            // Agregar el gesto de tap
            carta.GestureRecognizers.Add(tapGesture);

            return carta;
        }

        // ALUMNOS
        public static Frame CrearCartaAlumnoGestor(Alumno alumno, EventHandler<TappedEventArgs> evento, EventHandler editar, EventHandler eliminar, bool generarBotones)
        {
            // Crear el gesto de toque (tap)
            TapGestureRecognizer tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += evento;

            // Crear la lista de elementos de la carta
            var elementos = new List<View>
    {
        CrearLabel(alumno.DNI, 16, Colors.LightGray,  Colors.Gray),
        CrearLabel(alumno.Nombre, 16, Colors.LightGray,  Colors.Gray),
        CrearLabel(alumno.Apellidos, 16, Colors.LightGray,  Colors.Gray),

    };

            // Si se deben generar los botones, los agregamos a la lista
            if (generarBotones)
            {
                Button btnEditar = new Button
                {
                    Text = "Editar",
                    BackgroundColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.MidnightBlue : Colors.Blue,
                    TextColor = Colors.White,
                    FontSize = 16,
                    StyleId = "btnEditar"
                };
                btnEditar.Clicked += (sender, args) =>
                {
                    evento.Invoke(sender, new TappedEventArgs(null)); // Disparar evento Tapped
                    editar.Invoke(sender, args); // Ejecutar el evento de edición
                };

                Button btnEliminar = new Button
                {
                    Text = "Eliminar",
                    BackgroundColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.DarkRed : Colors.Red,
                    TextColor = Colors.White,
                    FontSize = 16,
                    StyleId = "btnEliminar"
                };
                btnEliminar.Clicked += (sender, args) =>
                {
                    evento.Invoke(sender, new TappedEventArgs(null)); // Disparar evento Tapped
                    eliminar.Invoke(sender, args); // Ejecutar el evento de eliminación
                };

                elementos.Add(btnEditar);
                elementos.Add(btnEliminar);
            }

            // Crear el Frame (carta)
            Frame carta = CrearCarta();

            // Agregar los elementos a la carta
            foreach (var elemento in elementos)
            {
                ((VerticalStackLayout)carta.Content).Children.Add(elemento);
            }

            // Agregar el gesto de toque (tap)
            carta.GestureRecognizers.Add(tapGesture);

            return carta;
        }
        public static Frame CrearCartaAlumnoBotonIndividual(Alumno alumno, EventHandler<TappedEventArgs> evento, Action<Alumno, bool> seleccionCambioCallback, Color colorNormal, Color colorSeleccionado, string texto, string textoAlternativo)
        {
            TapGestureRecognizer tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += evento;

            var elementos = new List<View>
            {
                CrearLabel(alumno.DNI, 16, Colors.LightGray,  Colors.Gray),
                CrearLabel(alumno.Nombre, 16, Colors.LightGray,  Colors.Gray),
                CrearLabel(alumno.Apellidos, 16, Colors.LightGray,  Colors.Gray),
            };

            var botonSelector = new BotonSelector(texto, textoAlternativo, colorNormal, colorSeleccionado);
            botonSelector.EstadoCambiado += (s, estaSeleccionado) =>
            {
                seleccionCambioCallback?.Invoke(alumno, estaSeleccionado);
            };

            elementos.Add(botonSelector);

            Frame carta = CrearCarta();

            foreach (var elemento in elementos)
            {
                ((VerticalStackLayout)carta.Content).Children.Add(elemento);
            }

            carta.GestureRecognizers.Add(tapGesture);

            return carta;
        }
        public static Frame CrearCartaAlumno(Alumno alumno, EventHandler<TappedEventArgs> evento)
        {
            Frame carta = CrearCarta();

            carta.Content = new VerticalStackLayout()
            {
                CrearLabel(alumno.DNI, 16, Colors.LightGray,  Colors.Gray),
                CrearLabel(alumno.Nombre, 16, Colors.LightGray,  Colors.Gray),
                CrearLabel(alumno.Apellidos, 16, Colors.LightGray,  Colors.Gray),
            };

            TapGestureRecognizer tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += evento;
            carta.GestureRecognizers.Add(tapGesture);

            return carta;
        }

        // PROFESORES
        public static Frame CrearCartaProfesor(Profesores profesor, EventHandler<TappedEventArgs> evento)
        {
            Frame carta = CrearCarta();

            carta.Content = new VerticalStackLayout()
            {
                CrearLabel(profesor.DNI, 16, Colors.LightGray, Colors.Gray),
                CrearLabel(profesor.Nombre, 16, Colors.LightGray, Colors.Gray),
                CrearLabel(profesor.Apellidos, 16, Colors.LightGray, Colors.Gray),
                CrearLabel(profesor.Deporte.Nombre, 16, Colors.LightGray, Colors.Gray)

            };

            TapGestureRecognizer tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += evento;
            carta.GestureRecognizers.Add(tapGesture);

            return carta;
        }
        public static Frame CrearCartaProfesorGestor(Profesores profesor, EventHandler<TappedEventArgs> evento, EventHandler editar, EventHandler eliminar, bool generarBotones)
        {
            // Recursos
            string nombreDeporte = "";
            // Crear el gesto de toque (tap)
            TapGestureRecognizer tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += evento;
            
            // Validar que deporte no sea null
            if(profesor.Deporte != null)
            {
                nombreDeporte = "Deporte: "+profesor.Deporte.Nombre;
            }
            else
            {
                nombreDeporte = "Sin Deporte Asignado";
            }

            // Crear la lista de elementos de la carta

            List<View> elementos = new List<View>
            {
                CrearLabel(profesor.DNI, 16, Colors.LightGray, Colors.Gray),
                CrearLabel(profesor.Nombre, 16, Colors.LightGray, Colors.Gray),
                CrearLabel(profesor.Apellidos, 16, Colors.LightGray, Colors.Gray),
                CrearLabel(nombreDeporte, 16, Colors.LightGray, Colors.Gray)

            };

            // Si se deben generar los botones, los agregamos a la lista
            if (generarBotones)
            {
                Button btnEditar = new Button
                {
                    Text = "Editar",
                    BackgroundColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.MidnightBlue : Colors.Blue,
                    TextColor = Colors.White,
                    FontSize = 16,
                    StyleId = "btnEditar"
                };
                btnEditar.Clicked += (sender, args) =>
                {
                    evento.Invoke(sender, new TappedEventArgs(null)); // Disparar evento Tapped
                    editar.Invoke(sender, args); // Ejecutar el evento de edición
                };

                Button btnEliminar = new Button
                {
                    Text = "Eliminar",
                    BackgroundColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.DarkRed : Colors.Red,
                    TextColor = Colors.White,
                    FontSize = 16,
                    StyleId = "btnEliminar"
                };
                btnEliminar.Clicked += (sender, args) =>
                {
                    evento.Invoke(sender, new TappedEventArgs(null)); // Disparar evento Tapped
                    eliminar.Invoke(sender, args); // Ejecutar el evento de eliminación
                };

                elementos.Add(btnEditar);
                elementos.Add(btnEliminar);
            }

            // Crear el Frame (carta)
            Frame carta = CrearCarta();

            // Agregar cada elemento a Children individualmente
            foreach (var elemento in elementos)
            {
                ((VerticalStackLayout)carta.Content).Children.Add(elemento);
            }

            // Agregar el gesto de toque (tap)
            carta.GestureRecognizers.Add(tapGesture);

            return carta;
        }

        // DEPORTE
        public static Frame CrearCartaDeporteGestor(Deporte deporte, EventHandler<TappedEventArgs> evento, EventHandler editar, EventHandler eliminar)
        {
            // Crear el gesto de toque (tap)
            TapGestureRecognizer tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += evento;

            // Crear la lista de elementos
            var elementos = new List<View>
    {
        CrearLabel(deporte.Id.ToString(), 16, Colors.LightGray, Colors.Gray),
        CrearLabel(deporte.Nombre, 16, Colors.LightGray, Colors.Gray),
        CrearLabel(deporte.Federacion, 16, Colors.OrangeRed, Colors.Red)

    };

            Button btnEditar = new Button
            {
                Text = "Editar",
                BackgroundColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.MidnightBlue : Colors.Blue,
                TextColor = Colors.White,
                FontSize = 16,
                StyleId = "btnEditar"
            };
            btnEditar.Clicked += (sender, args) =>
            {
                evento.Invoke(sender, new TappedEventArgs(null));
                editar.Invoke(sender, args);
            };

            Button btnEliminar = new Button
            {
                Text = "Eliminar",
                BackgroundColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.DarkRed : Colors.Red,
                TextColor = Colors.White,
                FontSize = 16,
                StyleId = "btnEliminar"
            };
            btnEliminar.Clicked += (sender, args) =>
            {
                evento.Invoke(sender, new TappedEventArgs(null));
                eliminar.Invoke(sender, args);
            };

            elementos.Add(btnEditar);
            elementos.Add(btnEliminar);

            // Crear el Frame
            Frame carta = CrearCarta();

            // Agregar cada elemento a Children individualmente
            foreach (var elemento in elementos)
            {
                ((VerticalStackLayout)carta.Content).Children.Add(elemento);
            }

            carta.GestureRecognizers.Add(tapGesture);

            return carta;
        }

        // CLASES
        public static Frame CrearCartaClases(Horario horario, EventHandler<TappedEventArgs> evento, EventHandler editar, EventHandler eliminar, EventHandler gestionarAlumnos)
        {
            API_BD api_bd = new API_BD();
            // Crear el gesto de toque (tap)
            TapGestureRecognizer tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += evento;

            // Crear la lista de elementos
            List <View> elementos = new List<View>
    {
        CrearLabel(api_bd.ObtenerDeportePorId(horario.DeporteId).Nombre, 16, Colors.LightGray, Colors.Gray),
        CrearLabel(api_bd.ObtenerProfesorPorDni(horario.ProfesorDni).Nombre, 16, Colors.LightGray, Colors.Gray),
        CrearLabel("Día: " + horario.Dia.ToString(), 16, Colors.LightGray, Colors.Gray),
        CrearLabel("Hora Inicio: " + horario.HoraInicio.ToString(), 16, Colors.OrangeRed, Colors.Red),
        CrearLabel("Hora Fin: " +horario.HoraFin.ToString(), 16, Colors.OrangeRed, Colors.Red),


    };

            Button btnEditar = new Button
            {
                Text = "Editar",
                BackgroundColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.MidnightBlue : Colors.Blue,
                TextColor = Colors.White,
                FontSize = 16,
                StyleId = "btnEditar"
            };
            btnEditar.Clicked += (sender, args) =>
            {
                evento.Invoke(sender, new TappedEventArgs(null));
                editar.Invoke(sender, args);
            };

            Button btnEliminar = new Button
            {
                Text = "Eliminar",
                BackgroundColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.DarkRed : Colors.Red,
                TextColor = Colors.White,
                FontSize = 16,
                StyleId = "btnEliminar"
            };
            btnEliminar.Clicked += (sender, args) =>
            {
                evento.Invoke(sender, new TappedEventArgs(null));
                eliminar.Invoke(sender, args);
            };
            Button btnGestionarAlumno = new Button
            {
                Text = "Gestionar Alumnos",
                BackgroundColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.Purple : Colors.Purple,
                TextColor = Colors.White,
                FontSize = 16,
                StyleId = "btnGestionarAlumno"
            };
            btnGestionarAlumno.Clicked += (sender, args) =>
            {
                evento.Invoke(sender, new TappedEventArgs(null));
                eliminar.Invoke(sender, args);
            };


            elementos.Add(btnEditar);
            elementos.Add(btnGestionarAlumno);
            elementos.Add(btnEliminar);

            // Crear el Frame
            Frame carta = CrearCarta();

            // Agregar cada elemento a Children individualmente
            foreach (var elemento in elementos)
            {
                ((VerticalStackLayout)carta.Content).Children.Add(elemento);
            }

            carta.GestureRecognizers.Add(tapGesture);

            return carta;
        }
        public static Frame CrearCartaClasesProfesor(Horario horario, EventHandler<TappedEventArgs> evento, EventHandler eventoBoton, string textoBoton, Color colorBoton)
        {
            API_BD api_bd = new API_BD();
            // Crear el gesto de toque (tap)
            TapGestureRecognizer tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += evento;

            // Crear la lista de elementos
            List<View> elementos = new List<View>
    {
        CrearLabel(api_bd.ObtenerDeportePorId(horario.DeporteId).Nombre, 16, Colors.LightGray, Colors.Gray),
        CrearLabel(api_bd.ObtenerProfesorPorDni(horario.ProfesorDni).Nombre, 16, Colors.LightGray, Colors.Gray),
        CrearLabel("Día: " + horario.Dia.ToString(), 16, Colors.LightGray, Colors.Gray),
        CrearLabel("Hora Inicio: " + horario.HoraInicio.ToString(), 16, Colors.OrangeRed, Colors.Red),
        CrearLabel("Hora Fin: " +horario.HoraFin.ToString(), 16, Colors.OrangeRed, Colors.Red),


    };

            Button Boton = new Button
            {
                Text = textoBoton,
                BackgroundColor = colorBoton,
                TextColor = Colors.White,
                FontSize = 16,
                StyleId = "Boton"
            };
            Boton.Clicked += (sender, args) =>
            {
                evento.Invoke(sender, new TappedEventArgs(null));
                eventoBoton.Invoke(sender, args);
            };

          


            elementos.Add(Boton);

            // Crear el Frame
            Frame carta = CrearCarta();

            // Agregar cada elemento a Children individualmente
            foreach (var elemento in elementos)
            {
                ((VerticalStackLayout)carta.Content).Children.Add(elemento);
            }

            carta.GestureRecognizers.Add(tapGesture);

            return carta;
        }
        #endregion




        public static async Task<bool> MostrarConfirmacion(Page page, string titulo, string mensaje)
        {
            bool respuesta = await page.DisplayAlert(titulo, mensaje, "Sí", "No");
            return respuesta;
        }
    }
}
