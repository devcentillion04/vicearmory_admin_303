using Account.DataContract.Entities;
using System.ComponentModel.DataAnnotations;

namespace Authenticate.DataContract
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