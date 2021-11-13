using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRentalService
    {
        Task<IDataResult<List<Rental>>> GetAll();
        Task<IDataResult<List<RentalDetailDto>>> GetRentalDetails();
        Task<IDataResult<Rental>> GetById(int rentalId);
        Task<IResult> Create(Rental rental);
        Task<IResult> Update(Rental rental);
        IResult Delete(Rental rental);
        IResult CheckIsCarRentalable(int carId, DateTime rentDate);
    }
}
