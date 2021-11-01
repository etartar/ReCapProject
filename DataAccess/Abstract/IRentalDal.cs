using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IRentalDal : IEntityRepository<Rental>
    {
        Rental GetLastRentalByCarId(int carId);
        Task<Rental> GetLastRentalByCarIdAsync(int carId);
        List<RentalDetailDto> GetRentalDetails();
        Task<List<RentalDetailDto>> GetRentalDetailsAsync();
    }
}
