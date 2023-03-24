using Car_App.Controllers.DTOModels;
using Car_App.Data.Models;
using Car_App.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Car_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _avtoService;

        public CarController(ICarService avtoService)
        {
            _avtoService = avtoService;
        }

        // GET: api/Cars

        // getter search  po vseh avtih
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars([FromQuery] string make, [FromQuery] PaginationParameters pagination)
        {
            var cars = await _avtoService.GetAllCarsAsync(make, pagination.Page, pagination.PageSize);
            return Ok(cars);
        }



        // getter find car by ID
        [HttpGet("GetCarById/{id}")]
        public async Task<ActionResult<Car>> GetCar(Guid id)
        {
            var car = await _avtoService.GetCarByIdAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        // getter number of cars
        [HttpGet("GetNumberOfCars/{count}")]
        public ActionResult GetAvto([FromQuery] int count)
        {
            Car[] avto =
            {
                new() { Title = "Citroen C-Elysee Seduction HDi 92 BVM"},
                new() { Title = "Renault Twingo 1.2 16V .TEMPOMAT.."},
                new() { Title = "Audi Q3 35 TFSI S-Tronic Advanced 150KM COCKPIT Full LED"}

            };

            return Ok(avto.Take(count));
        }

        // create a new car
        [HttpPost]
        public async Task<ActionResult> CreateNewCar([FromBody] CarDTO newAvto)
        {
            await _avtoService.CreateNewCarAsync(newAvto);
            return Created("", newAvto);
        }

        // delete a car by ID
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteCar(Guid id)
        {
            var car = await _avtoService.DeleteCarAsync(id);
            if (car == true)
            {
                return Ok(car);
            }
            else
            {
                return NotFound(HttpStatusCode.NotModified);
            }
        }

        // update a car by it's ID
        [HttpPut("Update/{id}")]
        public async Task<ActionResult> UpdateCar([FromBody] CarDTO newCar, Guid id)
        {
            await _avtoService.UpdateCarAsync(id, newCar);
            return Ok(newCar);
        }

    }
}
