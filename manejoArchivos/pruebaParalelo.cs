using manejoArchivos;
using System.Diagnostics;
using System.Text.Json;

// Toda la lógica de descarga vive acá
class DescargadorPrueba
{
    static HttpClient client = new HttpClient();

    // Método "orquestador": hace todo el trabajo de punta a punta
    public static async Task EjecutarParalelo()
    {
        string rutaParalela = @"C:\Users\Juampi\Downloads\Programacion\c#\paralelo";
        Directory.CreateDirectory(rutaParalela);
        string rutaJSON = Path.Combine(rutaParalela, "descargas.json");
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

        var descargas = new List<Task<ImagenDescargada>>()
        {
            DescargarImagen(rutaParalela, urls[0]),
            DescargarImagen(rutaParalela, urls[1]),
            DescargarImagen(rutaParalela, urls[2]),
            DescargarImagen(rutaParalela, urls[3]),
            DescargarImagen(rutaParalela, urls[4]),
        };

        ImagenDescargada[] resultados = await Task.WhenAll(descargas);

        sw.Stop();
        Console.WriteLine($"descarga paralela terminada en {sw.ElapsedMilliseconds}");

        foreach (var imagen in resultados)
        {
            Console.WriteLine($"Imagen paralela con url: {imagen.Url}, y  path:{imagen.Path} ");
        }

        var lista = new List<ImagenDescargada>(resultados);
        string JsonString = JsonSerializer.Serialize(lista);
        File.WriteAllText(rutaJSON, JsonString);
        string JsonLeido = File.ReadAllText(rutaJSON);
        var imagenDeserealizada = JsonSerializer.Deserialize<List<ImagenDescargada>>(JsonLeido);
        foreach (var imagenJson in imagenDeserealizada)
        {
            Console.WriteLine($"Imagen convertida a JSON con url: {imagenJson.Url}, y  path:{imagenJson.Path} ");
        }
    }

    // Descarga UNA imagen (usado adentro de EjecutarParalelo)
    static async Task<ImagenDescargada> DescargarImagen(string carpeta, string url)
    {
        string nombre = Path.GetFileName(url);
        string path = Path.Combine(carpeta, nombre);

        byte[] datos = await client.GetByteArrayAsync(url);
        await File.WriteAllBytesAsync(path, datos);

        return new ImagenDescargada { Path = path, Url = url };
    }
}
git remote add origin https://github.com/juampimedina06/descargar-imagen-paralela-y-secuencialmente.git