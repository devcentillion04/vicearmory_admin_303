using Account.DataContract.Entities;
using Account.DataContract.Entities.Enum;
using Account.Repository.Data.Interfaces;
using Account.Repository.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Account.Repository.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly IAccountContext _context;
        public AuditLogRepository(IAccountContext context)
        {
            _context = context;
        }
        public async Task CreateAuditLog(AuditLog req)
        {
            await _context.AuditLogs.InsertOneAsync(req);
        }

        public async Task<IEnumerable<AuditLog>> GetAuditLogs()
        {
            return await _context
                         .AuditLogs.Find(l => true).ToListAsync();

        }

        public async Task<IEnumerable<AuditLog>> GetAuditLogsByActivityAndID(CommonEnums.AuditActivityEnum auditActivityEnum, string ActivityID)
        {
            return await _context
                        .AuditLogs.Find(l => l.AuditActivity.GetHashCode() == auditActivityEnum.GetHashCode() && l.ActivityID == ActivityID).ToListAsync();
        }

        public async Task<IEnumerable<AuditLog>> GetAuditLogsByIPAddress(string IPAddress)
        {
            return await _context
                          .AuditLogs.Find(l => l.IPAddress == IPAddress).ToListAsync();
        }

        public async Task<IEnumerable<AuditLog>> GetAuditLogsByUser(string UserID)
        {
            return await _context
                          .AuditLogs.Find(l => l.CreatedBy == UserID).ToListAsync();
        }
    }
}
