using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;


namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ApplicationDBContext _db;

        public CategoryController(ApplicationDBContext db) {  _db = db; }
        public IActionResult Index()
        {

            List<Category> categories = _db.Categories.ToList();
            return View(categories);
        }

        public IActionResult Create() {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category) {
            if (category.Name == category.DisplayOrder.ToString()) {
                ModelState.AddModelError("name", "The Displayorder cannot exactly match the Name");
            }
            if (ModelState.IsValid) {
                _db.Categories.Add(category);
                _db.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
    }
}
