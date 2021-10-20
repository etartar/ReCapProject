using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICarDal
    {
        Car GetById(int id);
        Task<Car> GetByIdAsync();
        List<Car> GetAll();
        Task<List<Car>> GetAllAsync();
        void Add(Car car);
        Task AddAsync(Car car);
        void Update(Car car);
        Task UpdateAsync(Car car);
        void Delete(Car car);
    }
}
