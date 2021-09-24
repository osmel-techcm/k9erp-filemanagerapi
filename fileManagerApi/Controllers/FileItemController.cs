using fileManagerCore.Entities;
using fileManagerCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace fileManagerApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FileItemController : ControllerBase
    {
        private readonly IFileItemService _fileItemService;

        public FileItemController(IFileItemService fileItemService)
        {
            _fileItemService = fileItemService;
        }

        [HttpGet("{id}")]
        public async Task<responseData> GetFileItem(int id)
        {
            return await _fileItemService.GetFileItem(id);
        }

        [Route("GetFileItemByParent")]
        public async Task<responseData> GetFileItemByParent(int parent, int type)
        {
            return await _fileItemService.GetFileItemByParent(parent, type);
        }

        [HttpDelete("{id}")]
        public async Task<responseData> DeleteFileItem(int id)
        {
            return await _fileItemService.DeleteFileItem(id);
        }

        [HttpPost]
        public async Task<responseData> PostFileItem(int parent, int type)
        {
            var formFile = Request.Form.Files[0];

            var fileItem = new FileItem
            {
                Name = formFile.FileName,
                Size = formFile.Length,
                Parent = parent,
                Type = type
            };

            using (var ms = new MemoryStream())
            {
                formFile.CopyTo(ms);
                fileItem.Content = Convert.ToBase64String(ms.ToArray());
            }

            return await _fileItemService.PostFileItem(fileItem);
        }

        [Route("DownloadFileItem")]
        public async Task<FileResult> DownloadFileItem(int id)
        {
            var fileItemResponse = await _fileItemService.GetFileItem(id);
            var fileItem = (FileItem)fileItemResponse.data;
            var file = Convert.FromBase64String(fileItem.Content);
            return File(file, "application/octet-stream", fileItem.Name);
        }

        [HttpPost]
        [Route("uploadCompanyFile")]
        public async Task<responseData> uploadCompanyFile()
        {
            var formFile = Request.Form.Files[0];

            var fileItem = new FileItem
            {
                Name = formFile.FileName,
                Size = formFile.Length,
                Parent = 0,
                Type = 0
            };

            using (var ms = new MemoryStream())
            {
                formFile.CopyTo(ms);
                fileItem.Content = Convert.ToBase64String(ms.ToArray());
            }            

            return await _fileItemService.UploadCompanyFileItem(fileItem);
        }

        [HttpGet]
        [Route("getCompanyFile")]
        public async Task<responseData> getCompanyFile()
        {
            return await _fileItemService.GetCompanyFileItem();
        }
    }
}
