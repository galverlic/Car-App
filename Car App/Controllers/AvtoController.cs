using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvtoController : ControllerBase
    {
        [HttpGet]
        public string[] GetAvto()
        {
            string[] avto = { "Volkswagen, Golf, 2015", "BMW, 520i, 2011", "Renault, Clio, 2013" };
            return avto;
        }
    }
}
