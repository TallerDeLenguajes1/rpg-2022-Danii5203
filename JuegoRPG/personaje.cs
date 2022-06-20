using System;

namespace JuegoRPG
{
    class Personaje
    {
        //Instanciamos los objetos
        private Caracteristicas PJCaracteristicas;
        private Datos PJDatos;

        public Caracteristicas PjCaracteristicas {get=>PJCaracteristicas; set=>PJCaracteristicas = value;}
        public Datos PjDatos {get=>PJDatos; set=>PJDatos = value;}

        public Personaje(){ //contructor de la clase personaje
            //INSTANCIAMOS NUEVOS OBJETOS
            this.PjCaracteristicas = new Caracteristicas();
            this.PjDatos = new Datos();
        }
    }
}