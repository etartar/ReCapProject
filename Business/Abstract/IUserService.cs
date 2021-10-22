using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<IDataResult<List<User>>> GetAll();
        Task<IDataResult<User>> GetById(int userId);
        Task<IResult> Create(User user);
        Task<IResult> Update(User user);
        IResult Delete(User user);
    }
}
