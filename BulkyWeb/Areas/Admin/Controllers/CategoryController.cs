using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.DataAccess.Data;
using BulkyBook.Models.Models;
using Microsoft.AspNetCore.Mvc;




namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles=SD.Role_Admin)]
    public class CategoryController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }
        public IActionResult Index()
        {
            List<Category> categories = _unitOfWork.Category.GetAll().ToList();
            return View(categories);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            //if (category.Name == category.DisplayOrder.ToString()) {
            //    ModelState.AddModelError("name", "The Displayorder cannot exactly match the Name");
            //}
            //if (category.Name.ToLower() == "test") {
            //    ModelState.AddModelError("", "Test is an invalid value");
            //}
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(category);
                _unitOfWork.Category.Save();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _unitOfWork.Category.Get(x=>x.CategoryId ==id);
         
            //Category? categoryFromDb2 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category? categoryFromDb3 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
            if (categoryFromDb == null)
            {
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
                _unitOfWork.Category.Update(category);
                _unitOfWork.Category.Save();
                TempData["success"] = "Category Update Successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }



        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.CategoryId == id);
            //Category? categoryFromDb2 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category? categoryFromDb3 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            //if (category.Name == category.DisplayOrder.ToString()) {
            //    ModelState.AddModelError("name", "The Displayorder cannot exactly match the Name");
            //}
            //if (category.Name.ToLower() == "test") {
            //    ModelState.AddModelError("", "Test is an invalid value");
            //}


            Category? obj = _unitOfWork.Category.Get(u => u.CategoryId == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Category.Save();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index", "Category");
            //if (ModelState.IsValid)
            //{
            //    _db.Categories.Update(category);
            //    _db.SaveChanges();
            //  return View();

            //}

        }
    }
}
