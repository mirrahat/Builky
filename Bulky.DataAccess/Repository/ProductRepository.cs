using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
  
        public class ProductRepository : Repository<Product>, IProductRepository
        {
            private ApplicationDBContext _db;
            public ProductRepository(ApplicationDBContext db) : base(db)
            {
                _db = db;
            }
            public void Save()
            {
                _db.SaveChanges();
            }

            public void Update(Product obj)
            {
                _db.Products.Update(obj);
            var objFormDb = _db.Products.FirstOrDefault(u => u.ProductId == obj.ProductId);
            if (objFormDb != null)
            {
                objFormDb.Title= obj.Title;
                objFormDb.Description= obj.Description;
                objFormDb.Category= obj.Category;  
                objFormDb.Price= obj.Price;
                objFormDb.Price100= obj.Price100;
                objFormDb.Price50= obj.Price50;
                objFormDb.ISBN= obj.ISBN;
                objFormDb.Author= obj.Author;
                if (obj.ImageUrl != null)
                {
                    objFormDb.ImageUrl= obj.ImageUrl;
                }
            }
            }

           
        
    }
}
