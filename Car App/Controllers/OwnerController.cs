namespace Car_App.Controllers

{
    using Car_App.Controllers.DTOModels;
    using Car_App.Data.Models;
    using Car_App.Data.Models.NewFolder;
    using Car_App.Data.Models.Sorting;
    using Car_App.Service.Interface;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;


    [Authorize]

    [Route("owner")]
    [ApiController]
    public class OwnerController : ControllerBase

    {
        private readonly ICarService _carService;

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
        public async Task<ActionResult<PagedResult<Owner>>> GetOwners([FromQuery] PaginationParameters paginationParameters, [FromQuery] OwnerFilter filter, OwnerSortBy sortBy, SortingDirection sortingDirection)
        {
            var result = await _ownerService.GetAllOwnersAsync(paginationParameters, filter, sortBy, sortingDirection);



            return Ok(result);
        }


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

        // REGISTER NEW OWNER

        /// <summary>
        /// Register a new owner
        /// </summary>
        /// <returns>The details of the newly created owner</returns>

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterOwnerDto registerOwnerDto)
        {
            await _ownerService.RegisterAsync(registerOwnerDto);
            return Ok();
        }

        // LOGIN OWNER

        /// <summary>
        /// Login with username and password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequestDto model)
        {
            var response = await _ownerService.AuthenticateAsync(model);
            return Ok(response);
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
