using fileManagerCore.Entities;
using fileManagerCore.Interfaces;
using fileManagerInfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace fileManagerInfrastructure.Repositories
{
    public class FileItemRepo : IFileItemRepo
    {
        private readonly MultitenantDbContext _context;

        public FileItemRepo(MultitenantDbContext context)
        {
            _context = context;
        }

        public async Task<responseData> GetFileItem(int id)
        {
            var responseData = new responseData();

            try
            {
                responseData.data = await _context.FileItems.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception e)
            {
                responseData.error = true;
                responseData.errorValue = 2;
                responseData.description = e.Message;
                responseData.data = e;
            }

            return responseData;
        }

        public async Task<responseData> GetFileItemByParent(int parent, int type)
        {
            var responseData = new responseData();

            try
            {
                responseData.data = await _context.FileItems.Where(x => x.Parent == parent && x.Type == type).Select( x=> new FileItem { 
                    Id = x.Id, Name = x.Name
                }).ToListAsync();
            }
            catch (Exception e)
            {
                responseData.error = true;
                responseData.errorValue = 2;
                responseData.description = e.Message;
                responseData.data = e;
            }

            return responseData;
        }

        public async Task<responseData> PostFileItem(FileItem fileItem)
        {
            var responseData = new responseData();

            try
            {
                _context.FileItems.Add(fileItem);
                await _context.SaveChangesAsync();
                responseData.data = fileItem;
            }
            catch (Exception e)
            {
                responseData.error = true;
                responseData.errorValue = 2;
                responseData.description = e.Message;
                responseData.data = e;
            }

            return responseData;
        }

        public async Task<responseData> DeleteFileItem(int id)
        {
            var responseData = new responseData();

            try
            {
                var fileItem = await _context.FileItems.FirstOrDefaultAsync(x=>x.Id == id);
                if (fileItem == null)
                {
                    responseData.error = true;
                    responseData.errorValue = 2;
                    responseData.description = "File not found!";
                    return responseData;
                }

                _context.FileItems.Remove(fileItem);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                responseData.error = true;
                responseData.errorValue = 2;
                responseData.description = e.Message;
                responseData.data = e;
            }

            return responseData;
        }
    }
}
