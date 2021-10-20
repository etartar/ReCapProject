using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICarService
    {
        List<Car> GetAll();
        List<Car> GetCarsByBrandId(int brandId);
        List<Car> GetCarsByColorId(int colorId);
        List<CarDetailDto> GetCarDetails();
        Car GetById(int carId);
        void Create(Car car);
        void Update(int carId, Car car);
        void Delete(int carId);
    }
}
