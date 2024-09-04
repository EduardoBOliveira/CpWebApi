using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;
using WebApi.Model;

namespace WebApi.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class WebApiController : ControllerBase, IExchangeController
    {
        private readonly HttpClient _httpClient;
        

        public WebApiController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("rate")]
        [Tags("Consulta de taxas atualizadas")]
        [ProducesResponseType(typeof(WebApiSettings), 200)]
        [ProducesResponseType(404)]
        public async Task<JsonResult> GetExchangeRate()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("https://v6.exchangerate-api.com/v6/89b7db2aefb75a88078b8a0f/pair/USD/BRL");

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    return new JsonResult(new { success = true, data = responseData });
                }
                else
                {
                    return new JsonResult(new { success = false, error = $"Erro na requisição: {response.StatusCode}" });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, error = $"Ocorreu um erro: {ex.Message}" });
            }
        }
    }
}
