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
            set 
            {
                if (this.titulo != value) 
                {
                    this.titulo = value;
                    this.NotifyPropertyChanged("Titulo");
                }
            }
        }

        private String pista;

        public String Pista
        {
            get { return pista; }
            set 
            {
                if (this.pista != value)
                {
                    this.pista = value;
                    this.NotifyPropertyChanged("Pista");
                } 
            }
        }

        private String imagen;

        public String Imagen
        {
            get { return imagen; }
            set 
            {
                if (this.imagen != value)
                {
                    this.imagen = value;
                    this.NotifyPropertyChanged("Imagen");
                }
            }
        }

        private String dificultad;

        public String Dificultad
        {
            get { return dificultad; }
            set 
            { 
                if(this.dificultad!=value)
                {
                    this.dificultad = value;
                    this.NotifyPropertyChanged("Dificultad");
                }
            }
        }

        private String genero;

        public String Genero
        {
            get { return genero; }
            set 
            {
                if (this.genero != value)
                {
                    this.genero = value;
                    this.NotifyPropertyChanged("Genero");
                } 
            }
        }

        public Pelicula() { }
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
