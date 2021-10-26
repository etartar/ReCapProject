using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        Task<IDataResult<List<CarImage>>> GetAll(int carId);
        Task<IDataResult<CarImage>> GetById(int carImageId);
        Task<IResult> Create(CarImageCreateDto carImageCreateDto);
        Task<IResult> Update(CarImage carImage, CarImageUpdateDto carImageUpdateDto);
        IResult Delete(CarImage carImage);
    }
}
