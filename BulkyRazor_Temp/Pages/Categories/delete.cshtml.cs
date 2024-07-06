using BulkyRazor_Temp.Data;
using BulkyRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace BulkyRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class deleteModel : PageModel
    {

        public Category Category { get; set; }

        private readonly ApplicationRazorDBContext _dbcontext;
        public deleteModel(ApplicationRazorDBContext dbcontext)
        {
            _dbcontext = dbcontext;

        }
        public void OnGet(int? id)
        {
            if (id != null || id != 0)
            {
                Category = _dbcontext.Categories.Find(id);
            }

        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Category obj= _dbcontext.Categories.Find(Category.Id);
                _dbcontext.Categories.Remove(obj);
                _dbcontext.SaveChanges();
                TempData["delete"] = "Category deleted successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
