using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarRentalContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using CarRentalContext context = new CarRentalContext();
            return GetCarDetailsQuery(context).ToList();
        }

        public async Task<List<CarDetailDto>> GetCarDetailsAsync()
        {
            using CarRentalContext context = new CarRentalContext();
            return await GetCarDetailsQuery(context).ToListAsync();
        }

        public CarDetailDto GetCarDetailById(Expression<Func<CarDetailDto, bool>> filter)
        {
            using CarRentalContext context = new CarRentalContext();
            return GetCarDetailsQuery(context).SingleOrDefault(filter);
        }

        public async Task<CarDetailDto> GetCarDetailByIdAsync(Expression<Func<CarDetailDto, bool>> filter)
        {
            using CarRentalContext context = new CarRentalContext();
            return await GetCarDetailsQuery(context).SingleOrDefaultAsync(filter);
        }

        private IQueryable<CarDetailDto> GetCarDetailsQuery(CarRentalContext context)
        {
            return from car in context.Cars.AsNoTracking()
                   join brand in context.Brands.AsNoTracking() on car.BrandId equals brand.Id
                   join color in context.Colors.AsNoTracking() on car.ColorId equals color.Id
                   select new CarDetailDto
                   {
                       Id = car.Id,
                       BrandName = brand.Name,
                       ColorName = color.Name,
                       DailyPrice = car.DailyPrice,
                       Description = car.Description
                   };
        }
    }
}
