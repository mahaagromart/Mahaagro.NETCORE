using Microsoft.AspNetCore.Mvc;

namespace ECOMAPP.Controllers
{
    public class OrderApiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
