using Car_App.Controllers.DTOModels;
using Car_App.Data.Models;
using Car_App.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Car_App.Controllers
{
    [Route("owner")]
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

        /// <summary>
        /// Gets all owners
        /// </summary>
        /// <returns>A paged list of owners matching the filter criteria</returns>
        [HttpGet("owners")]
        public async Task<ActionResult<PagedResult<OwnerDto>>> GetOwners([FromQuery] string firstName, [FromQuery] PaginationParameters paginationParameters)
        {
            var result = await _ownerService.GetAllOwnersAsync(paginationParameters, firstName);



            return Ok(result);
        }

        //public async Task<ActionResult<List<OwnerDto>>> GetAllOwnersWithCars()
        //{
        //    var owners = await _ownerService.GetAllOwnersAsync();

        //    var ownerDTOs = owners.Select(owner =>
        //    {
        //        if (owner == null)
        //        {
        //            return null;
        //        }

        //        return new OwnerDto
        //        {
        //            //Id = owner.Id,
        //            FirstName = owner.FirstName,
        //            LastName = owner.LastName,
        //            Emso = owner.Emso,
        //            TelephoneNumber = owner.TelephoneNumber,
        //            CarIds = owner.Cars.Select(car => car.Id).ToList()
        //        };
        //    }).ToList();

        //    return ownerDTOs;
        //}

        // GET A CAR BY IT'S OWNER'S ID
        /// <summary>
        /// Gets all cars for an owner
        /// </summary>
        /// <returns>A list of cars owned by the specified owner</returns>

        [HttpGet("{owner-id}/cars")]
        public async Task<ActionResult<List<CarDto>>> GetCarsByOwnerId(Guid ownerId)
        {
            var owner = await _ownerService.GetOwnerWithCarsByIdAsync(ownerId);

            if (owner == null || owner.Cars == null || !owner.Cars.Any())
            {
                return NotFound();
            }

            var CarDtos = owner.Cars.Select(car => new CarDto
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

            return CarDtos;
        }




        // CREATE NEW OWNER

        /// <summary>
        /// Creates a new owner
        /// </summary>
        /// <returns>The details of the newly created owner</returns>
        [HttpPost("create-new-owner")]
        public async Task<ActionResult> CreateNewOwner([FromBody] OwnerDto newOwner)
        {
            await _ownerService.CreateNewOwnerAsync(newOwner);
            return Created("", newOwner);
        }

        // DELETE OWNER BY ID

        /// <summary>
        /// Deletes an owner by ID
        /// </summary>
        /// <returns>A status code indicating the result of the operation</returns>
        [HttpDelete("delete-owner/{id}")]
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

        /// <summary>
        /// Updates an owner by ID
        /// </summary>

        /// <returns>A status code indicating the result of the operation</returns>
        [HttpPut("update-owner/{id}")]
        public async Task<ActionResult> UpdateOwner([FromBody] OwnerDto newOwner, Guid id)
        {
            await _ownerService.UpdateOwnerAsync(id, newOwner);
            return Ok(newOwner);
        }
    }
}
