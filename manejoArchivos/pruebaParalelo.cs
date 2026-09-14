using manejoArchivos;
using System.Diagnostics;
using System.Text.Json;

class Descargar
{
    static HttpClient client = new HttpClient();

    public static async Task DescargarTodo()
    {
        string DirParalelo = @"C:\Users\Juampi\Downloads\Programacion\c#\paralelo";
        Directory.CreateDirectory(DirParalelo);
        string rutaJson = Path.Combine(DirParalelo, "json.Descargas");
        Stopwatch sw = Stopwatch.StartNew();

        string[] urls =
         {
            "https://images.unsplash.com/photo-1500530855697-b586d89ba3ee",
            "https://images.unsplash.com/photo-1507525428034-b723cf961d3e",
            "https://images.unsplash.com/photo-1470770841072-f978cf4d019e",
            "https://images.unsplash.com/photo-1441974231531-c6227db76b6e",
            "https://images.unsplash.com/photo-1501785888041-af3ef285b470"
        };

        sw.Start();

        //var descargas = new List<Task<ImagenDescargada>>()
        //{
        //    DescargarImagen(DirParalelo, urls[0]),
        //    DescargarImagen(DirParalelo, urls[1]),
        //    DescargarImagen(DirParalelo, urls[2]),
        //    DescargarImagen(DirParalelo, urls[3]),
        //    DescargarImagen(DirParalelo, urls[4]),
        //};

        //ImagenDescargada[] resultado = await Task.WhenAll(descargas);

        var resultado = new List<ImagenDescargada>();
        foreach (var url in urls)
        {
            ImagenDescargada imagen = await DescargarImagen(DirParalelo, url);
            resultado.Add(imagen);
        }

        sw.Stop();
        foreach (var imagen in resultado)
        {
            Console.WriteLine($"imagen con la url: {imagen.Url} y el path:{imagen.Path} ");
        }

        
        Console.WriteLine($" Las imagenes se descagaron exitosamente en {sw.ElapsedMilliseconds} ms ");

        var lista = new List<ImagenDescargada>();
        string StringJson = JsonSerializer.Serialize(resultado);
        File.WriteAllText(rutaJson, StringJson);
        string jsonLeido = File.ReadAllText(rutaJson);
        var imagenDeserealizada = JsonSerializer.Deserialize<List<ImagenDescargada>>(jsonLeido);
        foreach (var imagenJson in imagenDeserealizada)
        {
            Console.WriteLine($"imagen en json con la url: {imagenJson.Url} y el path:{imagenJson.Path} ");  
        }

    }


    static async Task<ImagenDescargada> DescargarImagen(string carpeta, string url)
    {
        string nombre = Path.GetFileName(url);
        string path = Path.Combine(carpeta, nombre);

        byte[] datos = await client.GetByteArrayAsync(url);
        await File.WriteAllBytesAsync(path, datos);

        return new ImagenDescargada { Path = path, Url = url };
    }


}


    
