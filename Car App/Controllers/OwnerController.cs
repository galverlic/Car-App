using Car_App.Controllers.DTOModels;
using Car_App.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Car_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase

    {
        private readonly ICarService _avtoService;

        private readonly IOwnerService _ownerService;
        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        //GET ALL OWNERS
        [HttpGet("owners")]
        public async Task<ActionResult<List<OwnerDTO>>> GetAllOwnersWithCars()
        {
            var owners = await _ownerService.GetAllOwnersAsync();

            var ownerDTOs = owners.Select(owner =>
            {
                if (owner == null)
                {
                    return null;
                }

                return new OwnerDTO
                {
                    Id = owner.Id,
                    FirstName = owner.FirstName,
                    LastName = owner.LastName,
                    Emso = owner.Emso,
                    TelephoneNumber = owner.TelephoneNumber,
                    CarIds = owner.Cars.Select(car => car.Id).ToList()
                };
            }).ToList();

            return ownerDTOs;
        }

        // GET A CAR BY IT'S OWNER'S ID

        [HttpGet("{ownerId}/cars")]
        public async Task<ActionResult<List<CarDTO>>> GetCarsByOwnerId(Guid ownerId)
        {
            var owner = await _ownerService.GetOwnerWithCarsByIdAsync(ownerId);

            if (owner == null || owner.Cars == null || !owner.Cars.Any())
            {
                return NotFound();
            }

            var AvtoDTOs = owner.Cars.Select(car => new CarDTO
            {
                //Id = car.Id,
                Title = car.Title,
                Make = car.Make,
                Model = car.Model,
                Year = car.Year,
                Distance = (int)car.Distance,
                FuelType = car.FuelType,
                Power = car.Power,
                OwnerId = car.OwnerId
            }).ToList();

            return AvtoDTOs;
        }




        // CREATE NEW OWNER
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
