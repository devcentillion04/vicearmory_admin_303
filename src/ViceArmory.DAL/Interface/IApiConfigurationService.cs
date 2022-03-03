using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViceArmory.DTO.RequestObject.ApiConfiguration;

namespace ViceArmory.DAL.Interface
{
    public interface IApiConfigurationService
    {
        Task<ApiConfigToken> GetAccessToken();
        Task<ApiConfigToken> GetAccessTokenFromSession();
    }
}
