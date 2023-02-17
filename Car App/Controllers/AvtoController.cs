using Car_App.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvtoController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetAvto([FromQuery]int count)
        {
            Avto[] avto =
            {
                new() { Title = "Citroen C-Elysee Seduction HDi 92 BVM"},
                new() { Title = "Renault Twingo 1.2 16V .TEMPOMAT.."},
                new() { Title = "Audi Q3 35 TFSI S-Tronic Advanced 150KM COCKPIT Full LED" }

            };
            
            return Ok(avto.Take(count));
        }

        [HttpPost]
        public ActionResult CreateNewAvto([FromBody] Avto newAvto)

        {
            // validate and save to database
            bool badThingsHappened = false;

            if (badThingsHappened)
                return BadRequest();
            return Created("", newAvto);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteAvto(string id)
        {
            bool badThingsHappened = false;

            if (badThingsHappened)
                return BadRequest();

            return NoContent();
        }
            
            

            
        
    }
}
