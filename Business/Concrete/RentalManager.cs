using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
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

        public async Task<IDataResult<Rental>> GetById(int rentalId)
        {
            var data = await _rentalDal.GetAsync(b => b.Id == rentalId);
            return new SuccessDataResult<Rental>(data);
        }

        public async Task<IResult> Create(Rental rental)
        {
            await _rentalDal.AddAsync(rental);
            return new SuccessResult(Messages.RentalAdded);
        }

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
    }
}
