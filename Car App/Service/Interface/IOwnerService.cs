using Car_App.Controllers.DTOModels;
using Car_App.Data.Models;
using Car_App.Data.Models.NewFolder;

namespace Car_App.Service.Interface
{
    public interface IOwnerService
    {
        public Task<PagedResult<Owner>> GetAllOwnersAsync(PaginationParameters paginationParameters, OwnerFilter filter);
        public Task<Owner> GetOwnerByIdAsync(Guid id);
        public Task<Owner> GetOwnerWithCarsByIdAsync(Guid ownerId);

        public Task CreateNewOwnerAsync(OwnerDto newOwner);
        public Task<bool> DeleteOwnerAsync(Guid id);
        public Task<bool> UpdateOwnerAsync(Guid id, OwnerDto newOwner);
        public Task<IEnumerable<Car>> GetCarsByOwnerIdAsync(Guid ownerId);
    }
}
