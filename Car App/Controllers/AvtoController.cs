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

        public AvtoController(IAvtoService avtoService)
        {
            _avtoService = avtoService;
        }

        // GET: api/Cars

        // getter search  po vseh avtih
        [HttpGet]



        public async Task<ActionResult<IEnumerable<Avto>>> GetCars()
        {
            return Ok(await _avtoService.GetAllCarsAsync());

        }

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

        [HttpPost]
        public async Task<ActionResult> CreateNewAvto([FromBody] AvtoDTO newAvto)

        {

            await _avtoService.CreateNewAvtoAsync(newAvto);


            return Created("", newAvto);
        }
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

        [HttpPut("Update/{id}")]
        public async Task<ActionResult> UpdateAvto([FromBody] AvtoDTO newAvto, Guid id)
        {
            await _avtoService.UpdateAvtoAsync(id, newAvto);
            return Ok(newAvto);
        }





    }
}
