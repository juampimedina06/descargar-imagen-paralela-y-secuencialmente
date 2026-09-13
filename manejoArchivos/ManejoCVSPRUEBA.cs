using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace manejoArchivos
{
    public class ManejoCVSPRUEBA
    {
        
        string ruta = "C:\\Users\\Juampi\\Downloads\\Programacion\\C#\\listaPrograma.csv";
        public void CrearCVS()
        {
            var personas = new List<Persona>()
            {
                new Persona { Id = 1, Nombre = "Juan", Ingreso = 1000 },
                new Persona { Id = 2, Nombre = "María", Ingreso = 2000 },
                new Persona { Id = 3, Nombre = "Pedro", Ingreso = 1500 },
                new Persona { Id = 4, Nombre = "Ana", Ingreso = 2500},
                new Persona { Id = 5, Nombre = "Luis", Ingreso = 3000 }
            };
            var stringBuilder = new StringBuilder();
            foreach (var persona in personas)
            {
                stringBuilder.AppendLine($"{persona.Id},{persona.Nombre},{persona.Ingreso}");
            }
            using (var archivo = new StreamWriter(ruta, append: false, Encoding.UTF8))
            {
                archivo.Write(stringBuilder.ToString());
            }
            ;
        }
    }
}
