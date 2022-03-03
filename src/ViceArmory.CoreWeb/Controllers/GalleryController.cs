using Microsoft.AspNetCore.Mvc;


namespace ViceArmory.Web.Controllers
{
    public class GalleryController : Controller
    {
        // GET: gallery
        public ActionResult Index()
        {
            return View();
        }
    }
}