using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }
        public IActionResult Index()
        {
            List<Product> products = _unitOfWork.Product.GetAll().ToList();
            return View(products);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            //if (category.Name == category.DisplayOrder.ToString()) {
            //    ModelState.AddModelError("name", "The Displayorder cannot exactly match the Name");
            //}
            //if (category.Name.ToLower() == "test") {
            //    ModelState.AddModelError("", "Test is an invalid value");
            //}
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(product);
                _unitOfWork.Product.Save();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index", "Product");
            }
            return View();
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _unitOfWork.Product.Get(x => x.Id == id);


            //Category? categoryFromDb2 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category? categoryFromDb3 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            //if (category.Name == category.DisplayOrder.ToString()) {
            //    ModelState.AddModelError("name", "The Displayorder cannot exactly match the Name");
            //}
            //if (category.Name.ToLower() == "test") {
            //    ModelState.AddModelError("", "Test is an invalid value");
            //}
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(product);
                _unitOfWork.Product.Save();

                TempData["success"] = "Product Update Successfully";
                return RedirectToAction("Index", "Product");
            }
            return View();
        }



        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            //Category? categoryFromDb2 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category? categoryFromDb3 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
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


            Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Product.Save();
            TempData["success"] = "Product Deleted Successfully";
            return RedirectToAction("Index", "Product");
            //if (ModelState.IsValid)
            //{
            //    _db.Categories.Update(category);
            //    _db.SaveChanges();
            //  return View();

            //}

        }
    }
}
