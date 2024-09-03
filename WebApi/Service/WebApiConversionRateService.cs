using WebApi.Interfaces;
using WebApi.Model;
using Microsoft.Extensions.Options;



namespace WebApi.Service
{
    public class WebApiConversionRateService : IConversionRate
    {
        public double BRL { get; set; }
        private readonly HttpClient _httpClient;
        private readonly string ApiKey;

        public WebApiConversionRateService(HttpClient httpClient, IOptions<WebApiSettings> settings)
        {
            _httpClient = httpClient;
            ApiKey = settings.Value.ApiKey;
        }

        public async Task GetBrlRateAsync()
        {
            var url = $"https://v6.exchangerate-api.com/v6/{ApiKey}/pair/USD/BRL";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<WebApiResponse>();

                if (result != null && result.Result == "sucesso")
                {
                    BRL = result.ConversionRate;
                }
                else
                {
                    throw new Exception("Falha na tentativa de conversão.");
                }
            }
            else 
            {
                throw new Exception($"Falha na conexão: {response.StatusCode}");
            }
        }

        private class WebApiResponse
        {
            public string Result { get; set; } = string.Empty;
            public double ConversionRate { get; set; }
        }
    }
}
