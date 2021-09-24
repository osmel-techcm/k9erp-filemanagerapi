using fileManagerCore.Entities;
using System.Threading.Tasks;

namespace fileManagerCore.Interfaces
{
    public interface IFileItemService
    {
        Task<responseData> GetFileItem(int id);

        Task<responseData> GetFileItemByParent(int parent, int type);

        Task<responseData> PostFileItem(FileItem formFile);

        Task<responseData> DeleteFileItem(int id);

        Task<responseData> UploadCompanyFileItem(FileItem formFile);

        Task<responseData> GetCompanyFileItem();
    }
}
