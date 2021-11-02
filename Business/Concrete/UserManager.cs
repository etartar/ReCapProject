using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
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

        [CacheAspect]
        [PerformanceAspect(5)]
        public async Task<IDataResult<List<User>>> GetAll()
        {
            var data = await _userDal.GetAllAsync();
            return new SuccessDataResult<List<User>>(data);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public async Task<IDataResult<User>> GetById(int userId)
        {
            var data = await _userDal.GetAsync(b => b.Id == userId);
            if (data is null) return new ErrorDataResult<User>(data, Messages.UserIsNull);
            else return new SuccessDataResult<User>(data);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public async Task<IDataResult<User>> GetByEmail(string email)
        {
            var data = await _userDal.GetAsync(b => b.Email == email);
            if (data is null) return new ErrorDataResult<User>(data, Messages.UserIsNull);
            else return new SuccessDataResult<User>(data);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public async Task<IDataResult<List<OperationClaim>>> GetClaims(User user)
        {
            var data = await _userDal.GetClaimsAsync(user);
            return new SuccessDataResult<List<OperationClaim>>(data);
        }

        [ValidationAspect(typeof(UserValidator))]
        [CacheRemoveAspect("IUserService.Get")]
        public async Task<IResult> Create(User user)
        {
            await _userDal.AddAsync(user);
            return new SuccessResult(Messages.UserAdded);
        }

        [ValidationAspect(typeof(UserValidator))]
        [CacheRemoveAspect("IUserService.Get")]
        public async Task<IResult> Update(User user)
        {
            await _userDal.UpdateAsync(user);
            return new SuccessResult(Messages.UserUpdated);
        }

        [CacheRemoveAspect("IUserService.Get")]
        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeleted);
        }
    }
}
