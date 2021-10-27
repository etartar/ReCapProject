using Core.Entities.Concrete;
using Core.Utilities.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<IDataResult<List<User>>> GetAll();
        Task<IDataResult<User>> GetById(int userId);
        Task<IDataResult<User>> GetByEmail(string email);
        Task<IDataResult<List<OperationClaim>>> GetClaims(User user);
        Task<IResult> Create(User user);
        Task<IResult> Update(User user);
        IResult Delete(User user);
    }
}
