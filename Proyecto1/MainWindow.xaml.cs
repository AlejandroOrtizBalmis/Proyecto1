using System.Windows;

namespace Proyecto1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Pelicula[] peliculasJuego = new Pelicula[5];
        int contador = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void atrasImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (contador == 0) contador = peliculasJuego.Length - 1;
            else contador--;
            jugarDockPanel.DataContext = peliculasJuego[contador];
            contadorTextBlock.Text = (contador+1)+" / "+peliculasJuego.Length;
        }

        private void adelanteImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (contador == peliculasJuego.Length-1) contador =0;
            else contador++;
            jugarDockPanel.DataContext = peliculasJuego[contador];
            contadorTextBlock.Text = (contador + 1) + " / " + peliculasJuego.Length;

        }
    }
}
