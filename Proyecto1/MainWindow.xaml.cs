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
            facilRadioButton.IsChecked = true;

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
            if (vacios() == false)
            {
                string dificultad = "";
                if (facilRadioButton.IsChecked == true) dificultad = "facil";
                else if (normalRadioButton.IsChecked == true) dificultad = "normal";
                else dificultad = "dificil";


                String genero="";
                switch (generoComboBox.SelectedItem.ToString()) 
                {
                    case "Comedia":
                        genero="comedia";
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

                peliculasTotales.Add(new Pelicula(tituloTextBox.Text, pistaTextBox.Text, imagenPeliculaTextBox.Text, dificultad, genero));
                limpiar();
            }
            else MessageBox.Show("Datos de película vacios","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            
        }
        private ObservableCollection<Pelicula> datos() {
            ObservableCollection<Pelicula> datos = new ObservableCollection<Pelicula>();
            datos.Add(new Pelicula("300","El protagonista de la película se llama Leónidas", "http://2.bp.blogspot.com/-bhxSSMta6ck/VqlbzSopFbI/AAAAAAAAC3w/wgOtNRNWtXA/s1600/300.jpg","facil","accion"));
            datos.Add(new Pelicula("Tron", "La mayor parte de la película transcurre dentro de un juego", "https://fundacionsistema.com/wp-content/uploads/2015/03/Tron20Legacy.jpg", "normal", "ciencia-ficcion"));


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
            if (tituloTextBox.Text == "" && imagenPeliculaTextBox.Text == "" && pistaTextBox.Text == "")
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
