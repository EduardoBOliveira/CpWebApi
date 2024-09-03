using WebApi.Interfaces;
using WebApi.Service;
using WebApi.Model;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurar a injeção de dependência para WebApiSettings
builder.Services.Configure<WebApiSettings>(
    builder.Configuration.GetSection("ExchangeRateAPI"));  

// Configurar o HttpClient com injeção de dependência para o serviço de conversão
builder.Services.AddHttpClient<IConversionRate, WebApiConversionRateService>();

// Adicionar controladores
builder.Services.AddControllers();

// Configurar o Swagger com descrição detalhada
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Exchange Rate API",
        Description = "API para obter a taxa de câmbio do dólar (USD) em relação ao real (BRL)."
    });


});

var app = builder.Build();

// Configuração do Swagger para ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Exchange Rate API V1");
        c.RoutePrefix = string.Empty;  // Define a interface do Swagger na raiz da aplicação
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
