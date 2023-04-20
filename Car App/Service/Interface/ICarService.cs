using Car_App.Controllers.DTOModels;
using Car_App.Data.Models;
using Car_App.Data.Models.Filtering;
using Car_App.Data.Models.Sorting;

namespace Car_App.Service.Interface
{
    public interface ICarService
    {
        /// <summary>
        /// Retrieves a paginated and sorted list of cars based on specified filtering criteria.
        /// </summary>
        /// <param name="paginationParameters"> Parameters for pagination</param>
        /// <param name="filter">Filters data by chosen filters</param>
        /// <param name="sortBy">Sorts data by chosen sorting parameter</param>
        /// <param name="sortingDirection">Sorts data in the chosen direction(asc/desc)</param>
        /// <returns></returns>
        Task<PagedResult<Car>> GetAllCarsAsync(PaginationParameters paginationParameters, CarFilter filter, CarSortBy sortBy, SortingDirection sortingDirection);
        /// <summary>
        /// Retrieves a car by it's owner's ID
        /// </summary>
        /// <param name="id">Owner Id is the input paramater</param>
        /// <returns></returns>
        Task<Car> GetCarByIdAsync(Guid id);
        /// <summary>
        /// Creates a new car
        /// </summary>
        /// <param name="newAvto"></param>
        /// <returns></returns>
        Task CreateNewCarAsync(CarDto newAvto);
        /// <summary>
        /// Deletes a car
        /// </summary>
        /// <param name="id">Car id is the input parameter</param>
        /// <returns></returns>
        Task<bool> DeleteCarAsync(Guid id);
        /// <summary>
        /// Updates data of a car 
        /// </summary>
        /// <param name="id"> The input parameter is the car ID</param>
        /// <param name="newAvto"></param>
        /// <returns></returns>
        Task<bool> UpdateCarAsync(Guid id, CarDto newAvto);
    }
}
