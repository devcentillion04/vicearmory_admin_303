
using Microsoft.Extensions.Options;
using Middleware.Infrastructure;
using Newtonsoft.Json;
using System.Threading.Tasks;
using ViceArmory.DTO.ResponseObject.AppSettings;
using ViceArmory.DTO.ResponseObject.Authenticate;
using ViceArmory.DTO.ResponseObject.Logs;
using ViceArmory.Middleware.Interface;

namespace ViceArmory.Middleware.Service
{
    public class LogService: ILogService
    {
        #region Members

        //private readonly IProductRepository _iProductRepository;
        //private readonly FileStorageConfig _fileStorageConfig;
        //private readonly IHostEnvironment _hostEnvironment;
        //private readonly ILogger<ProductRepository> _logger;
        private const string PRODUCT_IMAGE_LOCATION = "{0}/products/{1}/";
        private IOptions<APISettings> _options;
        private AuthenticateResponse _UserInfo;
        #endregion

        #region Construction
        public LogService(IOptions<APISettings> options)
        {
            _options = options;
            //_iProductRepository = iProductRepository;
            //_fileStorageConfig = fileStorageConfig.Value;
            //_hostEnvironment = hostEnvironment;
            //_logger = logger;
        }
        /// <summary>
        /// Create Product
        /// </summary>
        /// <param name="product">Request Object</param>
        /// <returns></returns>
        public async Task<bool> AddLogs(LogResponseDTO logs)
        {
            try
            {
                string responseString = InvokeCallRequest.PostRequestGetResponseString(Constants.ADDLOGS, logs, _options.Value.APIUrl, _UserInfo);
                LogResponseDTO res = JsonConvert.DeserializeObject<LogResponseDTO>(responseString);
                if (responseString.ToLower() == "ok")
                    return true;
                return false;
            }
            catch
            {
                throw;
            }
        }
        #endregion

    }
}
