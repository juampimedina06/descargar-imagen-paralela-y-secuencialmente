using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace manejoArchivos
{
    public static class ManejoJSON
    {
        public static void ManejarJSON()
        {
            var personas = new List<Persona>()
            {
                new Persona { Id = 1, Nombre = "Juan", Ingreso = 1000 },
                new Persona { Id = 2, Nombre = "María", Ingreso = 2000 },
                new Persona { Id = 3, Nombre = "Pedro", Ingreso = 1500 },
                new Persona { Id = 4, Nombre = "Ana", Ingreso = 2500},
                new Persona { Id = 5, Nombre = "Luis", Ingreso = 3000 }
            };
            string rutaArchivoJSON = "C:\\Users\\Juampi\\Downloads\\Programacion\\C#\\listaPrograma.json";

            // Serializar la lista de personas a JSON
            string jsonString = JsonSerializer.Serialize(personas);
            // Guardar el JSON en un archivo
            File.WriteAllText(rutaArchivoJSON, jsonString);
            // Leer el JSON desde el archivo
            string jsonLeido = File.ReadAllText(rutaArchivoJSON);
            // Deserializar el JSON a una lista de personas
            var personasDeserializadas = JsonSerializer.Deserialize<List<Persona>>(jsonLeido);
            // Mostrar las personas deserializadas
            foreach (var persona in personasDeserializadas)
            {
                Console.WriteLine($"Id: {persona.Id}, Nombre: {persona.Nombre}, Ingreso: {persona.Ingreso}");
            }
        }
    }
}
