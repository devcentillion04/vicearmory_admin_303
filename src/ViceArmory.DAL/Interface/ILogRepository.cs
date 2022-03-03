using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViceArmory.DTO.ResponseObject.Logs;

namespace ViceArmory.DAL.Interface
{
    public interface ILogRepository
    {
        /// <summary>
        /// Add logs
        /// </summary>
        /// <param name="Addlogs">Request object</param>
        /// <returns></returns>
        Task AddLogs(LogResponseDTO logs);
}
}
