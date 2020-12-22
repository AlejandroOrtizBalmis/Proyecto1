using System;
using System.ComponentModel;

namespace Proyecto1
{
    public class Pelicula:INotifyPropertyChanged
    {
        private String titulo;

        public String Titulo
        {
            get { return titulo; }
            set { titulo = value; }
        }

        private String pista;

        public String Pista
        {
            get { return pista; }
            set { pista = value; }
        }

        private String imagen;

        public String Imagen
        {
            get { return imagen; }
            set { imagen = value; }
        }

        private String dificultad;

        public String Dificultad
        {
            get { return dificultad; }
            set { dificultad = value; }
        }

        private String genero;

        public String Genero
        {
            get { return genero; }
            set { genero = value; }
        }


        public Pelicula(String titulo, String pista, String imagen, String dificultad, String genero) {
            this.Titulo = titulo;
            this.Pista = pista;
            this.Imagen = imagen;
            this.Dificultad = dificultad;
            this.Genero = genero;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
