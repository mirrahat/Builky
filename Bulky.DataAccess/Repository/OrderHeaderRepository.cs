using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.Models;
using BulkyBookWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private ApplicationDBContext _db;
        public OrderHeaderRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }
        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(OrderHeader entity)
        {
            _db.OrderHeader.Update(entity);
        }

		public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
		{
			var orderFromDb=_db.OrderHeader.FirstOrDefault(u=>u.Id==id);
            if (orderFromDb != null) {
            orderFromDb.OrderStatus = orderStatus;
                if (!string.IsNullOrEmpty(paymentStatus)) { 
                orderFromDb.PaymentStatus = paymentStatus;
                }
            
            }
		}

		public void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId)
		{
            var orderFromDb = _db.OrderHeader.FirstOrDefault(u => u.Id == id);
            if (!string.IsNullOrEmpty(sessionId)) {
            orderFromDb.SessionId=sessionId;
            }

            if (!string.IsNullOrEmpty(paymentIntentId)) { 
            orderFromDb.PaymentIntentId=paymentIntentId;
                orderFromDb.PaymentDate=DateTime.Now;
            }
		}
	}
}