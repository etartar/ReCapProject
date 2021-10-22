using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBrandService
    {
        Task<IDataResult<List<Brand>>> GetAll();
        Task<IDataResult<Brand>> GetById(int brandId);
        Task<IResult> Create(Brand brand);
        Task<IResult> Update(Brand brand);
        IResult Delete(Brand brand);
    }
}
