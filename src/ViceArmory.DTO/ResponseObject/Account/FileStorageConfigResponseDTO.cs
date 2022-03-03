using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViceArmory.DTO.ResponseObject.Account
{
    public class FileStorageConfigResponseDTO
    {
        public string AccessKey { get; set; }

        public string SecretKey { get; set; }

        public string ServiceURL { get; set; }

        public string SpaceName { get; set; }

        public string DownloadDomainUrl { get; set; }
    }
}
