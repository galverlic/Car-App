using Car_App.Controllers.DTOModels;
using Car_App.Data.Context;
using Car_App.Data.Models;
using Car_App.Service.Interface;
using Microsoft.EntityFrameworkCore;

public class OwnerService : IOwnerService
{
    private readonly DatabaseContext _dbContext;

    public OwnerService(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Owner>> GetAllOwnersAsync()
    {
        return await _dbContext.Owners.ToListAsync();
    }

    public async Task<Owner> GetOwnerByIdAsync(Guid id)
    {
        return await _dbContext.Owners.FindAsync(id);
    }

    public async Task CreateNewOwnerAsync(OwnerDTO newOwner)
    {
        Owner owner = new Owner
        {
            FirstName = newOwner.FirstName,
            LastName = newOwner.LastName,
            Emso = newOwner.Emso,
            TelephoneNumber = newOwner.TelephoneNumber
        };

        if (newOwner.CarIds != null)
        {
            foreach (Guid carId in newOwner.CarIds)
            {
                Avto car = await _dbContext.Cars.FindAsync(carId);
                if (car != null)
                {
                    owner.Cars.Add(car);
                }
            }
        }

        await _dbContext.Owners.AddAsync(owner);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteOwnerAsync(Guid id)
    {
        var owner = await _dbContext.Owners.FindAsync(id);

        if (owner != null)
        {
            _dbContext.Owners.Remove(owner);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> UpdateOwnerAsync(Guid id, OwnerDTO newOwner)
    {
        var owner = await _dbContext.Owners.FindAsync(id);
        if (owner != null)
        {
            owner.FirstName = newOwner.FirstName;
            owner.LastName = newOwner.LastName;
            owner.Emso = newOwner.Emso;
            owner.TelephoneNumber = newOwner.TelephoneNumber;

            // remove existing cars
            owner.Cars.Clear();

            // add new cars
            if (newOwner.CarIds != null)
            {
                foreach (Guid carId in newOwner.CarIds)
                {
                    Avto car = await _dbContext.Cars.FindAsync(carId);
                    if (car != null)
                    {
                        owner.Cars.Add(car);
                    }
                }
            }

            _dbContext.Owners.Update(owner);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        else
        {
            return false;
        }
    }
    public async Task<IEnumerable<Avto>> GetCarsByOwnerIdAsync(Guid ownerId)
    {
        return await _dbContext.Cars.Where(c => c.OwnerId == ownerId).ToListAsync();
    }

}
