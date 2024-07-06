using BulkyRazor_Temp.Data;
using BulkyRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BulkyRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {

       
        public Category Category { get; set; }

        private readonly ApplicationRazorDBContext _dbcontext;
        public CreateModel(ApplicationRazorDBContext dbcontext)
        {
            _dbcontext = dbcontext;

        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            _dbcontext.Categories.Add(Category);
            _dbcontext.SaveChanges();
            TempData["success"] = "Category added successfully";
            return RedirectToPage("Index");
        }
    }
}
