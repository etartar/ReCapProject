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
    public class EfCarDal : EfEntityRepositoryBase<Car, CarRentalContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                var result = from car in context.Cars.AsNoTracking()
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

                return result.ToList();
            }
        }

        public async Task<List<CarDetailDto>> GetCarDetailsAsync()
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                var result = from car in context.Cars.AsNoTracking()
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

                return await result.ToListAsync();
            }
        }
    }
}
