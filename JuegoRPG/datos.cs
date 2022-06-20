using System;

namespace JuegoRPG
{
    public class Datos
    {
        private string Raza="";
        private string Nombre="";
        private string Apodo="";
        private DateTime FechaDeNacimiento;
        private int Edad;
        private double Salud;
        private double PartidasGanadas;

        public string raza {get=>Raza; set=>Raza = value;}
        public string nombre {get=>Nombre; set=>Nombre = value;}
        public string apodo {get=>Apodo; set=>Apodo = value;}
        public DateTime fechaDeNac {get=>FechaDeNacimiento; set=>FechaDeNacimiento = value;}
        public int edad {get=>Edad; set=>Edad = value;}
        public double salud {get=>Salud; set=>Salud = value;}
        public double partidasGanadas {get=>PartidasGanadas; set=>PartidasGanadas = value;}


        public Datos(){ //CONSTRUCTOR DE LA CLASE DATOS
            string[] _nombre = new string[] {"DANI", "LUCAS", "MARGARITA", "XD", "LOL", "FIFA"};
            string[] _raza = new string[] {"Soul Master", "Blade Knight", "Muse Elf", "Magic Gladiator"};
            string[] _apodo = new string[] {"SM", "BK", "ELF", "MG"};

            Random nRand = new Random();
            
            int nRandPJ = nRand.Next(0, 4);
            int nRandNom = nRand.Next(1, 6);
            this.nombre = _nombre[nRandPJ];
            this.raza = _raza[nRandPJ];
            this.apodo = _apodo[nRandPJ];
            this.fechaDeNac = new DateTime(nRand.Next(1722, 2000), nRand.Next(1, 13), nRand.Next(1, 31)); //año, mes, dia
            this.edad = obtenerEdad(FechaDeNacimiento);
            this.salud = 100;
            this.partidasGanadas = partidasGanadas;
        }
        public int obtenerEdad(DateTime fechaDeNacimiento){
            int edad = DateTime.Now.Year - fechaDeNacimiento.Year; //restamos el año en el que estamos con el año de nacimiento
            if(fechaDeNacimiento.Month > DateTime.Now.Month){ //En caso de que el mes de nacimiento sea mayor que el que estamos 
                edad -= 1; //restaremos 1 año de la edad
            }
            if(fechaDeNacimiento.Month == DateTime.Now.Month){ //En el caso de que el mes sea el mismo
                if(fechaDeNacimiento.Day > DateTime.Now.Day){ //En el caso de que su cumpleaños todavia no llega
                    edad -= 1;
                }
            }
            return edad;
        }
    }
}