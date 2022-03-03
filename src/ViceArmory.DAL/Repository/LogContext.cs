using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViceArmory.DAL.Interface;
using ViceArmory.DTO.ResponseObject.AppSettings;
using ViceArmory.DTO.ResponseObject.Logs;

namespace ViceArmory.DAL.Repository
{
    public class LogContext : ILogContext
    {
        public LogContext(IOptions<DatabaseSettings> databaseSettings)
        {
            var client = new MongoClient(databaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(databaseSettings.Value.DatabaseName);
            AddLogs = database.GetCollection<LogResponseDTO>("Logs");
        }

        public IMongoCollection<LogResponseDTO> AddLogs { get; }
    }
}
