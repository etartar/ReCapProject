using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        private readonly List<Car> _cars;

        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
                new Car { Id = 1, BrandId = 1, ColorId = 1, DailyPrice = 300, ModelYear = 2021, Description = "BMW 3.20" },
                new Car { Id = 2, BrandId = 2, ColorId = 2, DailyPrice = 240, ModelYear = 2021, Description = "Renault Clio" },
                new Car { Id = 3, BrandId = 3, ColorId = 1, DailyPrice = 440, ModelYear = 2021, Description = "Audi A8" },
            };
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            return _cars;
        }

        public Task<List<Car>> GetAllAsync(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            return _cars.FirstOrDefault();
        }

        public Task<Car> GetAsync(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public Task AddAsync(Car car)
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            Car carToUpdate = _cars.SingleOrDefault(x => x.Id == car.Id);
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.ModelYear = car.ModelYear;
            carToUpdate.Description = car.Description;
        }

        public Task UpdateAsync(Car car)
        {
            throw new NotImplementedException();
        }

        public void Delete(Car car)
        {
            Car carToDelete = _cars.SingleOrDefault(x => x.Id == car.Id);

            _cars.Remove(carToDelete);
        }

        public List<CarDetailDto> GetCarDetails()
        {
            throw new NotImplementedException();
        }

        public Task<List<CarDetailDto>> GetCarDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public CarDetailDto GetCarDetailById(Expression<Func<CarDetailDto, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<CarDetailDto> GetCarDetailByIdAsync(Expression<Func<CarDetailDto, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}
