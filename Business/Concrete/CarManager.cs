using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
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

        [CacheAspect]
        [PerformanceAspect(5)]
        public async Task<IDataResult<List<Car>>> GetAll()
        {
            var data = await _carDal.GetAllAsync();
            return new SuccessDataResult<List<Car>>(data);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public async Task<IDataResult<List<Car>>> GetCarsByBrandId(int brandId)
        {
            var data = await _carDal.GetAllAsync(c => c.BrandId == brandId);
            return new SuccessDataResult<List<Car>>(data);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public async Task<IDataResult<List<Car>>> GetCarsByColorId(int colorId)
        {
            var data = await _carDal.GetAllAsync(c => c.ColorId == colorId);
            return new SuccessDataResult<List<Car>>(data);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public async Task<IDataResult<List<CarDetailDto>>> GetCarDetails()
        {
            var data = await _carDal.GetCarDetailsAsync();
            return new SuccessDataResult<List<CarDetailDto>>(data);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public async Task<IDataResult<Car>> GetById(int carId)
        {
            var data = await _carDal.GetAsync(c => c.Id == carId);
            if (data is null) return new ErrorDataResult<Car>(data, Messages.CarIsNull);
            else return new SuccessDataResult<Car>(data);
        }

        [SecuredOperation("Car.Create,admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public async Task<IResult> Create(Car car)
        {
            await _carDal.AddAsync(car);
            return new SuccessResult(Messages.CarAdded);
        }

        [SecuredOperation("CarImage.Update,admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public async Task<IResult> Update(Car car)
        {
            await _carDal.UpdateAsync(car);
            return new SuccessResult(Messages.CarUpdated);
        }

        [SecuredOperation("CarImage.Delete,admin")]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }
    }
}
