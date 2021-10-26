using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.FileManager
{
    public interface IFileManager
    {
        Task<IDataResult<IEnumerable<(DateTime date, string fileName)>>> UploadFilesAsync(List<IFormFile> files);
        IDataResult<IEnumerable<(DateTime date, string fileName)>> UploadFiles(List<IFormFile> files);
        Task<IDataResult<(DateTime date, string fileName)>> UploadFileAsync(IFormFile file);
        IDataResult<(DateTime date, string fileName)> UploadFile(IFormFile file);
        Task<IDataResult<(DateTime date, string fileName)>> UpdateFileAsync(IFormFile file, string path);
        IDataResult<(DateTime date, string fileName)> UpdateFile(IFormFile file, string path);
        IResult DeleteFile(string path);
        IResult IsFileExists(IFormFile file);
        IResult IsFileUploadTypeValid(string type);
        void IsDirectoryExists(string directory);
    }
}
