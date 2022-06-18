using System;

namespace JuegoRPG
{
    class Caracteristicas //clase
    {
        private double Velocidad;
        private double Destreza;
        private double Fuerza;
        private double Nivel;
        private double Armadura;

        public double velocidad{get=>Velocidad; set=>Velocidad = value;} //SIRVE PARA ACCEDER A LOS ATRIBUTOS DESDE FUERA
        public double destreza{get=>Destreza; set=>Destreza = value;}
        public double fuerza{get=>Fuerza; set=>Fuerza = value;}
        public double nivel{get=>Nivel; set=>Nivel = value;}
        public double armadura{get=>Armadura; set=>Armadura = value;}

        public Caracteristicas(){ //CONSTRUCTOR DE LA CLASE CARACTERISTICAS
            Random nRand = new Random(); //la clase RANDOM()
            this.Velocidad = nRand.Next(1, 11);
            this.Destreza = nRand.Next(1, 6); //entre 1 y 5
            this.Fuerza = nRand.Next(1, 11);
            this.Nivel = nRand.Next(1, 11);
            this.Armadura = nRand.Next(1, 6); //entre 1 y 5
        }
    }
}