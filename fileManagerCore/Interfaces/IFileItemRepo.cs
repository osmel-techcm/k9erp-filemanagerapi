using fileManagerCore.Entities;
using System.Threading.Tasks;

namespace fileManagerCore.Interfaces
{
    public interface IFileItemRepo
    {
        Task<responseData> GetFileItem(int id);

        Task<responseData> GetFileItemByParent(int parent, int type);

        Task<responseData> PostFileItem(FileItem fileItem);

        Task<responseData> DeleteFileItem(int id);
    }
}
