using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
