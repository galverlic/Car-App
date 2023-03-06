using Car_App.Controllers.DTOModels;
using Car_App.Data.Models;


namespace Car_App.Service.Interface
{
    public interface IOwnerService
    {
        public Task<IEnumerable<Owner>> GetAllOwnersAsync();
        public Task<Owner> GetOwnerByIdAsync(Guid id);
        public Task CreateNewOwnerAsync(OwnerDTO newOwner);
        public Task<bool> DeleteOwnerAsync(Guid id);
        public Task<bool> UpdateOwnerAsync(Guid id, OwnerDTO newOwner);
        public Task<IEnumerable<Avto>> GetCarsByOwnerIdAsync(Guid ownerId);
    }
}
