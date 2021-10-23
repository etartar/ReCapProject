using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task<IDataResult<List<User>>> GetAll()
        {
            var data = await _userDal.GetAllAsync();
            return new SuccessDataResult<List<User>>(data);
        }

        public async Task<IDataResult<User>> GetById(int userId)
        {
            var data = await _userDal.GetAsync(b => b.Id == userId);
            if (data is null) return new ErrorDataResult<User>(data, Messages.UserIsNull);
            else return new SuccessDataResult<User>(data);
        }

        public async Task<IResult> Create(User user)
        {
            await _userDal.AddAsync(user);
            return new SuccessResult(Messages.UserAdded);
        }

        public async Task<IResult> Update(User user)
        {
            await _userDal.UpdateAsync(user);
            return new SuccessResult(Messages.UserUpdated);
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeleted);
        }
    }
}
