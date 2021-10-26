using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.FileManager;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private readonly ICarImageDal _carImageDal;
        private readonly IFileManager _fileManager;

        public CarImageManager(ICarImageDal carImageDal, IFileManager fileManager)
        {
            _carImageDal = carImageDal;
            _fileManager = fileManager;
        }

        public async Task<IDataResult<List<CarImage>>> GetAll(int carId)
        {
            var data = await _carImageDal.GetAllAsync(c => c.CarId == carId);
            if (data.Count == 0)
            {
                data = new List<CarImage>
                {
                    new CarImage { CarId = carId, ImagePath = "/images/logo.png", Date = System.DateTime.Now }
                };
            }
            return new SuccessDataResult<List<CarImage>>(data);
        }

        public async Task<IDataResult<CarImage>> GetById(int carImageId)
        {
            var data = await _carImageDal.GetAsync(c => c.Id == carImageId);
            if (data is null) return new ErrorDataResult<CarImage>(data, Messages.CarImageIsNull);
            else return new SuccessDataResult<CarImage>(data);
        }

        [ValidationAspect(typeof(CarImageCreateDtoValidator))]
        public async Task<IResult> Create(CarImageCreateDto carImageCreateDto)
        {
            IResult result = BusinessRules.Run(CheckCarImageCount(carImageCreateDto.CarId));
            if (result != null)
            {
                return result;
            }

            IDataResult<(DateTime date, string fileName)> uploadedFileResult = await _fileManager.UploadFileAsync(carImageCreateDto.File);
            if (!uploadedFileResult.Success)
            {
                return new ErrorResult(uploadedFileResult.Message);
            }

            await _carImageDal.AddAsync(new CarImage
            {
                CarId = carImageCreateDto.CarId,
                ImagePath = uploadedFileResult.Data.fileName,
                Date = uploadedFileResult.Data.date
            });
            return new SuccessResult(Messages.CarImageAdded);
        }

        [ValidationAspect(typeof(CarImageUpdateDtoValidator))]
        public async Task<IResult> Update(CarImage carImage, CarImageUpdateDto carImageUpdateDto)
        {
            IDataResult<(DateTime date, string fileName)> updatedFileResult = await _fileManager.UpdateFileAsync(carImageUpdateDto.File, carImage.ImagePath);
            if (!updatedFileResult.Success)
            {
                return new ErrorResult(updatedFileResult.Message);
            }

            carImage.ImagePath = updatedFileResult.Data.fileName;
            await _carImageDal.UpdateAsync(carImage);
            return new SuccessResult(Messages.CarImageUpdated);
        }

        public IResult Delete(CarImage carImage)
        {
            IResult deleteFileResult = _fileManager.DeleteFile(carImage.ImagePath);
            _carImageDal.Delete(carImage);
            return new SuccessResult($"{Messages.CarImageDeleted} -> {deleteFileResult.Message}");
        }

        private IResult CheckCarImageCount(int carId)
        {
            var count = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (count >= 5)
            {
                return new ErrorResult(Messages.CarImageMustBeMaximumFiveImage);
            }
            return new SuccessResult();
        }
    }
}
