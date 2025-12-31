using MisPostulacionesApp.Api.DTOs.Responses;
using System.Text.Json;

namespace MisPostulacionesApp.Api.Services.Implements
{
    public class IAService
    {
        private readonly HttpClient _http;

        public IAService(HttpClient http)
        {
            _http = http;
        }

        public async Task<RegistrarPostulacionResponse> ProcesarTextoAsync(string texto)
        {
            var prompt = """
Devuelve SOLO un objeto JSON válido.
NO texto adicional.
NO markdown.
NO comentarios.

El JSON DEBE empezar con { y terminar con }.

Estructura EXACTA:
{
  "Titulo": "",
  "Empresa": "",
  "Rol": "",
  "Descripcion": "",
  "Tecnologias": "",
  "Salario": 0,
  "Modalidad": "",
  "Plataforma": "",
  "Notas": ""
}

Salario debe ser numérico.

Texto:
""" + texto;




            var request = new
            {
                model = "llama3",
                prompt = prompt,
                stream = false
            };

            var response = await _http.PostAsJsonAsync(
                "http://localhost:11434/api/generate",
                request
            );

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<OllamaResponse>();

            return JsonSerializer.Deserialize<RegistrarPostulacionResponse>(
                result!.Response,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            )!;

        }
        //private static string ExtraerJson(string texto)
        //{
        //    var inicio = texto.IndexOf('{');
        //    var fin = texto.LastIndexOf('}');

        //    if (inicio == -1 || fin == -1 || fin <= inicio)
        //        throw new Exception("La IA no devolvió un JSON válido");

        //    return texto.Substring(inicio, fin - inicio + 1);
        //}

    }
}