using BulkyRazor_Temp.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyRazor_Temp.Data
{
    public class ApplicationRazorDBContext : DbContext
    {

        public ApplicationRazorDBContext(DbContextOptions<ApplicationRazorDBContext> options)
             : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder moedlBuilder)
        {
            moedlBuilder.Entity<Category>().HasData(
               new Category { Id = 1, Name = "Mir", DisplayOrder = 1 },
               new Category { Id = 2, Name = "Mir", DisplayOrder = 2 },
               new Category { Id = 3, Name = "Mir", DisplayOrder = 3 }
              );
        }
    }
}
