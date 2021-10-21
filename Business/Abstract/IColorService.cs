using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IColorService
    {
        Task<IDataResult<List<Color>>> GetAll();
        Task<IDataResult<Color>> GetById(int colorId);
        Task<IResult> Create(Color color);
        Task<IResult> Update(int colorId, Color color);
        Task<IResult> Delete(int colorId);
    }
}
