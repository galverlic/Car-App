using Car_App.Controllers.DTOModels;
using Car_App.Data.Context;
using Car_App.Data.Models;
using Car_App.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Car_App.Services
{
    public class CarService : ICarService
    {
        private readonly DatabaseContext _dbContext;

        public CarService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedResult<Car>> GetAllCarsAsync(PaginationParameters paginationParameters, string make = null)
        {
            var query = _dbContext.Cars.AsQueryable();

            if (!string.IsNullOrEmpty(make))
            {
                query = query.Where(c => c.Make == make);
            }

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)paginationParameters.PageSize);
            var hasNextPage = totalPages > paginationParameters.Page;

            if (paginationParameters.Page < 1)
            {
                paginationParameters.Page = 1;
            }

            if (paginationParameters.Page > totalPages)
            {
                paginationParameters.Page = totalPages;
            }

            var cars = await query.Skip((paginationParameters.Page - 1) * paginationParameters.PageSize).Take(paginationParameters.PageSize).ToListAsync();

            return new PagedResult<Car>()
            {
                Results = cars,
                CurrentPage = paginationParameters.Page,
                TotalPages = totalPages,
                TotalCount = totalCount,
                PageSize = paginationParameters.PageSize,
                HasNextPage = hasNextPage
            };
        }


        public async Task<Car> GetCarByIdAsync(Guid id)
        {
            return await _dbContext.Cars.FindAsync(id);
        }

        public async Task<IEnumerable<Car>> GetCar([FromQuery] int count)
        {
            Car[] avto =
            {
                new() { Title = "Citroen C-Elysee Seduction HDi 92 BVM"},
                new() { Title = "Renault Twingo 1.2 16V .TEMPOMAT.."},
                new() { Title = "Audi Q3 35 TFSI S-Tronic Advanced 150KM COCKPIT Full LED"}

            };

            return avto.Take(count);
        }

        public async Task CreateNewCarAsync(CarDTO newAvto)
        {
            var newOwner = await _dbContext.Owners.FindAsync(newAvto.OwnerId);
            Car Item = new Car
            {
                Title = newAvto.Title,
                Make = newAvto.Make,
                Model = newAvto.Model,
                Year = newAvto.Year,
                Distance = newAvto.Distance,
                FuelType = newAvto.FuelType,
                Power = newAvto.Power,
                OwnerId = newOwner.Id
            };

            await _dbContext.Cars.AddAsync(Item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteCarAsync(Guid id)
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

        public async Task<bool> UpdateCarAsync(Guid id, CarDTO newAvto)
        {
            var car = await _dbContext.Cars.FindAsync(id);
            if (car != null)
            {
                car.Title = newAvto.Title;
                car.Make = newAvto.Make;
                car.Model = newAvto.Model;
                car.Year = newAvto.Year;
                car.Distance = newAvto.Distance;
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

        public async Task<int> GetTotalCountAsync(string make = null)
        {
            var query = _dbContext.Cars.AsQueryable();

            if (!string.IsNullOrEmpty(make))
            {
                query = query.Where(c => c.Make == make);
            }

            var totalCount = await query.CountAsync();

            return totalCount;
        }
    }
}