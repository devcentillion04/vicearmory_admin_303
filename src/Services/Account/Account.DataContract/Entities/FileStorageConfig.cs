using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.DataContract.Entities
{
    public class FileStorageConfig
    {
        public string AccessKey { get; set; }

        public string SecretKey { get; set; }

        public string ServiceURL { get; set; }

        public string SpaceName { get; set; }

        public string DownloadDomainUrl { get; set; }
    }
}
