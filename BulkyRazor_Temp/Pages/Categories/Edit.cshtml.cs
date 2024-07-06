using BulkyRazor_Temp.Data;
using BulkyRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {

        public Category Category { get; set; }

        private readonly ApplicationRazorDBContext _dbcontext;
        public EditModel(ApplicationRazorDBContext dbcontext)
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
                _dbcontext.Categories.Update(Category);
                _dbcontext.SaveChanges();
                TempData["update"] = "Category updated successfully";
                return RedirectToPage("Index");
            }
            return Page();        }
    }
}
