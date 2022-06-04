using System;
namespace JuegoRPG
{
    class Program
    {
        static void Main(string[] args){
            string nombrePJ1, nombrePJ2;
            funciones funciones = new funciones();

            List<Personaje> jugadores = new List<Personaje>(); //CREAMOS LA LISTA

            //PEDIMOS EL NOMBRE DE LOS PJ
            Console.WriteLine("******************************************************\n***** Crear Personajes *****\n******************************************************");
            //PJ 1
            Console.WriteLine("_Ingrese el nombre del PJ 1: ");
            nombrePJ1 = Console.ReadLine();
            Personaje PJ1 = funciones.crearPJ(nombrePJ1);
            //PJ 2
            Console.WriteLine("\n_Ingrese el nombre del PJ 2: ");
            nombrePJ2 = Console.ReadLine();
            Personaje PJ2 = funciones.crearPJ(nombrePJ2);

            //AGG LOS JUGADORES A LA LISTA
            jugadores.Add(PJ1);
            jugadores.Add(PJ2);

            //MOSTRAMOS LOS DATOS DE LOS PERSONAJES
            Console.WriteLine("\n******************************************************\n***** Datos y Caracteristicas de los Personajes *****\n******************************************************");
            int idPj = 1;
            foreach (var PJ in jugadores)
            {
                Console.WriteLine($"======================= PJ{idPj} ({PJ.PjDatos.nombre}) =======================");
                Console.WriteLine("_DATOS: ");
                funciones.mostrarDatosPJ(PJ);
                Console.WriteLine("\n_CARACTERISTICAS: ");
                funciones.mostrarCaracteristicasPJ(PJ);
                Console.WriteLine("\n");
                idPj++;
            }
        }
    }
}

