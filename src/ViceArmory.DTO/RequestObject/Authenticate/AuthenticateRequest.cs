using System.ComponentModel.DataAnnotations;
using ViceArmory.DTO.RequestObject.BaseRequest;

namespace ViceArmory.DTO.RequestObject.Authenticate
{
    /// <summary>
    /// Class used for authentication request
    /// </summary>
    public class AuthenticateRequest: BaseRequestModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}