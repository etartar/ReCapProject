using Business.Abstract;
using Business.Constants;
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

        public async Task<IDataResult<List<Brand>>> GetAll()
        {
            var data = await _brandDal.GetAllAsync();
            return new SuccessDataResult<List<Brand>>(data);
        }

        public async Task<IDataResult<Brand>> GetById(int brandId)
        {
            var data = await _brandDal.GetAsync(b => b.Id == brandId);
            return new SuccessDataResult<Brand>(data);
        }

        public async Task<IResult> Create(Brand brand)
        {
            await _brandDal.AddAsync(brand);
            return new SuccessResult(Messages.BrandAdded);
        }

        public async Task<IResult> Update(int brandId, Brand brand)
        {
            var getBrand = await _brandDal.GetAsync(b => b.Id == brandId);
            getBrand.Name = brand.Name;

            await _brandDal.UpdateAsync(getBrand);

            return new SuccessResult(Messages.BrandUpdated);
        }

        public async Task<IResult> Delete(int brandId)
        {
            var getBrand = await _brandDal.GetAsync(b => b.Id == brandId);

            _brandDal.Delete(getBrand);

            return new SuccessResult(Messages.BrandDeleted);
        }
    }
}
