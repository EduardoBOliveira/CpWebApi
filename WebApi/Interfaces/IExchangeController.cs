using Microsoft.AspNetCore.Mvc;

namespace WebApi.Interfaces
{
    public interface IExchangeController
    {
        Task<JsonResult> GetExchangeRate();
    }
}