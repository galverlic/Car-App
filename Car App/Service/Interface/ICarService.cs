using Car_App.Controllers.DTOModels;
using Car_App.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Car_App.Service.Interface
{
    public interface ICarService
    {

        public Task<IEnumerable<Car>> GetAllCarsAsync(string make = null, int page = 1, int pageSize = 10);

        public Task<Car> GetCarByIdAsync(Guid id);

        public Task<IEnumerable<Car>> GetCar([FromQuery] int count);

        public Task CreateNewCarAsync(CarDTO newAvto);
        public Task<bool> DeleteCarAsync(Guid id);
        public Task<bool> UpdateCarAsync(Guid id, CarDTO newAvto);

    }

}
