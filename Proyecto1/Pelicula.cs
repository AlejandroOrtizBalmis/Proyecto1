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

        private Boolean facil;

        public Boolean Facil
        {
            get { return facil; }
            set 
            { 
                if(this.facil!=value)
                {
                    this.facil = value;
                    this.NotifyPropertyChanged("Facil");
                    if (facil) 
                    {
                        normal = false;
                        dificil = false;
                    }
                }
            }
        }
        private Boolean normal;

        public Boolean Normal
        {
            get { return normal; }
            set
            {
                if (this.normal != value)
                {
                    this.normal = value;
                    this.NotifyPropertyChanged("Normal");
                    if (normal)
                    {
                        dificil = false;
                        facil = false;
                    }
                }
            }
        }

        private Boolean dificil;

        public Boolean Dificil
        {
            get { return dificil; }
            set
            {
                if (this.dificil != value)
                {
                    this.dificil = value;
                    this.NotifyPropertyChanged("Dificil");
                    if (dificil) 
                    {
                        normal = false;
                        facil = false;
                    }
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
        public Pelicula(String titulo, String pista, String imagen, Boolean facil,Boolean normal,  Boolean dificil, String genero) {
            this.Titulo = titulo;
            this.Pista = pista;
            this.Imagen = imagen;
            this.Facil = facil;
            this.Normal = normal;
            this.Dificil = dificil;
            this.Genero = genero;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
