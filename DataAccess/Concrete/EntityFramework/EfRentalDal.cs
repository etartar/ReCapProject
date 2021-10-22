using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, CarRentalContext>, IRentalDal
    {
        public Rental GetLastRentalByCarId(int carId)
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                return context.Rentals.Where(x => x.CarId == carId).OrderByDescending(o => o.Id).SingleOrDefault();
            }
        }

        public async Task<Rental> GetLastRentalByCarIdAsync(int carId)
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                return await context.Rentals.Where(x => x.CarId == carId).OrderByDescending(o => o.Id).SingleOrDefaultAsync();
            }
        }
    }
}
