using Car_App.Controllers.DTOModels;
using Car_App.Data.Models;
using Car_App.Data.Models.Filtering;
using Car_App.Data.Models.Sorting;
using Car_App.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Car_App.Controllers
{
    [Authorize]
    [ApiController]

    [Route("car")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        // GET: api/Cars

        /// <summary>
        /// Gets all cars
        /// </summary>
        /// <returns>A list of all cars in the database</returns>
        [HttpGet("cars")]
        public async Task<ActionResult<PagedResult<Car>>> GetCars([FromQuery] PaginationParameters paginationParameters, [FromQuery] CarFilter filter, CarSortBy sortBy, SortingDirection sortingDirection)
        {
            var result = await _carService.GetAllCarsAsync(paginationParameters, filter, sortBy, sortingDirection);

            return Ok(result);
        }



        // getter find car by ID

        /// <summary>
        /// Finds car by it's id
        /// </summary>
        /// <returns>One car matching it's id</returns>
        [HttpGet("get-car-by-id/{id}")]

        public async Task<ActionResult<Car>> GetCar(Guid id)
        {
            var car = await _carService.GetCarByIdAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }


        // create a new car
        /// <summary>
        /// Create a new car
        /// </summary>
        /// <returns>New car added to the database</returns>
        [HttpPost]

        public async Task<ActionResult> CreateNewCar([FromBody] CarDto newCar)
        {
            await _carService.CreateNewCarAsync(newCar);
            return Created("", newCar);
        }

        // delete a car by ID
        /// <summary>
        /// Delete a car
        /// </summary>
        /// <returns>Deletes a car from the database</returns>
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteCar(Guid id)
        {
            if (await _carService.DeleteCarAsync(id))
            {
                return Ok();
            }
            else
            {
                return NotFound(HttpStatusCode.NotModified);
            }
        }

        // update a car by it's ID
        /// <summary>
        /// Update a car
        /// </summary>
        /// <returns>Updates a car</returns>
        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateCar([FromBody] CarDto newCar, Guid id)
        {
            if (await _carService.UpdateCarAsync(id, newCar))
                return Ok(newCar);
            else
                return NotFound(HttpStatusCode.NotModified);
        }

    }
}
