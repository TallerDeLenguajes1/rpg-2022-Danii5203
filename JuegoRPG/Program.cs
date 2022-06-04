using System;

namespace JuegoRPG
{
    class Program
    {
        static void Main(string[] args){
            string nombrePJ1, nombrePJ2;

            List<Personaje> jugadores = new List<Personaje>(); //CREAMOS LA LISTA

            //PEDIMOS EL NOMBRE DE LOS PJ
            //PJ 1
            Console.WriteLine("Ingrese el nombre del PJ 1: ");
            nombrePJ1 = Console.ReadLine();
            Caracteristicas caracteristicasPJ1 = new Caracteristicas();
            Datos datosPJ1 = new Datos(nombrePJ1);
            Personaje PJ1 = new Personaje(caracteristicasPJ1, datosPJ1);
            //PJ 2
            Console.WriteLine("Ingrese el nombre del PJ 2: ");
            nombrePJ2 = Console.ReadLine();
            Caracteristicas caracteristicasPJ2 = new Caracteristicas();
            Datos datosPJ2 = new Datos(nombrePJ2);
            Personaje PJ2 = new Personaje(caracteristicasPJ2, datosPJ2);

            //AGG LOS JUGADORES A LA LISTA
            jugadores.Add(PJ1);
            jugadores.Add(PJ2);

        }
    }
}

