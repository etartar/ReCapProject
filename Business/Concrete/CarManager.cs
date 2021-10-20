using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public List<Car> GetAll()
        {
            return _carDal.GetAll();
        }

        public List<Car> GetCarsByBrandId(int brandId)
        {
            return _carDal.GetAll(c => c.BrandId == brandId);
        }

        public List<Car> GetCarsByColorId(int colorId)
        {
            return _carDal.GetAll(c => c.ColorId == colorId);
        }

        public List<CarDetailDto> GetCarDetails()
        {
            return _carDal.GetCarDetails();
        }

        public Car GetById(int carId)
        {
            return _carDal.Get(c => c.Id == carId);
        }

        public void Create(Car car)
        {
            if (car.Description.Length > 1 && car.DailyPrice > 0)
            {
                _carDal.Add(car);
            }
        }

        public void Update(int carId, Car car)
        {
            var getCar = _carDal.Get(c => c.Id == carId);
            getCar.BrandId = car.BrandId;
            getCar.ColorId = car.ColorId;
            getCar.DailyPrice = car.DailyPrice;
            getCar.Description = car.Description;
            getCar.ModelYear = car.ModelYear;

            _carDal.Update(getCar);
        }

        public void Delete(int carId)
        {
            var getCar = _carDal.Get(c => c.Id == carId);
            _carDal.Delete(getCar);
        }
    }
}
