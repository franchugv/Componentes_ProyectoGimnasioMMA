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
        public static EntryValidacion CrearEntryError(string texto, string styleId, EventHandler<FocusEventArgs> evento)
        {
            EntryValidacion entry = new EntryValidacion(texto);
            entry.EntryEditar.StyleId = styleId;

            entry.EntryEditar.BackgroundColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.Black : Colors.White;
            entry.EntryEditar.TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.White : Colors.Black;
            entry.EntryEditar.PlaceholderColor = Colors.Gray;
            entry.EntryEditar.Margin = new Thickness(10);

            entry.EntryEditar.Unfocused += evento;

            return entry;
        }

        public static EntryConfirmacion CrearEntryConfirmacion(string texto, string styleId, EventHandler<FocusEventArgs> evento)
        {
            EntryConfirmacion entry = new EntryConfirmacion(texto);
            entry.EntryEditar.StyleId = styleId;

            entry.EntryEditar.BackgroundColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.Black : Colors.White;
            entry.EntryEditar.TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.White : Colors.Black;
            entry.EntryEditar.PlaceholderColor = Colors.Gray;
            entry.EntryEditar.Margin = new Thickness(10);

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
            Picker picker = new Picker
            {
                StyleId = styleId,
                BackgroundColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.Black : Colors.White,
                TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.White : Colors.Black,
                Title = titulo,
                Margin = new Thickness(10)
            };

            foreach (var item in items)
            {
                picker.Items.Add(item);
            }

            picker.SelectedIndexChanged += evento;

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

            foreach (var item in items)
            {
                picker.PickerEditar.Items.Add(item);
            }

            picker.PickerEditar.SelectedIndexChanged += evento;

            return picker;
        }
        #endregion

        #region Crear Cards Personalizados


        public static Frame CrearCartaEscuela(Escuela escuela, EventHandler<TappedEventArgs> evento)
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
                    Spacing = 10,
                    Children =
            {
                new Label
                {
                    Text = escuela.Id.ToString(),
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 16,
                    TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.LightGray : Colors.Gray
                },
                new Label
                {
                    Text = escuela.Nombre,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 16,
                    TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.LightGray : Colors.Gray
                },
                new Label
                {
                    Text = escuela.Ubicacion,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 22,
                    TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.LightBlue : Color.FromArgb("#1E88E5")
                }
            }
                }
            };

            // Agregar el gesto de toque (tap)
            TapGestureRecognizer tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += evento; // Se asigna el evento recibido como parámetro

            carta.GestureRecognizers.Add(tapGesture);

            return carta;
        }


        public static Frame CrearCartaMensaje(string mensaje)
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
                }
            };

            return carta;
        }


        public static Frame CrearCartaUsuario(Usuario usuario, EventHandler<TappedEventArgs> evento)
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
                    Spacing = 10,
                    Children =
            {
                new Label
                {
                    Text = usuario.Correo,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 16,
                    TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.LightGray : Colors.Gray
                },
                new Label
                {
                    Text = usuario.Nombre,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 16,
                    TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.LightGray : Colors.Gray
                },
                new Label
                {
                    Text = usuario.OntenerTipoUsuario,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 16,
                    TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.OrangeRed : Colors.Red
                }
            }
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
        new Label
        {
            Text = usuario.Correo,
            FontAttributes = FontAttributes.Bold,
            FontSize = 16,
            TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.LightGray : Colors.Gray
        },
        new Label
        {
            Text = usuario.Nombre,
            FontAttributes = FontAttributes.Bold,
            FontSize = 16,
            TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.LightGray : Colors.Gray
        },
        new Label
        {
            Text = usuario.OntenerTipoUsuario,
            FontAttributes = FontAttributes.Bold,
            FontSize = 16,
            TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.OrangeRed : Colors.Red
        }
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
                    Spacing = 10,
                    Children = { }
                }
            };

            // Agregar los elementos al contenido
            foreach (var elemento in elementos)
            {
                ((VerticalStackLayout)carta.Content).Children.Add(elemento);
            }

            // Agregar el gesto de tap
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
        new Label
        {
            Text = escuela.Id.ToString(),
            FontAttributes = FontAttributes.Bold,
            FontSize = 16,
            TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.LightGray : Colors.Gray
        },
        new Label
        {
            Text = escuela.Nombre,
            FontAttributes = FontAttributes.Bold,
            FontSize = 16,
            TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.LightGray : Colors.Gray
        },
        new Label
        {
            Text = escuela.Ubicacion,
            FontAttributes = FontAttributes.Bold,
            FontSize = 16,
            TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.OrangeRed : Colors.Red
        }
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
                    Spacing = 10,
                    Children = { } // Se inicializa vacío
                }
            };

            // Agregar los elementos a la carta
            foreach (var elemento in elementos)
            {
                ((VerticalStackLayout)carta.Content).Children.Add(elemento);
            }

            // Agregar el gesto de toque (tap)
            carta.GestureRecognizers.Add(tapGesture);

            return carta;
        }

        public static Frame CrearCartaAlumnoGestor(Alumno alumno, EventHandler<TappedEventArgs> evento, EventHandler editar, EventHandler eliminar, bool generarBotones)
        {
            // Crear el gesto de toque (tap)
            TapGestureRecognizer tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += evento;

            // Crear la lista de elementos de la carta
            var elementos = new List<View>
    {
        new Label
        {
            Text = alumno.DNI,
            FontAttributes = FontAttributes.Bold,
            FontSize = 16,
            TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.LightGray : Colors.Gray
        },
        new Label
        {
            Text = alumno.Nombre,
            FontAttributes = FontAttributes.Bold,
            FontSize = 16,
            TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.LightGray : Colors.Gray
        },
        new Label
        {
            Text = alumno.Apellidos,
            FontAttributes = FontAttributes.Bold,
            FontSize = 16,
            TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.LightGray : Colors.Gray
        }
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
                    Spacing = 10,
                    Children = { } // Se inicializa vacío
                }
            };

            // Agregar los elementos a la carta
            foreach (var elemento in elementos)
            {
                ((VerticalStackLayout)carta.Content).Children.Add(elemento);
            }

            // Agregar el gesto de toque (tap)
            carta.GestureRecognizers.Add(tapGesture);

            return carta;
        }


        public static Frame CrearCartaAlumno(Alumno alumno, EventHandler<TappedEventArgs> evento)
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
                    Spacing = 10,
                    Children =
            {
                new Label
                {
                    Text = alumno.DNI,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 16,
                    TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.LightGray : Colors.Gray
                },
                new Label
                {
                    Text = alumno.Nombre,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 16,
                    TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.LightGray : Colors.Gray
                },
                new Label
                {
                    Text = alumno.Apellidos,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 16,
                    TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.LightGray : Colors.Gray
                }
            }
                }
            };

            TapGestureRecognizer tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += evento;
            carta.GestureRecognizers.Add(tapGesture);

            return carta;
        }
        public static Frame CrearCartaProfesor(Profesores profesor, EventHandler<TappedEventArgs> evento)
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
                    Spacing = 10,
                    Children =
            {
                new Label
                {
                    Text = profesor.DNI,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 16,
                    TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.LightGray : Colors.Gray
                },
                new Label
                {
                    Text = profesor.Nombre,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 16,
                    TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.LightGray : Colors.Gray
                },
                new Label
                {
                    Text = profesor.Apellidos,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 16,
                    TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.LightGray : Colors.Gray
                }
            }
                }
            };

            TapGestureRecognizer tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += evento;
            carta.GestureRecognizers.Add(tapGesture);

            return carta;
        }

        public static Frame CrearCartaDeporteGestor(Deporte deporte, EventHandler<TappedEventArgs> evento, EventHandler editar, EventHandler eliminar)
        {
            // Crear el gesto de toque (tap)
            TapGestureRecognizer tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += evento;

            // Crear la lista de elementos
            var elementos = new List<View>
    {
        new Label
        {
            Text = deporte.Id.ToString(),
            FontAttributes = FontAttributes.Bold,
            FontSize = 16,
            TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.LightGray : Colors.Gray
        },
        new Label
        {
            Text = deporte.Nombre,
            FontAttributes = FontAttributes.Bold,
            FontSize = 16,
            TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.LightGray : Colors.Gray
        },
        new Label
        {
            Text = deporte.Federacion,
            FontAttributes = FontAttributes.Bold,
            FontSize = 16,
            TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.OrangeRed : Colors.Red
        }
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

            // Agregar cada elemento a Children individualmente
            foreach (var elemento in elementos)
            {
                ((VerticalStackLayout)carta.Content).Children.Add(elemento);
            }

            carta.GestureRecognizers.Add(tapGesture);

            return carta;
        }

        public static Frame CrearCartaProfesorGestor(Profesores profesor, EventHandler<TappedEventArgs> evento, EventHandler editar, EventHandler eliminar, bool generarBotones)
        {
            // Crear el gesto de toque (tap)
            TapGestureRecognizer tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += evento;

            // Crear la lista de elementos de la carta
            var elementos = new List<View>
    {
        new Label
        {
            Text = profesor.DNI,
            FontAttributes = FontAttributes.Bold,
            FontSize = 16,
            TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.LightGray : Colors.Gray
        },
        new Label
        {
            Text = profesor.Nombre,
            FontAttributes = FontAttributes.Bold,
            FontSize = 16,
            TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.LightGray : Colors.Gray
        },
        new Label
        {
            Text = profesor.Apellidos,
            FontAttributes = FontAttributes.Bold,
            FontSize = 16,
            TextColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.LightGray : Colors.Gray
        }
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

            // Agregar cada elemento a Children individualmente
            foreach (var elemento in elementos)
            {
                ((VerticalStackLayout)carta.Content).Children.Add(elemento);
            }

            // Agregar el gesto de toque (tap)
            carta.GestureRecognizers.Add(tapGesture);

            return carta;
        }
        #endregion




        public static async Task<bool> MostrarConfirmacion(Page page, string titulo, string mensaje)
        {
            bool respuesta = await page.DisplayAlert(titulo, mensaje, "Sí", "No");
            return respuesta;
        }

        internal static Picker CrearPicker(string v1, string v2, object unfocusedPicker)
        {
            throw new NotImplementedException();
        }
    }
}
