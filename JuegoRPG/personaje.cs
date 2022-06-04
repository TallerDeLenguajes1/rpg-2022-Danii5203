using System;

namespace JuegoRPG
{
    class Personaje
    {
        private Caracteristicas PJCaracteristicas; 
        private Datos PJDatos;

        public Caracteristicas PjCaracteristicas {get=>PJCaracteristicas; set=>PJCaracteristicas = value;}
        public Datos PjDatos {get=>PJDatos; set=>PJDatos = value;}

        public Personaje(Caracteristicas _PJCararteristicas, Datos _PJDatos){
            this.PJCaracteristicas = _PJCararteristicas;
            this.PJDatos = _PJDatos;
        }
    }
}