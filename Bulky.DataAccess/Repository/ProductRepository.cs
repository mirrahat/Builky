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
                _db.Products.Update(entity);
            var objFormDb=_db.Products.FirstOrDefault(u=>u.ProductId==obj.ProductId)
            }

           
        
    }
}
