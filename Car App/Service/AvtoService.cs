using Car_App.Controllers.DTOModels;
using Car_App.Data.Context;
using Car_App.Data.Models;
using Car_App.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Car_App.Services
{
    public class AvtoService : IAvtoService
    {
        private readonly DatabaseContext _dbContext;

        public AvtoService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Avto>> GetAllCarsAsync()
        {
            return await _dbContext.Cars.ToListAsync();
        }

        public async Task<Avto> GetCarByIdAsync(Guid id)
        {
            return await _dbContext.Cars.FindAsync(id);
        }

        public async Task<IEnumerable<Avto>> GetAvto([FromQuery] int count)
        {
            Avto[] avto =
            {
                new() { Title = "Citroen C-Elysee Seduction HDi 92 BVM"},
                new() { Title = "Renault Twingo 1.2 16V .TEMPOMAT.."},
                new() { Title = "Audi Q3 35 TFSI S-Tronic Advanced 150KM COCKPIT Full LED"}

            };

            return avto.Take(count);
        }

        public async Task CreateNewAvtoAsync(AvtoDTO newAvto)
        {
            Avto Item = new Avto
            {
                Title = newAvto.Title,
                Make = newAvto.Make,
                Model = newAvto.Model,
                Year = newAvto.Year,
                Mileage = newAvto.Mileage,
                FuelType = newAvto.FuelType,
                Power = newAvto.Power
            };

            await _dbContext.Cars.AddAsync(Item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteAvtoAsync(Guid id)
        {
            var car = await _dbContext.Cars.FindAsync(id);

            if (car != null)
            {
                _dbContext.Cars.Remove(car);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateAvtoAsync(Guid id, AvtoDTO newAvto)
        {
            var car = await _dbContext.Cars.FindAsync(id);
            if (car != null)
            {
                car.Title = newAvto.Title;
                car.Make = newAvto.Make;
                car.Model = newAvto.Model;
                car.Year = newAvto.Year;
                car.Mileage = newAvto.Mileage;
                car.FuelType = newAvto.FuelType;
                car.Power = newAvto.Power;

                _dbContext.Cars.Update(car);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
