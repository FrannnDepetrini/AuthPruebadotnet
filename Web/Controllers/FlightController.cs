using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class FlightController : ControllerBase
    {
      [HttpGet]
      public string Saludar()
        {
            return "hola Admin";
        }
    }
}
