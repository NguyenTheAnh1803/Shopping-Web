using Microsoft.AspNetCore.Mvc;

namespace Shopping_Web.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
