using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public async Task<IDataResult<List<Car>>> GetAll()
        {
            var data = await _carDal.GetAllAsync();
            return new SuccessDataResult<List<Car>>(data);
        }

        public async Task<IDataResult<List<Car>>> GetCarsByBrandId(int brandId)
        {
            var data = await _carDal.GetAllAsync(c => c.BrandId == brandId);
            return new SuccessDataResult<List<Car>>(data);
        }

        public async Task<IDataResult<List<Car>>> GetCarsByColorId(int colorId)
        {
            var data = await _carDal.GetAllAsync(c => c.ColorId == colorId);
            return new SuccessDataResult<List<Car>>(data);
        }

        public async Task<IDataResult<List<CarDetailDto>>> GetCarDetails()
        {
            var data = await _carDal.GetCarDetailsAsync();
            return new SuccessDataResult<List<CarDetailDto>>(data);
        }

        public async Task<IDataResult<Car>> GetById(int carId)
        {
            var data = await _carDal.GetAsync(c => c.Id == carId);
            return new SuccessDataResult<Car>(data);
        }

        public async Task<IResult> Create(Car car)
        {
            if (car.Description.Length < 2)
            {
                return new ErrorResult(Messages.CarNameMinLengthTwo);
            }
            
            if (car.DailyPrice <= 0)
            {
                return new ErrorResult(Messages.CarDailyPriceGreaterThanZero);
            }

            await _carDal.AddAsync(car);
            return new SuccessResult(Messages.CarAdded);
        }

        public async Task<IResult> Update(int carId, Car car)
        {
            var getCar = await _carDal.GetAsync(c => c.Id == carId);
            getCar.BrandId = car.BrandId;
            getCar.ColorId = car.ColorId;
            getCar.DailyPrice = car.DailyPrice;
            getCar.Description = car.Description;
            getCar.ModelYear = car.ModelYear;

            await _carDal.UpdateAsync(getCar);

            return new SuccessResult(Messages.CarUpdated);
        }

        public async Task<IResult> Delete(int carId)
        {
            var getCar = await _carDal.GetAsync(c => c.Id == carId);

            _carDal.Delete(getCar);

            return new SuccessResult(Messages.CarDeleted);
        }
    }
}
