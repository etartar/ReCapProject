using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICarDal : IEntityRepository<Car>
    {
        List<CarDetailDto> GetCarDetails();
        Task<List<CarDetailDto>> GetCarDetailsAsync();
        CarDetailDto GetCarDetailById(Expression<Func<CarDetailDto, bool>> filter);
        Task<CarDetailDto> GetCarDetailByIdAsync(Expression<Func<CarDetailDto, bool>> filter);
    }
}
