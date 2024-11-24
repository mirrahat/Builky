using BulkyBook.DataAccess.Data;
using BulkyBook.Models.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.DbInitializer
{
    public class DbInitializer:IDbInitializer
    {
        private readonly UserManager<IdentityUser>   _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDBContext _dbContext;


        public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDBContext db) { 
        
        _userManager=userManager;
        _roleManager = roleManager;
        _dbContext=db;
        
        
        }
        public void Initialize() {

            //migrations if they are not applied

            try {
                if (_dbContext.Database.GetPendingMigrations().Count() > 0) {
                _dbContext.Database.Migrate();
                }
            
            }
            catch(Exception e) { }

            //create roles if they are not created
            if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();
                //if roles are not created , 
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@dotnetmaestry.com",
                    Email = "admin@dotnetmaestry.com",
                    Name = "Adhora",
                    PhoneNumber = "0924084923",
                    StreetAddress = "Mason st",
                    State = "vic",
                    PostalCode = "3015",
                    City = "Melbourne"

                }, "Admin123").GetAwaiter().GetResult();

                ApplicationUser user = _dbContext.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@dotnetmaestry.com");
                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
            }

            return;

          

        }
       
    }
}
