using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViceArmory.DTO.ResponseObject.Authenticate;
using ViceArmory.DTO.ResponseObject.Logs;

namespace ViceArmory.Middleware.Interface
{
    public interface ILogService
    {
        //void SetSession(AuthenticateResponse req);
        /// <summary>
        /// Create Product
        /// </summary>
        /// <param name="product">Request Object</param>
        /// <returns></returns>
        Task<bool> AddLogs(LogResponseDTO logs);
    }
}
