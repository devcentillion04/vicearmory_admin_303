using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViceArmory.RequestObject.Account
{
    public class FileStorageConfigRequestDTO
    {
        public string AccessKey { get; set; }

        public string SecretKey { get; set; }

        public string ServiceURL { get; set; }

        public string SpaceName { get; set; }

        public string DownloadDomainUrl { get; set; }
    }
}
