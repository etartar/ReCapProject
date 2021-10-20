using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICarService
    {
        List<Car> GetAllCars();
        Car GetCarById(int carId);
        void CreateCar(Car car);
        void UpdateCar(Car car);
        void DeleteCar(Car car);
    }
}
