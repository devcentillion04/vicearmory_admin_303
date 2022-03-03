using System.IO;
using System.Threading.Tasks;

namespace ViceArmory.DAL.Repository.Interface
{
    public interface IFileStorageRepository
    {
        Task<string> SaveAsync(
           string containerName,
           string blobName,
           Stream source,
           string fileNameSuffix,
           bool closeStream = true,
           bool isPublic = false);
    }
}
