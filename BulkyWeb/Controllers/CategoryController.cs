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
            //if (category.Name == category.DisplayOrder.ToString()) {
            //    ModelState.AddModelError("name", "The Displayorder cannot exactly match the Name");
            //}
            //if (category.Name.ToLower() == "test") {
            //    ModelState.AddModelError("", "Test is an invalid value");
            //}
            if (ModelState.IsValid) {
                _db.Categories.Add(category);
                _db.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
            return View();
        }


        public IActionResult Edit(int? id)
        {
            if (id==null || id == 0) {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
            //Category? categoryFromDb2 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category? categoryFromDb3 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
            if (categoryFromDb == null) {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            //if (category.Name == category.DisplayOrder.ToString()) {
            //    ModelState.AddModelError("name", "The Displayorder cannot exactly match the Name");
            //}
            //if (category.Name.ToLower() == "test") {
            //    ModelState.AddModelError("", "Test is an invalid value");
            //}
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
    }
}
