using Microsoft.AspNetCore.Mvc;
using ViceArmory.Middleware.Interface;

namespace ViceArmory.Web.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _IProductService;
        public HomeController(IProductService iProductService)
        {
            _IProductService = iProductService;
        }
        public ActionResult Index()
        {
            //_IProductService.GetProducts(new RequestObject.Product.ProductQueryRequestDTO() { PageNumber = 1 });
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}