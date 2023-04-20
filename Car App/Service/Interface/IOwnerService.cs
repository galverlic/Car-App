using Car_App.Controllers.DTOModels;
using Car_App.Data.Models;
using Car_App.Data.Models.NewFolder;
using Car_App.Data.Models.Sorting;

namespace Car_App.Service.Interface
{
    public interface IOwnerService
    {
        /// <summary>
        /// Retrieves a list of all owners, their data and their car data
        /// </summary>
        /// <param name="paginationParameters"> Pagination parameters can be set</param>
        /// <param name="filter"> Filters retrieved data based on chosen filter</param>
        /// <param name="sortBy"> Sorts data by chosen sorting parameter</param>
        /// <param name="sortingDirection">Sorts data either in ascending or descending direction</param>
        /// <returns></returns>
        public Task<PagedResult<Owner>> GetAllOwnersAsync(PaginationParameters paginationParameters, OwnerFilter filter, OwnerSortBy sortBy, SortingDirection sortingDirection);
        /// <summary>
        /// Retrieves data of an owner based on his ID
        /// </summary>
        /// <param name="id"> Input parameter is the owner's ID</param>
        /// <returns></returns>
        public Task<Owner> GetOwnerByIdAsync(Guid id);
        /// <summary>
        /// Creates a new user(owner)
        /// </summary>
        /// <param name="registerOwnerDto"></param>
        /// <returns></returns>
        public Task RegisterAsync(RegisterOwnerDto registerOwnerDto);
        /// <summary>
        /// Authenticates a user, based on username and password validation with tokens(bearer)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<AuthenticateResponseDto> AuthenticateAsync(AuthenticateRequestDto model);
        /// <summary>
        /// Retrieves car data of owner based on their ID
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        public Task<Owner> GetOwnerWithCarsByIdAsync(Guid ownerId);
        /// <summary>
        /// Deletes all owner data and their car data 
        /// </summary>
        /// <param name="id">The input parameter is the owner ID </param>
        /// <returns></returns>
        public Task<bool> DeleteOwnerAsync(Guid id);
        /// <summary>
        /// Changes the owner data 
        /// </summary>
        /// <param name="id">The input parameter is the owner's ID</param>
        /// <param name="newOwner"></param>
        /// <returns></returns>
        public Task<bool> UpdateOwnerAsync(Guid id, OwnerDto newOwner);
        /// <summary>
        /// Retrieves all car data based on the owner's ID
        /// </summary>
        /// <param name="ownerId">The input parameter is owner's ID</param>
        /// <returns></returns>
        public Task<IEnumerable<Car>> GetCarsByOwnerIdAsync(Guid ownerId);
    }
}
