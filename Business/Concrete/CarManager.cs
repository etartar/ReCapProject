using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
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

        public List<Car> GetAllCars()
        {
            return _carDal.GetAll();
        }

        public Car GetCarById(int carId)
        {
            return _carDal.GetById(carId);
        }

        public void CreateCar(Car car)
        {
            _carDal.Add(car);
        }

        public void UpdateCar(Car car)
        {
            _carDal.Update(car);
        }

        public void DeleteCar(Car car)
        {
            _carDal.Delete(car);
        }
    }
}
