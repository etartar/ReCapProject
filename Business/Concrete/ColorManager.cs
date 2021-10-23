using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        private readonly IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public async Task<IDataResult<List<Color>>> GetAll()
        {
            var data = await _colorDal.GetAllAsync();
            return new SuccessDataResult<List<Color>>(data);
        }

        public async Task<IDataResult<Color>> GetById(int colorId)
        {
            var data = await _colorDal.GetAsync(b => b.Id == colorId);
            if (data is null) return new ErrorDataResult<Color>(data, Messages.ColorIsNull);
            else return new SuccessDataResult<Color>(data);
        }

        public async Task<IResult> Create(Color color)
        {
            await _colorDal.AddAsync(color);
            return new SuccessResult(Messages.ColorAdded);
        }

        public async Task<IResult> Update(Color color)
        {
            await _colorDal.UpdateAsync(color);
            return new SuccessResult(Messages.ColorUpdated);
        }

        public IResult Delete(Color color)
        {
            _colorDal.Delete(color);
            return new SuccessResult(Messages.ColorDeleted);
        }
    }
}
