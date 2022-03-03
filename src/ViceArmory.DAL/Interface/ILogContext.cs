using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViceArmory.DTO.ResponseObject.Logs;

namespace ViceArmory.DAL.Interface
{
    public interface ILogContext
    {
        IMongoCollection<LogResponseDTO> AddLogs { get; }
    }
}
