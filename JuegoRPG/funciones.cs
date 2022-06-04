using System;

namespace JuegoRPG
{
    class funciones
    {
        public Personaje crearPJ(string _nombrePJ){
            Caracteristicas caracteristicasPJ = new Caracteristicas();
            Datos datosPJ = new Datos(_nombrePJ);
            Personaje PJ = new Personaje(caracteristicasPJ, datosPJ);
            return PJ;
        }
        public void mostrarDatosPJ(Personaje _PJ){
            Console.WriteLine("Nombre: "+_PJ.PjDatos.nombre);
            Console.WriteLine("Apodo: "+_PJ.PjDatos.apodo);
            Console.WriteLine("Raza: "+_PJ.PjDatos.raza);
            Console.WriteLine("Fecha de Nacimiento: "+_PJ.PjDatos.fechaDeNac);
            Console.WriteLine("Edad: "+_PJ.PjDatos.edad+" años");
            Console.WriteLine("Salud: "+_PJ.PjDatos.salud);
        }
        public void mostrarCaracteristicasPJ(Personaje _PJ){
            Console.WriteLine("Velocidad: "+_PJ.PjCaracteristicas.velocidad);
            Console.WriteLine("Destreza: "+_PJ.PjCaracteristicas.destreza);
            Console.WriteLine("Fuerza: "+_PJ.PjCaracteristicas.fuerza);
            Console.WriteLine("Nivel: "+_PJ.PjCaracteristicas.nivel);
            Console.WriteLine("Armadura: "+_PJ.PjCaracteristicas.armadura);
        }
    }
}