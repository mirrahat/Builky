using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;



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
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(
               u => new SelectListItem
               {
                   Text = u.Name,
                   Value = u.CategoryId.ToString(),
               });


            ProductVM productVM = new ProductVM
            {
                CategoryList = CategoryList,
                Product = new Product()
                
            };

            return View(productVM);
        }

        [HttpPost]
        public IActionResult Create(ProductVM productVm)
        {
            //if (category.Name == category.DisplayOrder.ToString()) {
            //    ModelState.AddModelError("name", "The Displayorder cannot exactly match the Name");
            //}
            //if (category.Name.ToLower() == "test") {
            //    ModelState.AddModelError("", "Test is an invalid value");
            //}
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(productVm.Product);
                _unitOfWork.Product.Save();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            else {

                productVm.CategoryList = _unitOfWork.Category.GetAll().Select(
           u => new SelectListItem
           {
               Text = u.Name,
               Value = u.CategoryId.ToString(),
           });
               

                
                return View(productVm);
            }
           
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _unitOfWork.Product.Get(x => x.ProductId == id);


             if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            
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
            Product? productFromDb = _unitOfWork.Product.Get(u => u.ProductId == id);
             if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product? obj = _unitOfWork.Product.Get(u => u.ProductId == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Product.Save();
            TempData["success"] = "Product Deleted Successfully";
            return RedirectToAction("Index", "Product");
            

        }
    }
}
