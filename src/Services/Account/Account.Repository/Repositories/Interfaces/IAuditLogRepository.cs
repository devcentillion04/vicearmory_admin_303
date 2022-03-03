using Account.DataContract.Entities;
using Account.DataContract.Entities.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Repository.Repositories.Interfaces
{
    public interface IAuditLogRepository
    {
        Task CreateAuditLog(AuditLog req);
        Task<IEnumerable<AuditLog>> GetAuditLogs();
        Task<IEnumerable<AuditLog>> GetAuditLogsByUser(string UserID);
        Task<IEnumerable<AuditLog>> GetAuditLogsByIPAddress(string IPAddress);
        Task<IEnumerable<AuditLog>> GetAuditLogsByActivityAndID(CommonEnums.AuditActivityEnum auditActivityEnum, string ActivityID);
    }
}
