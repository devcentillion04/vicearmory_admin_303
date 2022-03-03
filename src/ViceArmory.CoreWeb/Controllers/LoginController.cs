using Microsoft.AspNetCore.Mvc;
using ViceArmory.DTO.RequestObject.Authenticate;

namespace ViceArmory.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View(new AuthenticateRequest());
        }
    }
}