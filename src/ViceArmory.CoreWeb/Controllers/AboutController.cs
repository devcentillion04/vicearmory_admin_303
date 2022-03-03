using Microsoft.AspNetCore.Mvc;

namespace ViceArmory.Web.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult Index()
        {
            return View();
        }
    }
}