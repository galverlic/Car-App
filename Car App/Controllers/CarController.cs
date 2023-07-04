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
        [HttpGet("cars")]
        public async Task<ActionResult<PagedResult<Car>>> GetCars([FromQuery] PaginationParameters paginationParameters, [FromQuery] CarFilter filter, CarSortBy sortBy, SortingDirection sortingDirection)
        {
            var result = await _carService.GetAllCarsAsync(paginationParameters, filter, sortBy, sortingDirection);

            return Ok(result);
        }

        // getter find car by ID
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
        [HttpPost]

        public async Task<ActionResult> CreateNewCar([FromBody] CarDto newCar)
        {
            await _carService.CreateNewCarAsync(newCar);
            return Created("", newCar);
        }

        // delete a car by ID

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<bool>> DeleteCar(Guid id)
        {

            if (await _carService.DeleteCarAsync(id))

                return Ok(true);

            return NotFound();
        }
    


    // update a car by it's ID

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
