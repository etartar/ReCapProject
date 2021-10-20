using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        private List<Car> _cars;

        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
                new Car { Id = 1, BrandId = 1, ColorId = 1, DailyPrice = 300, ModelYear = 2021, Description = "BMW 3.20" },
                new Car { Id = 2, BrandId = 2, ColorId = 2, DailyPrice = 240, ModelYear = 2021, Description = "Renault Clio" },
                new Car { Id = 3, BrandId = 3, ColorId = 1, DailyPrice = 440, ModelYear = 2021, Description = "Audi A8" },
            };
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public Task<List<Car>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Car GetById(int id)
        {
            return _cars.SingleOrDefault(x => x.Id == id);
        }

        public Task<Car> GetByIdAsync()
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
    }
}
