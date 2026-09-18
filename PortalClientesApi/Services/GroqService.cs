using System.Text;
using System.Text.Json;

namespace PortalClientesApi.Services
{
    public class GroqService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GroqService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["Groq:ApiKey"]
                ?? throw new InvalidOperationException("Falta configurar Groq:ApiKey");
        }

        public async Task<string> GenerarMensajeConfirmacionAsync(string nombreCliente, decimal total)
        {
            var prompt = $"Escribí un mensaje breve y cordial (máximo 2 oraciones) confirmando " +
                         $"un pedido para el cliente {nombreCliente} por un total de ${total}. " +
                         $"En español rioplatense, tono profesional pero cercano.";

            var requestBody = new
            {
                model = "llama-3.3-70b-versatile",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                },
                temperature = 0.7
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

            var response = await _httpClient.PostAsync(
                "https://api.groq.com/openai/v1/chat/completions", content);

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseBody);

            var mensaje = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return mensaje ?? "Pedido confirmado.";
        }

        public async Task<(string Nota, string Prioridad)> AnalizarPedidoAsync(string nombreCliente, decimal total)
        {
            var prompt = $"Analizá este pedido: cliente '{nombreCliente}', monto total ${total}. " +
                         $"Clasificá la prioridad como \"Alta\" (montos grandes o clientes a destacar), " +
                         $"\"Normal\" o \"Baja\", y escribí una nota interna breve (una oración) " +
                         $"para el equipo de ventas resumiendo el pedido. " +
                         $"Respondé ÚNICAMENTE con un JSON válido, sin texto adicional, con este formato exacto: " +
                         $"{{\"nota\": \"...\", \"prioridad\": \"Alta|Normal|Baja\"}}";

            var requestBody = new
            {
                model = "llama-3.3-70b-versatile",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                },
                temperature = 0.3,
                response_format = new { type = "json_object" }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

            var response = await _httpClient.PostAsync(
                "https://api.groq.com/openai/v1/chat/completions", content);

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseBody);

            var contenido = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString() ?? "{}";

            try
            {
                using var resultado = JsonDocument.Parse(contenido);
                var nota = resultado.RootElement.GetProperty("nota").GetString() ?? "Sin nota generada.";
                var prioridad = resultado.RootElement.GetProperty("prioridad").GetString() ?? "Normal";
                return (nota, prioridad);
            }
            catch (Exception)
            {
                // Fallback simple si la IA no devuelve JSON válido
                var prioridadFallback = total >= 100000 ? "Alta" : "Normal";
                return ("No se pudo generar la nota automática.", prioridadFallback);
            }
        }
    }
}