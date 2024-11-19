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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        { _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> products = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return View(products);
        }

        public IActionResult Upsert(int? Id)
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

            if (Id == null || Id == 0)
            {
                return View(productVM);
            }
            else {
                productVM.Product = _unitOfWork.Product.Get(x => x.ProductId == Id);
                return View(productVM);
            }

           
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVm, IFormFile? file)
        {
           
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;// access the www root path
                if (file != null)
                { 
                    string fileName=Guid.NewGuid().ToString()+ Path.GetExtension(file.FileName);//final image name
                    string productPath= Path.Combine(wwwRootPath, @"images\product");//location where to save

                    if (!string.IsNullOrEmpty(productVm.Product.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, productVm.Product.ImageUrl.Trim('\\'));
                        if (System.IO.File.Exists(oldImagePath)) 
                        {
                        System.IO.File.Delete(oldImagePath);
                        }
                    }

                    //save that image
                    //filemode actually creating a new file there
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {

                        file.CopyTo(fileStream);
                    }
                    //save the filename into productVmmodel
                    productVm.Product.ImageUrl = @"\images\product\" + fileName;
                }
                if (productVm.Product.ProductId == 0)
                {
                    _unitOfWork.Product.Add(productVm.Product);
                }
                else {
                    _unitOfWork.Product.Update(productVm.Product);
                }
               
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



        /* public IActionResult Delete(int? id)
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
         }*/

       /* [HttpPost, ActionName("Delete")]
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


        }*/


        #region API CALLS
        public IActionResult GetAll()
        {
            List<OrderHeader> orjOrderHeader = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();
            return Json(new { data = orjOrderHeader });
        }

        #endregion

       
        public IActionResult Delete(int? id) {
            var productToBeDeleted = _unitOfWork.Product.Get(u => u.ProductId == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.Trim('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleting Successfully" });

        }
    }
}
