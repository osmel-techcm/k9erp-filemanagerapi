using fileManagerCore.Entities;
using fileManagerCore.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fileManagerCore.Services
{
    public class FileItemService : IFileItemService
    {
        private readonly IFileItemRepo _iFileItemRepo;

        public FileItemService(IFileItemRepo iFileItemRepo)
        {
            _iFileItemRepo = iFileItemRepo;
        }

        public async Task<responseData> GetFileItem(int id)
        {
            return await _iFileItemRepo.GetFileItem(id);
        }

        public async Task<responseData> GetFileItemByParent(int parent, int type)
        {
            return await _iFileItemRepo.GetFileItemByParent(parent, type);
        }

        public async Task<responseData> PostFileItem(FileItem fileItem)
        {
            var responseData = validateData(fileItem);
            if (responseData.error)
            {
                return responseData;
            }
            return await _iFileItemRepo.PostFileItem(fileItem);
        }

        public async Task<responseData> DeleteFileItem(int id)
        {
            return await _iFileItemRepo.DeleteFileItem(id);
        }

        private responseData validateData(FileItem fileItem)
        {
            responseData responseData = new();

            if (string.IsNullOrEmpty(fileItem.Name))
            {
                responseData.error = true;
                responseData.errorValue = 2;
                responseData.description = "The Name is required!";
                return responseData;
            }

            if (fileItem.Parent == 0)
            {
                responseData.error = true;
                responseData.errorValue = 2;
                responseData.description = "The Parent is required!";
                return responseData;
            }

            if (fileItem.Size == 0)
            {
                responseData.error = true;
                responseData.errorValue = 2;
                responseData.description = "The file is empty!";
                return responseData;
            }

            return responseData;
        }

        public async Task<responseData> UploadCompanyFileItem(FileItem formFile)
        {
            var fileResponse = await _iFileItemRepo.GetFileItemByParent(0, 0);
            if (fileResponse.error)
            {
                return fileResponse;
            }

            var files = (List<FileItem>)fileResponse.data;
            foreach (var file in files)
            {
                var response = await _iFileItemRepo.DeleteFileItem(file.Id);
                if (response.error)
                {
                    return response;
                }
            }

            return await _iFileItemRepo.PostFileItem(formFile);
        }

        public async Task<responseData> GetCompanyFileItem()
        {
            var responseData = new responseData();

            var fileResponse = await _iFileItemRepo.GetFileItemByParent(0, 0);
            if (fileResponse.error)
            {
                return fileResponse;
            }

            var files = (List<FileItem>)fileResponse.data;
            if (files.Count > 0)
            {
                return await _iFileItemRepo.GetFileItem(files.FirstOrDefault().Id);
            }

            return responseData;
        }
    }
}
