using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public ShoppingCartVM shoppingCartVM { get; set; }

        public CartController(IUnitOfWork unitofWork)
        {
            _unitOfWork = unitofWork;

        }
		public IActionResult Index()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (userId == null)
			{
				// Handle null user ID, such as redirecting to the login page
				return RedirectToAction("Login", "Account");
			}

			/*shoppingCartVM.ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product");
*/
			shoppingCartVM = new() {
			ShoppingCartList=  _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product"),
			OrderHeader= new()

		};
			// Calculate OrderTotal
			foreach (var cart in shoppingCartVM.ShoppingCartList)
			{
				cart.Price = GetPriceBasedOnQuantity(cart);
				shoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
			}

			return View(shoppingCartVM);
		}


		public IActionResult Summary()
		 {
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

			shoppingCartVM = new()
			{
				ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(
					u => u.ApplicationUserId == userId,
					includeProperties: "Product"
				),
				OrderHeader = new()
			};

			shoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

			foreach (var cart in shoppingCartVM.ShoppingCartList)
			{
				cart.Price = GetPriceBasedOnQuantity(cart);
				shoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
			}

			

			//customer account
			return View(shoppingCartVM);
		}


		[HttpPost]
		[ActionName("Summary")]
		public IActionResult SummaryPost()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			// If userId is null, return an error
			if (userId == null) return BadRequest("User ID not found");

		

			shoppingCartVM.ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId,includeProperties:"Product");

			// Ensure OrderHeader is initialized

			// Retrieve application user details
			var applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);
			if (applicationUser == null) return NotFound("Application user not found");

			// Set OrderHeader properties based on application user info
			shoppingCartVM.OrderHeader.Name = applicationUser.Name;
			shoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);
			shoppingCartVM.OrderHeader.OrderDate=System.DateTime.Now;
			shoppingCartVM.OrderHeader.ApplicationUserId = userId;
			shoppingCartVM.OrderHeader.City=applicationUser.City;
			shoppingCartVM.OrderHeader.PhoneNumber = applicationUser.PhoneNumber;
			shoppingCartVM.OrderHeader.PostalCode = applicationUser.PostalCode;
			shoppingCartVM.OrderHeader.StreetAddress = applicationUser.StreetAddress;
			// Calculate order total
			foreach (var cart in shoppingCartVM.ShoppingCartList)
			{
				cart.Price = GetPriceBasedOnQuantity(cart);
				shoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
			}

			// Set payment and order status based on CompanyId
			if (shoppingCartVM.OrderHeader.ApplicationUser.CompnayId==0)
			{
				shoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
				shoppingCartVM.OrderHeader.OrderStatus = SD.StatusPending;
			}
			else
			{
				shoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusDelayedPayment;
				shoppingCartVM.OrderHeader.OrderStatus = SD.StatusApproved;
			}

			// Add OrderHeader to the database
			_unitOfWork.OrderHeader.Add(shoppingCartVM.OrderHeader);
			_unitOfWork.Save();

			
           foreach (var cart in shoppingCartVM.ShoppingCartList) {

                OrderDetails orderDetails = new OrderDetails
                {

                    ProductId = cart.ProductId,
                    OrderHeadId = shoppingCartVM.OrderHeader.Id,
                    Price = cart.Price,
                    Count = cart.Count,

                };
                _unitOfWork.OrderDetail.Add(orderDetails);
                _unitOfWork.Save();
            }
			if (applicationUser.CompnayId == 0)
			{
				/*ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
				ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusPending;*/
				StripeConfiguration.ApiKey = "sk_test_51QJpFIEjPbFCe1qTGPnk0louMDAswtqBdaeamDIrJ2vCu4hOnWZDcFduTOzUWvi2fN1ouTBNrta2mP540dqXmFvk00b4D0Ebxt";
				var domain = "https://localhost:7195/";
				var options = new SessionCreateOptions
				{
					SuccessUrl = domain+ $"/customer/cart/OrderConfirmation?id={shoppingCartVM.OrderHeader.Id}",
					CancelUrl = domain+"customer/cart/index",
					LineItems = new List<Stripe.Checkout.SessionLineItemOptions>()
	            {
		
	},
					Mode = "payment",
				};

				foreach (var item in shoppingCartVM.ShoppingCartList) {
					var sessionLineItems = new SessionLineItemOptions {
						PriceData = new SessionLineItemPriceDataOptions {
							UnitAmount = (long)(item.Price * 100),
							Currency = "usd",
							ProductData = new SessionLineItemPriceDataProductDataOptions {
								Name = item.Product.Title
							}
						},
					Quantity = item.Count
						
				};
					options.LineItems.Add(sessionLineItems);
				}

				var service = new SessionService();
				Session session = service.Create(options);
				var x = session.Id;
				_unitOfWork.OrderHeader.UpdateStripePaymentId(shoppingCartVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
				_unitOfWork.Save();
				Response.Headers.Add("Location",session.Url);
				return new StatusCodeResult(303);
			}

			return RedirectToAction(nameof(ShoppingCartVM), new { id=shoppingCartVM.OrderHeader.Id });
		}

        public IActionResult OrderConfirmation(int id) {
			OrderHeader orderHeader = _unitOfWork.OrderHeader.Get(u => u.Id==id, includeProperties: "ApplicationUsers");
			if (orderHeader.PaymentStatus != SD.PaymentStatusDelayedPayment) {
				var service = new SessionService();
				Session session = service.Get(orderHeader.SessionId);
				if (session.PaymentStatus.ToLower() == "paid")
				{
					_unitOfWork.OrderHeader.UpdateStripePaymentId(id, session.Id, session.PaymentIntentId);
					_unitOfWork.OrderHeader.UpdateStatus(id, SD.StatusApproved, SD.PaymentStatusApproved);
					_unitOfWork.Save();
				}
			}
			List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCart.GetAll(u=>u.ApplicationUserId==orderHeader.ApplicationUserId).ToList();
			_unitOfWork.ShoppingCart.RemoveRange(shoppingCarts);
			_unitOfWork.Save();
			
            return View(id);
        }

		public IActionResult Plus(int cartId) {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            cartFromDb.Count += 1;
            _unitOfWork.ShoppingCart.Update(cartFromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        
        }

        public IActionResult Minus(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            
            if (cartFromDb.Count <= 1)
            {
                _unitOfWork.ShoppingCart.Remove(cartFromDb);
            }
            else {
                cartFromDb.Count -= 1;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
            }
            _unitOfWork.ShoppingCart.Update(cartFromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Remove(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            _unitOfWork.ShoppingCart.Remove(cartFromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));

        }

        private double GetPriceBasedOnQuantity(ShoppingCart shoppingCart) {
            if (shoppingCart.Count <= 50)
            {

                return shoppingCart.Product.Price;
            }
            else {
                if (shoppingCart.Count <= 100)
                {
                    return shoppingCart.Product.Price50;
                }
                else {
                    return shoppingCart.Product.Price100;
                }
            
            }
        
        }
    }
}
