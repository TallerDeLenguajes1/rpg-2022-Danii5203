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

        public Personaje(Caracteristicas _PJCararteristicas, Datos _PJDatos){ //contructor de la clase personaje
            //le cargamos los datos enviados cuando creamos el pj
            this.PJCaracteristicas = _PJCararteristicas;
            this.PJDatos = _PJDatos;
        }
    }
}