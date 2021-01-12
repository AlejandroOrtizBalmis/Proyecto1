using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Proyecto1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Pelicula> peliculasJuego = new ObservableCollection<Pelicula>();
        ObservableCollection<Pelicula> peliculasTotales;
        int contadorJuego = 0;
        int contador = 0;
        Pelicula nueva;
        List<int> puntuaciones = new List<int>();
        List<int> peliculasCorregidas = new List<int>();
        bool terminado = false;
        bool pista = false;
        
        public MainWindow()
        {
            InitializeComponent();
            peliculasTotales = datos();
            peliculasListBox.DataContext = peliculasTotales;
            gestionarGrid.DataContext = peliculasListBox.SelectedItem;
            List<string> generos = new List<string> {"Comedia","Drama","Acción","Terror","Ciencia-Ficción"};
            generoComboBox.ItemsSource = generos;
            generoComboBox.SelectedItem = "Comedia";
            List<string> dificultad = new List<string> {"Fácil","Normal","Difícil"};
            dificultadComboBox.ItemsSource = dificultad;
            dificultadComboBox.SelectedItem = "Fácil";
            jugarDockPanel.DataContext = new Pelicula("Título","Pista", "https://www.elcineenlasombra.com/wp-content/uploads/2018/10/pelicula-rodar-FB.jpg","Fácil","Comedia");
            añadirPeliculasJuego();
        }


        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            string peliculas = JsonConvert.SerializeObject(peliculasJuego);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Json file (*.json)|*.json";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, peliculas);
            }
                
        }

        private void CargarButton_Click(object sender, RoutedEventArgs e)
        { peliculasTotales = new ObservableCollection<Pelicula>();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Json file (*.json)|*.json";
            if (openFileDialog.ShowDialog() == true)
            {
                if(File.OpenText(openFileDialog.FileName) != null)
                {
                    using (StreamReader jsonStream = File.OpenText(openFileDialog.FileName))
                    {
                        var json = jsonStream.ReadToEnd();
                        List<Pelicula> peliculas = JsonConvert.DeserializeObject<List<Pelicula>>(json);

                        foreach (Pelicula p in peliculas)
                        {
                            peliculasTotales.Add(p);
                        }
                    }
                    peliculasListBox.DataContext = peliculasTotales;
                }
            }
        }

        private void AñadirPeliculaButton_Click(object sender, RoutedEventArgs e)
        {
            if (contador == 0)
            {
                nueva = new Pelicula();
                contador++;
                peliculasTotales.Add(nueva);
                MessageBox.Show("Introduzca los datos de la películas", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (!vacios())
                {

                    Pelicula quitar = nueva;
                    nueva.Titulo = tituloTextBox.Text;
                    nueva.Pista = pistaTextBox.Text;
                    nueva.Imagen = imagenPeliculaTextBox.Text;
                    if (generoComboBox.SelectedItem == null) nueva.Genero = "Comedia";
                    else nueva.Genero = generoComboBox.SelectedItem.ToString();
                    if (dificultadComboBox.SelectedItem == null) nueva.Dificultad = "Fácil";
                    else nueva.Dificultad = dificultadComboBox.SelectedItem.ToString();
                    limpiar();
                    contador = 0;
                    peliculasTotales.Remove(quitar);
                    peliculasTotales.Add(nueva);
                }
                else MessageBox.Show("Datos de película vacios", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private ObservableCollection<Pelicula> datos() {
            ObservableCollection<Pelicula> datos = new ObservableCollection<Pelicula>();
            datos.Add(new Pelicula("300","El protagonista de la película se llama Leónidas", "http://2.bp.blogspot.com/-bhxSSMta6ck/VqlbzSopFbI/AAAAAAAAC3w/wgOtNRNWtXA/s1600/300.jpg","Fácil","Acción"));
            datos.Add(new Pelicula("Tron", "La mayor parte de la película transcurre dentro de un juego", "https://fundacionsistema.com/wp-content/uploads/2015/03/Tron20Legacy.jpg","Normal", "Ciencia-Ficción"));
            datos.Add(new Pelicula("El Rey Leon", "Película de dibujos animados sobre la vida de un león", "https://2.bp.blogspot.com/--lzqGUaUL8M/WTr0pTqYXBI/AAAAAAAABpY/RvVRNiHk5NUmgQ_17H0DPvPmJtQBy32UQCLcB/s1600/the-lion-king-524fb69e8c273.jpg","Normal", "Drama"));
            datos.Add(new Pelicula("Un Ciudadano Ejemplar", "Venganza de un hombre despues de ver como asesinan a su esposa e hija", "http://es.web.img2.acsta.net/c_310_420/medias/nmedia/18/74/21/97/19417685.jpg","Difícil", "Acción"));
            datos.Add(new Pelicula("Up", "El vehículo de transporte es una casa con globos", "https://vlagc.files.wordpress.com/2018/03/4681060_640px-e1521048043801.jpg","Normal", "Ciencia-Ficción"));


            return datos;
        }
        private void limpiar() {
            tituloTextBox.Text = "";
            pistaTextBox.Text = "";
            imagenPeliculaTextBox.Text = "";
            dificultadComboBox.SelectedItem = "Fácil";
            generoComboBox.SelectedItem = "Comedia";
        }
        public bool vacios() {
            if (tituloTextBox.Text == ""  && imagenPeliculaTextBox.Text == "" && pistaTextBox.Text == "")
                return true;
            else return false;
        }
        public void añadirPeliculasJuego(){
            if (peliculasTotales.Count <= 5) peliculasJuego = peliculasTotales;
            else
            {
                peliculasJuego = new ObservableCollection<Pelicula>();
                int cont = 0;
                List<int> usados = new List<int>();
                while (cont < 5) {
                    int numeroAleatorio = new Random().Next(0, peliculasTotales.Count);
                    if (!usados.Contains(numeroAleatorio)) {
                        usados.Add(numeroAleatorio);
                        peliculasJuego.Add(peliculasTotales[numeroAleatorio]);
                        cont++;
                    }
                }
            }
        }
        private void EliminarPeliculaButton_Click(object sender, RoutedEventArgs e)
        {
            peliculasTotales.Remove((Pelicula)peliculasListBox.SelectedItem);
        }

        private void examinarButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                imagenPeliculaTextBox.Text = openFileDialog.FileName;
            }
        }
        // Ventana de juego

        private void nuevaPartidaButton_Click(object sender, RoutedEventArgs e)
        {
            terminado = false;
            if (peliculasTotales.Count >= 5)
            {
                int cont = 0;
                while (cont < 5)
                {
                    Random random = new Random();
                    int num = random.Next(0, peliculasTotales.Count - 1);
                    if (!peliculasJuego.Contains(peliculasTotales[num]))
                    {
                        peliculasJuego.Add(peliculasTotales[num]);
                        cont++;
                    }
                }

                jugarDockPanel.DataContext = peliculasJuego[0];
                contadorTextBlock.Text = "1/" + peliculasJuego.Count;
                vaciar(0);
                puntuacionStackPanel.Children.Clear();
                puntuaciones.Clear();
                peliculasCorregidas.Clear();
            }
            else MessageBox.Show("No hay películas suficientes","",MessageBoxButton.OK,MessageBoxImage.Warning);
        }


        private void atrasImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!terminado)
            {
                if (contadorJuego == 0) contadorJuego = peliculasJuego.Count - 1;
                else contadorJuego--;
                jugarDockPanel.DataContext = peliculasJuego[contadorJuego];
                contadorTextBlock.Text = (contadorJuego + 1) + " / " + peliculasJuego.Count;
                vaciar(contadorJuego);
            }
           
        }

        private void adelanteImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!terminado)
            {
                if (contadorJuego == peliculasJuego.Count - 1) contadorJuego = 0;
                else contadorJuego++;
                jugarDockPanel.DataContext = peliculasJuego[contadorJuego];
                contadorTextBlock.Text = (contadorJuego + 1) + " / " + peliculasJuego.Count;
                vaciar(contadorJuego);
            }
        }
        public void vaciar(int cont) {

            tituloPeliculaJuegoTextBox.Text = "";
            pistaCheckBox.IsChecked = false;
            jugarDockPanel.DataContext = peliculasJuego[cont];
            pista = false;
        }

        private void validarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!peliculasCorregidas.Contains(contadorJuego))
            {
                if (peliculasJuego[contadorJuego].Titulo.ToLower() == tituloPeliculaJuegoTextBox.Text.ToLower())
                {
                    if (pista)
                    {
                        int puntos = calcularPuntos(peliculasJuego[contadorJuego].Dificultad) / 2;
                        añadirPuntuacion(puntos);
                    }
                    else
                    {
                        int puntos = calcularPuntos(peliculasJuego[contadorJuego].Dificultad);
                        añadirPuntuacion(puntos);
                    }
                }
                else 
                {
                    añadirPuntuacion(0);
                }
                
                peliculasCorregidas.Add(contadorJuego);
            }
        }
        public int calcularPuntos(string dificultad) 
        {
            if (dificultad=="Fácil") return 150;
            else if (dificultad == "Normal") return 300;
            else return 500;
        }
        public void añadirPuntuacion(int puntos) 
        { 
            Thickness thickness = new Thickness(2, 0, 2, 2);
            TextBlock t = new TextBlock
            {
                FontSize = 12,
                Margin = thickness,
                Text = "Puntuación " + (contadorJuego+1) + " : " + puntos
            };
            puntuacionStackPanel.Children.Add(t);
            puntuaciones.Add(puntos);
        }

        private void finalizarButton_Click(object sender, RoutedEventArgs e)
        {
            int totales = 0;
            for (int i = 0; i < puntuaciones.Count; i++) 
            {
                totales = totales + puntuaciones[i];
            }
            Thickness thickness = new Thickness(2, 0, 2, 2);
            TextBlock t = new TextBlock
            {
                FontSize = 14,
                Margin = thickness,
                Text = "Total : " +totales
            };
            puntuacionStackPanel.Children.Add(t);
            terminado = true;
        }

        private void pistaCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            pista = true;
        }

        private void nuevaPartidaButton_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            KeyGesture play = new KeyGesture(Key.N,ModifierKeys.Control);
            if (play.Matches(null,e)) 
            {
                terminado = false;
                if (peliculasTotales.Count >= 5)
                {
                    int cont = 0;
                    while (cont < 5)
                    {
                        Random random = new Random();
                        int num = random.Next(0, peliculasTotales.Count - 1);
                        if (!peliculasJuego.Contains(peliculasTotales[num]))
                        {
                            peliculasJuego.Add(peliculasTotales[num]);
                            cont++;
                        }
                    }

                    jugarDockPanel.DataContext = peliculasJuego[0];
                    contadorTextBlock.Text = "1/" + peliculasJuego.Count;
                    vaciar(0);
                    puntuacionStackPanel.Children.Clear();
                    puntuaciones.Clear();
                    peliculasCorregidas.Clear();
                }
                else MessageBox.Show("No hay películas suficientes", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
