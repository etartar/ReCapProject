using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private readonly IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public async Task<IDataResult<List<Rental>>> GetAll()
        {
            var data = await _rentalDal.GetAllAsync();
            return new SuccessDataResult<List<Rental>>(data);
        }

        public async Task<IDataResult<List<RentalDetailDto>>> GetRentalDetails()
        {
            var data = await _rentalDal.GetRentalDetailsAsync();
            return new SuccessDataResult<List<RentalDetailDto>>(data);
        }

        public async Task<IDataResult<Rental>> GetById(int rentalId)
        {
            var data = await _rentalDal.GetAsync(b => b.Id == rentalId);
            if (data is null) return new ErrorDataResult<Rental>(data, Messages.RentalIsNull);
            else return new SuccessDataResult<Rental>(data);
        }

        [ValidationAspect(typeof(RentalValidator))]
        public async Task<IResult> Create(Rental rental)
        {
            IResult result = BusinessRules.Run(CheckIsCarRentalable(rental.CarId, rental.RentDate));
            if (result != null)
            {
                return result;
            }

            await _rentalDal.AddAsync(rental);
            return new SuccessResult(Messages.RentalAdded);
        }

        [ValidationAspect(typeof(RentalValidator))]
        public async Task<IResult> Update(Rental rental)
        {
            await _rentalDal.UpdateAsync(rental);
            return new SuccessResult(Messages.RentalUpdated);
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeleted);
        }

        private IResult CheckIsCarRentalable(int carId, DateTime rentDate)
        {
            Rental getLastRental = _rentalDal.GetLastRentalByCarId(carId);
            if (getLastRental != null)
            {
                if ((!getLastRental.ReturnDate.HasValue) || (rentDate <= getLastRental.ReturnDate))
                {
                    return new ErrorResult(Messages.RentalAddedError);
                }
            }

            return new SuccessResult();
        }
    }
}
