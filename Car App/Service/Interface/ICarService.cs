using Car_App.Controllers.DTOModels;
using Car_App.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Car_App.Service.Interface
{
    public interface ICarService
    {
        Task<IEnumerable<Car>> GetAllCarsAsync(string make = null, int page = 1, int pageSize = 10);
        Task<Car> GetCarByIdAsync(Guid id);
        Task<IEnumerable<Car>> GetCar([FromQuery] int count);
        Task CreateNewCarAsync(CarDTO newAvto);
        Task<bool> DeleteCarAsync(Guid id);
        Task<bool> UpdateCarAsync(Guid id, CarDTO newAvto);
        Task<int> GetTotalCountAsync(string make = null);
    }
}
