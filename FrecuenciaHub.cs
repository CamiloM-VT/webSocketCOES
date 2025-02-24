using Microsoft.AspNetCore.SignalR;
using System.Net.Http;
using System.Threading.Tasks;

public class FrecuenciaHub : Hub
{
    private readonly HttpClient _httpClient;

    public FrecuenciaHub(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task GetFrequencyData()
    {
        string url = "https://appserver.coes.org.pe/COES.WebService.Movil/movilservicio.svc/obtenerfrecuencia";

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string data = await response.Content.ReadAsStringAsync();

            await Clients.Caller.SendAsync("ReceiveFrequencyData", data);
        }
        catch (HttpRequestException ex)
        {
            await Clients.Caller.SendAsync("ReceiveFrequencyData", $"Error al obtener datos: {ex.Message}");
        }
    }
}
