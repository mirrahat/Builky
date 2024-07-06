using BulkyRazor_Temp.Data;
using BulkyRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyRazor_Temp.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationRazorDBContext _dbcontext;
        public List<Category> CategoryList { get; set; }

        public IndexModel(ApplicationRazorDBContext dbcontext)
        {
            _dbcontext = dbcontext;
           
        }

        public void OnGet()
        {
            CategoryList = _dbcontext.Categories.ToList();
        }
    }
}
