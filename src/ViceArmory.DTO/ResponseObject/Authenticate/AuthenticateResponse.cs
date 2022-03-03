
using System;
using ViceArmory.DTO.RequestObject.User;

namespace ViceArmory.DTO.ResponseObject.Authenticate
{
    /// <summary>
    /// Authentication response
    /// </summary>
    public class AuthenticateResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public DateTime UserLoggedinStartTime { get; set; }
        public DateTime UserLoggedinEndTime { get; set; }
        public string TokenId { get; set; }
        public string IpAddress { get; set; }
        public string EmailId { get; set; }

        ///// <summary>
        ///// Initiate class
        ///// </summary>
        ///// <param name="user">UserLogin object</param>
        //public AuthenticateResponse(UserLogin user)
        //{
        //    Id = user._id;
        //    TokenId = user.TokenId;
        //    UserName = user.UserName;
        //    UserLoggedinEndTime = user.UserLoggedinEndTime;
        //    UserLoggedinStartTime = user.UserLoggedinStartTime;
        //    IpAddress = user.IpAddress;
        //}
    }
}