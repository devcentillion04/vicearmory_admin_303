using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViceArmory.DTO.RequestObject.BaseRequest;

namespace ViceArmory.DTO.RequestObject.OTP
{
    public class OTPRequestDTO: BaseRequestModel
    {
        public int OTPText { get; set; }
    }
}
