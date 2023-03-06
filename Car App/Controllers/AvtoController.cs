using Car_App.Controllers.DTOModels;
using Car_App.Data.Models;
using Car_App.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Car_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvtoController : ControllerBase
    {
        private readonly IAvtoService _avtoService;
        private readonly IOwnerService _ownerService;

        public AvtoController(IAvtoService avtoService, IOwnerService ownerService)
        {
            _avtoService = avtoService;
            _ownerService = ownerService;
        }

        // GET: api/Cars

        // getter search  po vseh avtih
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Avto>>> GetCars()
        {
            return Ok(await _avtoService.GetAllCarsAsync());
        }

        // getter find car by ID
        [HttpGet("GetCarById/{id}")]
        public async Task<ActionResult<Avto>> GetCar(Guid id)
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
            Avto[] avto =
            {
                new() { Title = "Citroen C-Elysee Seduction HDi 92 BVM"},
                new() { Title = "Renault Twingo 1.2 16V .TEMPOMAT.."},
                new() { Title = "Audi Q3 35 TFSI S-Tronic Advanced 150KM COCKPIT Full LED"}

            };

            return Ok(avto.Take(count));
        }

        // create a new car
        [HttpPost]
        public async Task<ActionResult> CreateNewAvto([FromBody] AvtoDTO newAvto)
        {
            await _avtoService.CreateNewAvtoAsync(newAvto);
            return Created("", newAvto);
        }

        // delete a car by ID
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteAvto(Guid id)
        {
            var car = await _avtoService.DeleteAvtoAsync(id);
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
        public async Task<ActionResult> UpdateAvto([FromBody] AvtoDTO newAvto, Guid id)
        {
            await _avtoService.UpdateAvtoAsync(id, newAvto);
            return Ok(newAvto);
        }

        // get a car by it's owner
        [HttpGet("{id}/owner")]
        public async Task<ActionResult<OwnerDTO>> GetCarOwner(Guid id)
        {
            var car = await _avtoService.GetCarByIdAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var owner = await _ownerService.GetOwnerByIdAsync(car.OwnerId);

            if (owner == null)
            {
                return NotFound();
            }

            return new OwnerDTO
            {
                Id = owner.Id,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                Emso = owner.Emso,
                TelephoneNumber = owner.TelephoneNumber,
                CarIds = owner.Cars.Select(c => c.Id).ToList()
            };
        }
        [HttpGet("GetCarsByOwnerId/{ownerId}")]
        public async Task<ActionResult<IEnumerable<Avto>>> GetCarsByOwnerId(Guid ownerId)
        {
            var cars = await _ownerService.GetCarsByOwnerIdAsync(ownerId);
            if (cars == null || cars.Count() == 0)
            {
                return NotFound();
            }

            return Ok(cars);

        }

        // CREATE OWNER
        [HttpPost("CreateNewOwner")]
        public async Task<ActionResult> CreateNewOwner([FromBody] OwnerDTO newOwner)
        {
            await _ownerService.CreateNewOwnerAsync(newOwner);
            return Created("", newOwner);
        }

        // DELETE OWNER BY ID
        [HttpDelete("DeleteOwner/{id}")]
        public async Task<ActionResult> DeleteOwner(Guid id)
        {
            var owner = await _ownerService.DeleteOwnerAsync(id);
            if (owner == true)
            {
                return Ok(owner);
            }
            else
            {
                return NotFound(HttpStatusCode.NotModified);
            }
        }

        // UPDATE OWNER BY ID
        [HttpPut("UpdateOwner/{id}")]
        public async Task<ActionResult> UpdateOwner([FromBody] OwnerDTO newOwner, Guid id)
        {
            await _ownerService.UpdateOwnerAsync(id, newOwner);
            return Ok(newOwner);
        }
    }
}
