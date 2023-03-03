using Car_App.Controllers.DTOModels;
using Car_App.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Car_App.Service.Interface
{
    public interface IAvtoService
    {

        public Task<IEnumerable<Avto>> GetAllCarsAsync();
        public Task<Avto> GetCarByIdAsync(Guid id);

        public Task<IEnumerable<Avto>> GetAvto([FromQuery] int count);

        public Task CreateNewAvtoAsync(AvtoDTO newAvto);
        public Task<bool> DeleteAvtoAsync(Guid id);
        public Task<bool> UpdateAvtoAsync(Guid id, AvtoDTO newAvto);
    }

}
