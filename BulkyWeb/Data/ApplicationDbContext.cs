



using BulkyWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Data
{
    public class ApplicationDBContext:DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base (options)
        {
                
        }

        public DbSet<Category> Categories { get; set; }
    
        protected override void OnModelCreating(ModelBuilder moedlBuilder) {
            moedlBuilder.Entity<Category>().HasData(
               new Category { Id = 1, Name = "Mir", DisplayOrder = 1 },
               new Category { Id = 2, Name = "Mir", DisplayOrder = 2 },
               new Category { Id = 3, Name = "Mir", DisplayOrder = 3 }
              );
        }
    }
}
