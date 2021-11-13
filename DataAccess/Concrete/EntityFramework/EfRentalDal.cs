using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, CarRentalContext>, IRentalDal
    {
        public Rental GetLastRentalByCarId(int carId)
        {
            using CarRentalContext context = new CarRentalContext();
            return context.Rentals.Where(x => x.CarId == carId).OrderByDescending(o => o.Id).FirstOrDefault();
        }

        public async Task<Rental> GetLastRentalByCarIdAsync(int carId)
        {
            using CarRentalContext context = new CarRentalContext();
            return await context.Rentals.Where(x => x.CarId == carId).OrderByDescending(o => o.Id).FirstOrDefaultAsync();
        }

        public List<RentalDetailDto> GetRentalDetails()
        {
            using CarRentalContext context = new CarRentalContext();
            return GetRentalDetailsQuery(context).ToList();
        }

        public async Task<List<RentalDetailDto>> GetRentalDetailsAsync()
        {
            using CarRentalContext context = new CarRentalContext();
            return await GetRentalDetailsQuery(context).ToListAsync();
        }

        private IQueryable<RentalDetailDto> GetRentalDetailsQuery(CarRentalContext context)
        {
            return from rental in context.Rentals.AsNoTracking()
                   join car in context.Cars.AsNoTracking() on rental.CarId equals car.Id
                   join brand in context.Brands.AsNoTracking() on car.BrandId equals brand.Id
                   join customer in context.Customers.AsNoTracking() on rental.CustomerId equals customer.Id
                   join user in context.Users.AsNoTracking() on customer.UserId equals user.Id
                   select new RentalDetailDto
                   {
                       Id = rental.Id,
                       BrandName = brand.Name,
                       FullName = user.FullName,
                       RentDate = rental.RentDate,
                       ReturnDate = rental.ReturnDate
                   };
        }
    }
}
