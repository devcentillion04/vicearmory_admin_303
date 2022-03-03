using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViceArmory.DAL.Interface;
using ViceArmory.DTO.RequestObject.ApiConfiguration;
using ViceArmory.DTO.ResponseObject.AppSettings;
using ViceArmory.DTO.ResponseObject.Logs;

namespace ViceArmory.DAL.Repository
{
    public class LogRepository : ILogRepository
    {
        #region Members
        private readonly ILogContext _context;
        //private readonly IAuditLogRepository _iAuditLogRepository;
       
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IApiConfigurationService _iApiConfigurationService;
        private readonly IOptions<ApiConfigurationSetting> _apiConfigurationSetting;
        #endregion

        #region Construction
        public LogRepository(ILogContext context, IHostEnvironment hostEnvironment, IOptions<ApiConfigurationSetting> apiConfigurationSetting, IApiConfigurationService iApiConfigurationService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _iApiConfigurationService = iApiConfigurationService;
            _hostEnvironment = hostEnvironment;
            this._apiConfigurationSetting = apiConfigurationSetting;
            //_iAuditLogRepository = iAuditLogRepository;
        }
        #endregion



        /// <summary>
        /// Create Product
        /// </summary>
        /// <param name="product">Request Object</param>
        /// <returns></returns>
        public async Task AddLogs(LogResponseDTO logs)
        {
          
                await _context.AddLogs.InsertOneAsync(logs); ;
                //var item = _context.Products.Find(f => true).Sort("{ id: -1}").Limit(1).FirstOrDefault();
                //var auditLog = new AuditLog
                //{
                //    ActivityID = product.Id,
                //    AuditActivity = CommonEnums.AuditActivityEnum.Product,
                //    IPAddress = product.IPAddress,
                //    CreatedBy = product.UserId,
                //    CreatedDate = DateTime.Now,
                //    Description = "Add Product."
                //};
                //await _iAuditLogRepository.CreateAuditLog(auditLog);
            }
        }
}
