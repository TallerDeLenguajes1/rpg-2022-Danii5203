using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

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
            string[] _raza = new string[] {"Soul Master", "Blade Knight", "Muse Elf", "Magic Gladiator"};
            string[] _apodo = new string[] {"SM", "BK", "ELF", "MG"};

            Random nRand = new Random();
            
            int nRandPJ = nRand.Next(0, 4);
            this.nombre = APIGeneraNomAleatorio();
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
        public string APIGeneraNomAleatorio(){ //API que nos devuelve el nombre e informacion de una persona random
            var url = $"https://randomuser.me/api/?inc=name&noinfo"; //inc=name, solo devuelve datos del nombre, apellido
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            string? nombreReturn;
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string strNomCompleto = objReader.ReadToEnd();
                            nombres? nombreCompleto = JsonSerializer.Deserialize<nombres>(strNomCompleto);
                            nombreReturn = nombreCompleto.Results[0].Name.First; 
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                //throw;
                string[] nombres = new string[] {"PLAYER 1", "PLAYER 2", "PLAYER 3", "PLAYER 4"};

                Random nRand = new Random();
                nombreReturn = nombres[nRand.Next(0,4)];
            }
            return nombreReturn;
        }
    }
}