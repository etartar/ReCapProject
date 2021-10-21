using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarService
    {
        Task<IDataResult<List<Car>>> GetAll();
        Task<IDataResult<List<Car>>> GetCarsByBrandId(int brandId);
        Task<IDataResult<List<Car>>> GetCarsByColorId(int colorId);
        Task<IDataResult<List<CarDetailDto>>> GetCarDetails();
        Task<IDataResult<Car>> GetById(int carId);
        Task<IResult> Create(Car car);
        Task<IResult> Update(int carId, Car car);
        Task<IResult> Delete(int carId);
    }
}
