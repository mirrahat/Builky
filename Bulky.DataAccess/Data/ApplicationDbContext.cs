using BulkyBook.Models;
using BulkyBook.Models.Models;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.DataAccess.Data
{
    public class ApplicationDBContext:IdentityDbContext<IdentityUser>
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base (options)
        {
                
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Company> Company { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderHeader> OrderHeader { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder moedlBuilder) {

            base.OnModelCreating(moedlBuilder);
            moedlBuilder.Entity<Category>().HasData(
               new Category { CategoryId = 1, Name = "Mir", DisplayOrder = 1 },
               new Category { CategoryId = 2, Name = "Mir", DisplayOrder = 2 },
               new Category { CategoryId = 3, Name = "Mir", DisplayOrder = 3 }
              );

            moedlBuilder.Entity<Company>().HasData(
                new Company { Id = 11, Name = "Mir", StreeAddress = "lonnn", City="Newport", PostalCode="3015", PhoneNumber="018454594509" },
                new Company { Id = 22, Name = "Mir", StreeAddress = "lonnn", City = "Newport", PostalCode = "3015", PhoneNumber = "018454594509" },
                new Company { Id = 33, Name = "Mir", StreeAddress = "lonnn", City = "Newport", PostalCode = "3015", PhoneNumber = "018454594509" }
             );

            moedlBuilder.Entity<Product>().HasData(
             new Product
             {
                 ProductId = 1,
                 Title = "Fortune of Time",
                 Author = "Billy Spark",
                 Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                 ISBN = "SWD9999001",
                 ListPrice = 99,
                 Price = 90,
                 Price50 = 85,
                 Price100 = 80,
                 CategoryId = 1,
                 ImageUrl = ""
             },
                new Product
                {
                    ProductId = 2,
                    Title = "Dark Skies",
                    Author = "Nancy Hoover",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "CAW777777701",
                    ListPrice = 40,
                    Price = 30,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryId = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    ProductId = 3,
                    Title = "Vanish in the Sunset",
                    Author = "Julian Button",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "RITO5555501",
                    ListPrice = 55,
                    Price = 50,
                    Price50 = 40,
                    Price100 = 35,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    ProductId = 4,
                    Title = "Cotton Candy",
                    Author = "Abby Muscles",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "WS3333333301",
                    ListPrice = 70,
                    Price = 65,
                    Price50 = 60,
                    Price100 = 55,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    ProductId = 5,
                    Title = "Rock in the Ocean",
                    Author = "Ron Parker",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "SOTJ1111111101",
                    ListPrice = 30,
                    Price = 27,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryId = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    ProductId = 6,
                    Title = "Leaves and Wonders",
                    Author = "Laura Phantom",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "FOT000000001",
                    ListPrice = 25,
                    Price = 23,
                    Price50 = 22,
                    Price100 = 20,
                    CategoryId = 1,
                    ImageUrl = ""
                }
             );


            moedlBuilder.Entity<Company>().HasData(
            new Company
            {
                Id = 111,
                Name = "Mir1",
                StreeAddress = "lonn1n",
                City = "Newpo1rt",
                PostalCode = "30115",
                PhoneNumber = "0118454594509"
            },
               new Company
               {

                   Id = 222,
                   Name = "Mir2",
                   StreeAddress = "l2onnn",
                   City = "Newpor2t",
                   PostalCode = "32015",
                   PhoneNumber = "0218454594509"
               },
               new Company
               {
                   Id = 333,
                   Name = "Mir3",
                   StreeAddress = "lonnn3",
                   City = "Newport3",
                   PostalCode = "30315",
                   PhoneNumber = "0318454594509"
               }
              
            );
        }
    }
}
