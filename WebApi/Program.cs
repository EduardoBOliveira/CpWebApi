using WebApi.Interfaces;
using WebApi.Service;
using WebApi.Model;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<WebApiSettings>(
    builder.Configuration.GetSection("ExchangeRateAPI"));  

builder.Services.AddHttpClient<IConversionRate, WebApiConversionRateService>();

builder.Services.AddControllers();

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Exchange Rate API V1");
        c.RoutePrefix = string.Empty;  
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
