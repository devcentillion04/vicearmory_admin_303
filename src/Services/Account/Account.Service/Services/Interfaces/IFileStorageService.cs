using System.IO;
using System.Threading.Tasks;

namespace Account.Service.Services.Interfaces
{
    public interface IFileStorageService
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
