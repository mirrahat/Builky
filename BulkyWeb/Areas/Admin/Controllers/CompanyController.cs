using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Company> Company = _unitOfWork.Company.GetAll().ToList();
            return View(Company);
        }

        public IActionResult Upsert(int? Id)
        {



            if (Id == null || Id == 0)
            {
                return View(new Company());
            }
            else
            {
                Company companyObj = _unitOfWork.Company.Get(x => x.Id == Id);
                return View(companyObj);
            }


        }

        [HttpPost]
        public IActionResult Upsert(Company companyObj)
        {

            if (ModelState.IsValid)
            {

                if (companyObj.Id == 0)
                {
                    _unitOfWork.Company.Add(companyObj);
                }
                else
                {
                    _unitOfWork.Company.Update(companyObj);
                }

                _unitOfWork.Company.Save();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(companyObj);
            }

        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Company? CompanyFromDb = _unitOfWork.Company.Get(x => x.Id == id);


            if (CompanyFromDb == null)
            {
                return NotFound();
            }
            return View(CompanyFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Company Company)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Company.Update(Company);
                _unitOfWork.Company.Save();

                TempData["success"] = "Company Update Successfully";
                return RedirectToAction("Index", "Company");
            }
            return View();
        }



        /* public IActionResult Delete(int? id)
         {
             if (id == null || id == 0)
             {
                 return NotFound();
             }
             Company? CompanyFromDb = _unitOfWork.Company.Get(u => u.CompanyId == id);
              if (CompanyFromDb == null)
             {
                 return NotFound();
             }
             return View(CompanyFromDb);
         }*/

        /* [HttpPost, ActionName("Delete")]
         public IActionResult DeletePost(int? id)
         {
             Company? obj = _unitOfWork.Company.Get(u => u.CompanyId == id);
             if (obj == null)
             {
                 return NotFound();
             }
             _unitOfWork.Company.Remove(obj);
             _unitOfWork.Company.Save();
             TempData["success"] = "Company Deleted Successfully";
             return RedirectToAction("Index", "Company");


         }*/


        #region API CALLS
        public IActionResult GetAll()
        {
            List<Company> Companys = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = Companys });
        }

        #endregion


        public IActionResult Delete(int? id)
        {
            var CompanyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            if (CompanyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Company.Remove(CompanyToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleting Successfully" });

        }
    }
}
