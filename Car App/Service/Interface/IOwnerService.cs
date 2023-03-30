using Car_App.Controllers.DTOModels;
using Car_App.Data.Models;


namespace Car_App.Service.Interface
{
    public interface IOwnerService
    {
        public Task<PagedResult<Owner>> GetAllOwnersAsync(PaginationParameters paginationParameteres, string firstName = null);
        public Task<Owner> GetOwnerByIdAsync(Guid id);
        public Task<Owner> GetOwnerWithCarsByIdAsync(Guid ownerId);

        public Task CreateNewOwnerAsync(OwnerDto newOwner);
        public Task<bool> DeleteOwnerAsync(Guid id);
        public Task<bool> UpdateOwnerAsync(Guid id, OwnerDto newOwner);
        public Task<IEnumerable<Car>> GetCarsByOwnerIdAsync(Guid ownerId);
    }
}
