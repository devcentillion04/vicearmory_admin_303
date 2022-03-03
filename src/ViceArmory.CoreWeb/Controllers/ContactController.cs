using Microsoft.AspNetCore.Mvc;

namespace ViceArmory.Web.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }
    }
}