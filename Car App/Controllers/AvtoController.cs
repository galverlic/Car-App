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
        public ActionResult GetAvto()
        {
            string[] avto = { "Volkswagen, Golf, 2015", "BMW, 520i, 2011", "Renault, Clio, 2013" };

            if (avto.Any())
                return NotFound();
            return Ok(avto);
        }

        [HttpPost]
        public ActionResult PostAvto([FromBody] string carString)

        {

            string[] carData = carString.Split(',');

            if (carData.Length != 3)
            {
                return BadRequest("The request body should contain a comma-separated string with the car make, model, and year");
            }

            var newCar = new Avto { Make = carData[0], Model = carData[1], Year = int.Parse(carData[2]) };

            // Add the new car to your database or data store
            // ...

            // Return the newly created car with a 201 Created status code
            return CreatedAtAction(nameof(GetAvto), new { id = newCar.Id }, newCar);
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
