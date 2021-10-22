using Core.DataAccess;
using Entities.Concrete;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IRentalDal : IEntityRepository<Rental>
    {
        Rental GetLastRentalByCarId(int carId);
        Task<Rental> GetLastRentalByCarIdAsync(int carId);
    }
}
