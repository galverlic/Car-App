using Car_App.Controllers.DTOModels;
using Car_App.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Car_App.Service.Interface
{
    public interface ICarService
    {
        Task<PagedResult<Car>> GetAllCarsAsync(PaginationParameters paginationParameters, CarFilter filter, CarSortBy sortBy);
        Task<Car> GetCarByIdAsync(Guid id);
        Task<IEnumerable<Car>> GetCar([FromQuery] int count);
        Task CreateNewCarAsync(CarDto newAvto);
        Task<bool> DeleteCarAsync(Guid id);
        Task<bool> UpdateCarAsync(Guid id, CarDto newAvto);
        Task<int> GetTotalCountAsync(string make = null);
    }
}
