using Car_App.Controllers.DTOModels;
using Car_App.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Car_App.Service.Interface
{
    public interface ICarService
    {
        Task<PagedResult<Car>> GetAllCarsAsync(PaginationParameters paginationParameters, string make = null);
        Task<Car> GetCarByIdAsync(Guid id);
        Task<IEnumerable<Car>> GetCar([FromQuery] int count);
        Task CreateNewCarAsync(CarDTO newAvto);
        Task<bool> DeleteCarAsync(Guid id);
        Task<bool> UpdateCarAsync(Guid id, CarDTO newAvto);
        Task<int> GetTotalCountAsync(string make = null);
    }
}
