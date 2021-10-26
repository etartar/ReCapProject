using Core.Utilities.Business;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.FileManager.Microsoft
{
    public class MemoryFileManager : IFileManager
    {
        private readonly string _currentDirectory = $"{Environment.CurrentDirectory}\\wwwroot";
        private readonly string _uploadFolderName = "\\images\\";
        private readonly string _fullPath;
        private readonly List<string> _fileUploadTypes;

        public MemoryFileManager(IConfiguration configuration)
        {
            _fullPath = string.Concat(_currentDirectory, _uploadFolderName);
            _fileUploadTypes = configuration.GetSection("FileUploadTypes").GetChildren().Select(x => x.Value).ToList();
        }

        public IDataResult<(DateTime date, string fileName)> UploadFile(IFormFile file)
        {
            IsDirectoryExists(_fullPath);

            var fileType = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid().ToString();
            var filePath = Path.Combine(_fullPath, fileName) + fileType;
            var returnFileName = Path.Combine(_uploadFolderName, fileName) + fileType;

            IResult result = BusinessRules.Run(IsFileExists(file), IsFileUploadTypeValid(fileType));
            if (result != null)
                return new ErrorDataResult<(DateTime date, string fileName)>(result.Message);

            FileUploadProcess(filePath, file);
            return new SuccessDataResult<(DateTime date, string fileName)>(data: (DateTime.Now, ReplaceData(returnFileName, "\\", "/")));
        }

        public async Task<IDataResult<(DateTime date, string fileName)>> UploadFileAsync(IFormFile file)
        {
            IsDirectoryExists(_fullPath);

            var fileType = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid().ToString();
            var filePath = Path.Combine(_fullPath, fileName) + fileType;
            var returnFileName = Path.Combine(_uploadFolderName, fileName) + fileType;

            IResult result = BusinessRules.Run(IsFileExists(file), IsFileUploadTypeValid(fileType));
            if (result != null)
                return new ErrorDataResult<(DateTime date, string fileName)>(result.Message);

            await FileUploadProcessAsync(filePath, file);
            return new SuccessDataResult<(DateTime date, string fileName)>(data: (DateTime.Now, ReplaceData(returnFileName, "\\", "/")));
        }

        public IDataResult<IEnumerable<(DateTime date, string fileName)>> UploadFiles(List<IFormFile> files)
        {
            IsDirectoryExists(_fullPath);

            BlockingCollection<(DateTime date, string fileName)> imageList = new();

            files.ForEach((file) =>
            {
                var fileType = Path.GetExtension(file.FileName);
                var fileName = Guid.NewGuid().ToString();
                var filePath = Path.Combine(_fullPath, fileName) + fileType;
                var returnFileName = Path.Combine(_uploadFolderName, fileName) + fileType;

                IResult result = BusinessRules.Run(IsFileExists(file), IsFileUploadTypeValid(fileType));
                if (result == null)
                {
                    imageList.Add((DateTime.Now, ReplaceData(returnFileName, "\\", "/")));
                    FileUploadProcess(filePath, file);
                }
            });

            return new SuccessDataResult<IEnumerable<(DateTime date, string fileName)>>(data: imageList.ToList());
        }

        public async Task<IDataResult<IEnumerable<(DateTime date, string fileName)>>> UploadFilesAsync(List<IFormFile> files)
        {
            IsDirectoryExists(_fullPath);

            BlockingCollection<(DateTime date, string fileName)> imageList = new();

            await Task.Run(() =>
            {
                files.ForEach(async (file) =>
                {
                    var fileType = Path.GetExtension(file.FileName);
                    var fileName = Guid.NewGuid().ToString();
                    var filePath = Path.Combine(_fullPath, fileName) + fileType;
                    var returnFileName = Path.Combine(_uploadFolderName, fileName) + fileType;

                    IResult result = BusinessRules.Run(IsFileExists(file), IsFileUploadTypeValid(fileType));
                    if (result == null)
                    {
                        imageList.Add((DateTime.Now, ReplaceData(returnFileName, "\\", "/")));
                        await FileUploadProcessAsync(filePath, file);
                    }
                });
            });

            return new SuccessDataResult<IEnumerable<(DateTime date, string fileName)>>(data: imageList.ToList());
        }

        public IDataResult<(DateTime date, string fileName)> UpdateFile(IFormFile file, string path)
        {
            var uploadedFile = UploadFile(file);
            if (uploadedFile.Success)
            {
                DeleteFile(path);
            }
            return uploadedFile;
        }

        public async Task<IDataResult<(DateTime date, string fileName)>> UpdateFileAsync(IFormFile file, string path)
        {
            var uploadedFile = await UploadFileAsync(file);
            if (uploadedFile.Success)
            {
                DeleteFile(path);
            }
            return uploadedFile;
        }

        public IResult DeleteFile(string path)
        {
            var fileName = string.Concat(_currentDirectory, ReplaceData(path, "/", "\\"));
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
                return new SuccessResult();
            }
            return new ErrorResult("File doesn't exists");
        }

        public IResult IsFileExists(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                return new SuccessResult();
            }
            return new ErrorResult("File doesn't exists");
        }

        public IResult IsFileUploadTypeValid(string type)
        {
            if (_fileUploadTypes.Contains(type))
            {
                return new SuccessResult();
            }
            return new ErrorResult($"Invalid file type. File type must be {GetFileUploadTypes()}");
        }

        public void IsDirectoryExists(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private static void FileUploadProcess(string filePath, IFormFile file)
        {
            using FileStream stream = new(filePath, FileMode.Create);
            file.CopyTo(stream);
            stream.Flush();
        }

        private static async Task FileUploadProcessAsync(string filePath, IFormFile file)
        {
            using FileStream stream = new(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            await stream.FlushAsync();
        }

        private static string ReplaceData(string data, string oldValue, string newValue = "")
        {
            return data.Replace(oldValue, newValue);
        }

        private string GetFileUploadTypes()
        {
            StringBuilder fileUploadTypes = new StringBuilder();

            _fileUploadTypes.ForEach((type) => fileUploadTypes.Append(string.Concat(type, ",")));

            return fileUploadTypes.ToString();
        }
    }
}
