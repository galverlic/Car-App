using Car_App.Controllers.DTOModels;
using Car_App.Data.Context;
using Car_App.Data.Models;
using Car_App.Data.Models.NewFolder;
using Car_App.Data.Models.Sorting;
using Car_App.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;

public class OwnerService : IOwnerService
{
    private readonly DatabaseContext _dbContext;


    public OwnerService(DatabaseContext dbContext)
    {
        _dbContext = dbContext;

    }

    public async Task<PagedResult<Owner>> GetAllOwnersAsync(PaginationParameters paginationParameters, OwnerFilter filter, OwnerSortBy sortBy, SortingDirection sortingDirection)
    {
        var query = _dbContext.Owners.AsQueryable();

        query = ApplyFiltering(query, filter);
        query = SortOwners(query, sortBy, sortingDirection);


        var totalCount = await query.CountAsync();
        var totalPages = (int)Math.Ceiling((double)totalCount / paginationParameters.PageSize);
        var hasNextPage = (paginationParameters.Page < totalPages);

        //if (paginationParameters.Page > totalPages)
        //{
        //    paginationParameters.Page = totalPages;
        //}

        var owners = await query.Include(o => o.Cars)
                                .Skip((paginationParameters.Page - 1) * paginationParameters.PageSize)
                                .Take(paginationParameters.PageSize)
                                .ToListAsync();

        return new PagedResult<Owner>()
        {
            Results = owners,
            CurrentPage = paginationParameters.Page,
            TotalPages = totalPages,
            TotalCount = totalCount,
            PageSize = paginationParameters.PageSize,
            HasNextPage = hasNextPage
        };
    }

    private IQueryable<Owner> ApplyFiltering(IQueryable<Owner> query, OwnerFilter filter)
    {
        if (filter.Id != null)
        {
            query = query.Where(o => o.Id == filter.Id);
        }

        if (!string.IsNullOrEmpty(filter.FirstName))
        {
            query = query.Where(o => o.FirstName == filter.FirstName);
        }

        if (!string.IsNullOrEmpty(filter.LastName))
        {
            query = query.Where(o => o.LastName == filter.LastName);
        }

        if (filter.Emso != null)
        {
            query = query.Where(o => o.Emso == filter.Emso);
        }

        if (filter.TelephoneNumber != null)
        {
            query = query.Where(o => o.TelephoneNumber == filter.TelephoneNumber);
        }

        return query;





    }
    private IQueryable<Owner> SortOwners(IQueryable<Owner> query, OwnerSortBy sortBy, SortingDirection sortDirection)
    {
        switch (sortBy)
        {
            case OwnerSortBy.FirstName:
                if (sortDirection == SortingDirection.Ascending)
                    query = query.OrderBy(o => o.FirstName);
                else
                    query = query.OrderByDescending(o => o.FirstName);
                break;
            case OwnerSortBy.LastName:
                if (sortDirection == SortingDirection.Ascending)
                    query = query.OrderBy(o => o.LastName);
                else
                    query = query.OrderByDescending(o => o.LastName);
                break;
            case OwnerSortBy.TelephoneNumber:
                if (sortDirection == SortingDirection.Ascending)
                    query = query.OrderBy(o => o.TelephoneNumber);
                else
                    query = query.OrderByDescending(o => o.TelephoneNumber);
                break;
            default:
                query = query.OrderBy(o => o.Emso);
                break;
        }
        return query;
    }


    public async Task<Owner> GetOwnerWithCarsByIdAsync(Guid id)
    {
        return await _dbContext.Owners.Include(o => o.Cars).FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<Owner> GetOwnerByIdAsync(Guid id)
    {
        return await _dbContext.Owners.FindAsync(id);
    }
    public void SetPassword(Owner owner, string password)
    {
        using var hmac = new HMACSHA512();

        owner.PasswordSalt = hmac.Key;
        owner.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

    }

    public bool VerifyPassword(Owner owner, string password)
    {
        using var hmac = new HMACSHA512(owner.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != owner.PasswordHash[i]) return false;
        }

        return true;

    }

    public async Task CreateNewOwnerAsync(OwnerDto newOwner)
    {
        Owner owner = new Owner
        {
            FirstName = newOwner.FirstName,
            LastName = newOwner.LastName,
            UserName = newOwner.UserName,
            Email = newOwner.Email,
            Emso = newOwner.Emso,
            TelephoneNumber = newOwner.TelephoneNumber
        };
        SetPassword(owner, newOwner.Password);

        if (newOwner.CarIds != null)
        {
            foreach (Guid carId in newOwner.CarIds)
            {
                Car car = await _dbContext.Cars.FindAsync(carId);
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

    public async Task<bool> UpdateOwnerAsync(Guid id, OwnerDto newOwner)
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
                    Car car = await _dbContext.Cars.FindAsync(carId);
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
    public async Task<IEnumerable<Car>> GetCarsByOwnerIdAsync(Guid ownerId)
    {
        return await _dbContext.Cars.Where(c => c.OwnerId == ownerId).ToListAsync();
    }
}
