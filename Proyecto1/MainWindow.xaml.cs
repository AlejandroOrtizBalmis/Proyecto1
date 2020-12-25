using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace Proyecto1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Pelicula> peliculasJuego = new ObservableCollection<Pelicula>();
        ObservableCollection<Pelicula> peliculasTotales;
        int contador = 0;
        public MainWindow()
        {
            InitializeComponent();
            peliculasTotales = datos();
            peliculasListBox.DataContext = peliculasTotales;
            gestionarGrid.DataContext = peliculasListBox.SelectedItem;
            List<string> generos = new List<string> {"Comedia","Drama","Acción","Terror","Ciencia-Ficción"};
            generoComboBox.ItemsSource = generos;
            generoComboBox.SelectedItem = "Comedia";
            int contador = 0;
            añadirPeliculasJuego();
        }

        private void atrasImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (contador == 0) contador = peliculasJuego.Count - 1;
            else contador--;
            jugarDockPanel.DataContext = peliculasJuego[contador];
            contadorTextBlock.Text = (contador+1)+" / "+peliculasJuego.Count;
        }

        private void adelanteImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (contador == peliculasJuego.Count-1) contador =0;
            else contador++;
            jugarDockPanel.DataContext = peliculasJuego[contador];
            contadorTextBlock.Text = (contador + 1) + " / " + peliculasJuego.Count;
            
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            string peliculas = JsonConvert.SerializeObject(peliculasJuego);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                saveFileDialog.Filter = "Json file (*.json)|*.json";
                File.WriteAllText(saveFileDialog.FileName, peliculas);
            }
                
        }

        private void CargarButton_Click(object sender, RoutedEventArgs e)
        { peliculasTotales = new ObservableCollection<Pelicula>();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                openFileDialog.Filter = "Json file (*.json)|*.json";
                using (StreamReader jsonStream = File.OpenText(openFileDialog.FileName))
                {
                    var json = jsonStream.ReadToEnd();
                    List<Pelicula> peliculas = JsonConvert.DeserializeObject<List<Pelicula>>(json);

                    foreach (Pelicula p in peliculas)
                    {
                        peliculasTotales.Add(p);
                    }
                }
            }
        }

        private void AñadirPeliculaButton_Click(object sender, RoutedEventArgs e)
        {
            Pelicula n = new Pelicula(); ;
            if (contador == 0)
            {
                contador++;
                peliculasTotales.Add(n);
                MessageBox.Show("Introduzca los datos de la películas", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (vacios())
                {
                    string dificultad = "";
                    if (facilRadioButton.IsChecked == true) dificultad = "facil";
                    else if (normalRadioButton.IsChecked == true) dificultad = "normal";
                    else dificultad = "dificil";
                    

                    String genero = "";
                    switch (generoComboBox.SelectedItem.ToString())
                    {
                        case "Comedia":
                            genero = "comedia";
                            break;
                        case "Drama":
                            genero = "drama";
                            break;
                        case "Acción":
                            genero = "accion";
                            break;
                        case "Terror":
                            genero = "terror";
                            break;
                        case "Ciencia-Ficción":
                            genero = "ciencia-ficcion";
                            break;

                    }
                    n.Titulo = tituloTextBox.Text;
                    n.Pista = pistaTextBox.Text;
                    n.Imagen = imagenPeliculaTextBox.Text;
                    n.Genero = genero;
                    n.Dificultad = dificultad; limpiar();
                    contador = 0;
                }
                else MessageBox.Show("Datos de película vacios", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private ObservableCollection<Pelicula> datos() {
            ObservableCollection<Pelicula> datos = new ObservableCollection<Pelicula>();
            datos.Add(new Pelicula("300","El protagonista de la película se llama Leónidas", "http://2.bp.blogspot.com/-bhxSSMta6ck/VqlbzSopFbI/AAAAAAAAC3w/wgOtNRNWtXA/s1600/300.jpg","facil","accion"));
            datos.Add(new Pelicula("Tron", "La mayor parte de la película transcurre dentro de un juego", "https://fundacionsistema.com/wp-content/uploads/2015/03/Tron20Legacy.jpg", "normal", "ciencia-ficcion"));
            datos.Add(new Pelicula("El Rey Leon", "Película de dibujos animados sobre la vida de un león", "https://2.bp.blogspot.com/--lzqGUaUL8M/WTr0pTqYXBI/AAAAAAAABpY/RvVRNiHk5NUmgQ_17H0DPvPmJtQBy32UQCLcB/s1600/the-lion-king-524fb69e8c273.jpg", "normal", "drama"));
            datos.Add(new Pelicula("Un Ciudadano Ejemplar", "Venganza de un hombre despues de ver como asesinan a su esposa e hija", "http://es.web.img2.acsta.net/c_310_420/medias/nmedia/18/74/21/97/19417685.jpg", "dificil", "accion"));
            datos.Add(new Pelicula("Up", "El vehículo de transporte es una casa con globos", "https://vlagc.files.wordpress.com/2018/03/4681060_640px-e1521048043801.jpg", "normal", "ciencia-ficcion"));


            return datos;
        }
        private void limpiar() {
            tituloTextBox.Text = "";
            pistaTextBox.Text = "";
            imagenPeliculaTextBox.Text = "";
            facilRadioButton.IsChecked = true;
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
                    if (usados.Contains(numeroAleatorio) == false) {
                        usados.Add(numeroAleatorio);
                        peliculasJuego.Add(peliculasTotales[numeroAleatorio]);
                        cont++;
                    }
                }
            }
        }
    }
}
