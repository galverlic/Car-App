using Car_App.Context;
using Car_App.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace Car_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvtoController : ControllerBase
    {

        private readonly DatabaseContext _dbContext;

        public AvtoController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Cars

        // getter search  po vseh avtih
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Avto>>> GetCars()
            {
            if (_dbContext.Cars == null)

            {
                return NotFound();
            }
            return await _dbContext.Cars.ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Avto>> GetCar(int id)
        {
            var car = await _dbContext.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }


        public ActionResult GetAvto([FromQuery]int count)
        {
            Avto[] avto =
            {
                new() { Title = "Citroen C-Elysee Seduction HDi 92 BVM"},
                new() { Title = "Renault Twingo 1.2 16V .TEMPOMAT.."},
                new() { Title = "Audi Q3 35 TFSI S-Tronic Advanced 150KM COCKPIT Full LED"}

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
