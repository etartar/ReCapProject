using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRentalService
    {
        Task<IDataResult<List<Rental>>> GetAll();
        Task<IDataResult<Rental>> GetById(int rentalId);
        Task<IResult> Create(Rental rental);
        Task<IResult> Update(Rental rental);
        IResult Delete(Rental rental);
    }
}
