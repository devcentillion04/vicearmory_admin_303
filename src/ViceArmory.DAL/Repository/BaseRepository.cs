using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ViceArmory.DAL.Interface;
using ViceArmory.DTO.ResponseObject.Logs;

namespace ViceArmory.DAL.Repository
{
    public class BaseRepository : IBaseRepository
    {
        private readonly ILogContext _logs;
        public BaseRepository(ILogContext logs)
        {
            _logs = logs ?? throw new ArgumentNullException(nameof(logs));
        }
        public async Task AddLogs(string PageName, string Descritpion, string CreatedBy)
        {
            try
            {
                string HostName = Dns.GetHostName();
                IPAddress[] add = Dns.GetHostAddresses(HostName);
                string IpAddress = add[0].ToString();
                string IpAddress2 = add[1].ToString(); 
                

                var logs = new LogResponseDTO()
                {
                    PageName = PageName,
                    Description = Descritpion,
                    HostName = HostName,
                    IPaddress = add[0].ToString(),
                    IPaddress1 = add[1].ToString(),
                    created_by = CreatedBy,
                    Created_date = DateTime.Now
                };
                await _logs.AddLogs.InsertOneAsync(logs);
            }
            catch (Exception ex)
            {
                 new LogResponseDTO();
            }
        }
    }
}
