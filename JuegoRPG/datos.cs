using System;

namespace JuegoRPG
{
    class Datos
    {
        private string Raza="";
        private string Nombre="";
        private string Apodo="";
        private DateOnly FechaDeNacimiento;
        private int Edad;
        private int Salud;

        public string raza {get=>Raza; set=>Raza = value;}
        public string nombre {get=>Nombre; set=>Nombre = value;}
        public string apodo {get=>Apodo; set=>Apodo = value;}
        public DateOnly fechaDeNac {get=>FechaDeNacimiento; set=>FechaDeNacimiento = value;}
        public int edad {get=>Edad; set=>Edad = value;}
        public int salud {get=>Salud; set=>Salud = value;}


        public Datos(string _nombre){ //creamos un objeto con los datos del pj
            string[] _raza = new string[] {"Soul Master", "Blade Knight", "Muse Elf", "Magic Gladiator"};
            string[] _apodo = new string[] {"SM", "BK", "ELF", "MG"};

            Random nRand = new Random();
            
            int nRaza = nRand.Next(0, 4);
            this.Nombre = _nombre;
            this.Raza = _raza[nRaza];
            this.Apodo = _raza[nRaza];
            this.FechaDeNacimiento = new DateOnly(nRand.Next(1900, 2001), nRand.Next(1, 13), nRand.Next(1, 31)); //año, mes, dia
            this.Edad = obtenerEdad(FechaDeNacimiento);
            this.Salud = 100;
        }
        public int obtenerEdad(DateOnly fechaDeNacimiento){
            int edad = DateTime.Now.Year - fechaDeNacimiento.Year;
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