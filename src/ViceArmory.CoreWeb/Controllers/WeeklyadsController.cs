using Microsoft.AspNetCore.Mvc;

namespace ViceArmory.Web.Controllers
{
    public class WeeklyadsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}