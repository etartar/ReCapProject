using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private readonly IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public async Task<IDataResult<List<Brand>>> GetAll()
        {
            var data = await _brandDal.GetAllAsync();
            return new SuccessDataResult<List<Brand>>(data);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public async Task<IDataResult<Brand>> GetById(int brandId)
        {
            var data = await _brandDal.GetAsync(b => b.Id == brandId);
            if (data is null) return new ErrorDataResult<Brand>(data, Messages.BrandIsNull);
            else return new SuccessDataResult<Brand>(data);
        }

        [SecuredOperation("Brand.Create,admin")]
        [ValidationAspect(typeof(BrandValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public async Task<IResult> Create(Brand brand)
        {
            await _brandDal.AddAsync(brand);
            return new SuccessResult(Messages.BrandAdded);
        }

        [SecuredOperation("Brand.Update,admin")]
        [ValidationAspect(typeof(BrandValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public async Task<IResult> Update(Brand brand)
        {
            await _brandDal.UpdateAsync(brand);
            return new SuccessResult(Messages.BrandUpdated);
        }

        [SecuredOperation("Brand.Delete,admin")]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Delete(Brand brand)
        {
            _brandDal.Delete(brand);
            return new SuccessResult(Messages.BrandDeleted);
        }
    }
}
