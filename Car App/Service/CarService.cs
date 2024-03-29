﻿using Car_App.Controllers.DTOModels;
using Car_App.Data.Context;
using Car_App.Data.Models;
using Car_App.Data.Models.Filtering;
using Car_App.Data.Models.Sorting;
using Car_App.Service.Interface;
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

        public async Task<PagedResult<Car>> GetAllCarsAsync(PaginationParameters paginationParameters, CarFilter filter, CarSortBy sortBy, SortingDirection sortingDirection)
        {
            var query = _dbContext.Cars.Include(o => o.Owner).AsQueryable();

            query = ApplyFiltering(query, filter);
            query = SortCars(query, sortBy, sortingDirection);

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / paginationParameters.PageSize);
            var hasNextPage = (paginationParameters.Page < totalPages);

            var cars = await query.Skip((paginationParameters.Page - 1) * paginationParameters.PageSize)
                                  .Take(paginationParameters.PageSize)
                                  .ToListAsync();

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

        private IQueryable<Car> ApplyFiltering(IQueryable<Car> query, CarFilter filter)
        {
            if (filter.Id != null)
            {
                query = query.Where(c => c.Id == filter.Id);
            }

            if (!string.IsNullOrEmpty(filter.Make))
            {
                query = query.Where(c => c.Make == filter.Make);
            }

            if (filter.Year != null)
            {
                query = query.Where(c => c.Year == filter.Year);
            }

            if (filter.Distance != null)
            {
                query = query.Where(c => c.Distance == filter.Distance);
            }

            if (!string.IsNullOrEmpty(filter.FuelType))
            {
                query = query.Where(c => c.FuelType == filter.FuelType);
            }

            if (filter.Power != null)
            {
                query = query.Where(c => c.Power == filter.Power);
            }

            return query;
        }

        private IQueryable<Car> SortCars(IQueryable<Car> query, CarSortBy sortBy, SortingDirection sortDirection)
        {
            switch (sortBy)
            {
                case CarSortBy.Make:
                    if (sortDirection == SortingDirection.Ascending)
                        query = query.OrderBy(c => c.Make);
                    else
                        query = query.OrderByDescending(c => c.Make);
                    break;

                case CarSortBy.Model:
                    if (sortDirection == SortingDirection.Ascending)
                        query = query.OrderBy(c => c.Model);
                    else
                        query = query.OrderByDescending(c => c.Model);
                    break;

                case CarSortBy.Year:
                    if (sortDirection == SortingDirection.Ascending)
                        query = query.OrderBy(c => c.Year);
                    else
                        query = query.OrderByDescending(c => c.Year);
                    break;

                case CarSortBy.Distance:
                    if (sortDirection == SortingDirection.Ascending)
                        query = query.OrderBy(c => c.Distance);
                    else
                        query = query.OrderByDescending(c => c.Distance);
                    break;

                case CarSortBy.Power:
                    if (sortDirection == SortingDirection.Ascending)
                        query = query.OrderBy(c => c.Power);
                    else
                        query = query.OrderByDescending(c => c.Power);
                    break;

                default:
                    query = query.OrderBy(c => c.Year);
                    break;
            }

            return query;
        }

        public async Task<Car> GetCarByIdAsync(Guid id)
        {
            return await _dbContext.Cars.FindAsync(id);
        }

        public async Task CreateNewCarAsync(CarDto newCar)
        {
            var newOwner = await _dbContext.Owners.FindAsync(newCar.OwnerId);
            Car Item = new Car
            {
                Title = newCar.Title,
                Make = newCar.Make,
                Model = newCar.Model,
                Year = newCar.Year,
                Distance = newCar.Distance,
                FuelType = newCar.FuelType,
                Power = newCar.Power,
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

        public async Task<bool> UpdateCarAsync(Guid id, CarDto newCar)
        {
            var car = await _dbContext.Cars.FindAsync(id);
            if (car != null)
            {
                car.Title = newCar.Title;
                car.Make = newCar.Make;
                car.Model = newCar.Model;
                car.Year = newCar.Year;
                car.Distance = newCar.Distance;
                car.FuelType = newCar.FuelType;
                car.Power = newCar.Power;

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