using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViceArmory.DTO.ResponseObject.AppSettings
{
    public class ProjectSettings
    {
        public string ProjectUrl { get; set; }

        public string UserProjectUrl { get; set; }
        public string ProductImagePath { get; set; }
        public string smtpAddress { get; set; }
        public int portNumber { get; set; }
        public string userName { get; set; }
        public string passWord { get; set; }
        public string from { get; set; }
        public string fromName { get; set; }
        public string urlPathFrontEnd { get; set; }
        public string UserNameForLog { get; set; }
        public string OTPValue { get; set; }
    }
}
