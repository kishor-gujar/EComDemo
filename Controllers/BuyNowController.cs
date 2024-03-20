using EComDemo.Models;
using EComDemo.ViewModels;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace EComDemo.Controllers
{
	public class BuyNowController : Controller
	{
		private readonly ApplicationDbContext _context;
        private UserManager<IdentityUser> _userManager;

        public BuyNowController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

		public async Task<IActionResult> Index(int productId, int qty = 1)
		{
			var product = await _context.Products.FindAsync(productId);
			if (product == null)
			{
				return NotFound();
			}

			var model = new BuyNowViewModel
			{
				product = product
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Index(BuyNowViewModel model, int productId, int qty = 1)
		{
			var product = await _context.Products.FindAsync(productId);
			if (product == null)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
                var currentUser = await _userManager.GetUserAsync(User);

                var order = new Order
				{
					Userid = currentUser.Id,
					FistName = model.FistName,
					Lastname = model.Lastname,
					Address = model.Address,
				};
				_context.Orders.Add(order);
				await _context.SaveChangesAsync();

				//var carts = await _context.Carts.ToListAsync();
				//foreach (var cart in carts)
				//{
				//	var orderProduct1 = new OrderProduct()
				//	{
				//		OrderId = order.Id,
				//		ProductId = cart.ProductId,
				//	};
				//	_context.Add(orderProduct1);
				//}
				//await _context.SaveChangesAsync();

				var orderProduct = new OrderProduct()
				{
					OrderId = order.Id,
					ProductId = product.Id,
					Qty = qty
				};

				_context.OrderProducts.Add(orderProduct);
				await _context.SaveChangesAsync();



                //var cartItems = await _context.Carts
                //    .Where(x => x.UserId == currentUser.Id)
                //    .ToListAsync();

                //_context.Carts.RemoveRange(cartItems);
                //await _context.SaveChangesAsync();

				return RedirectToAction(nameof(ThankYou));
			}

			model.product = product;
			return View(model);
		}

		public IActionResult ThankYou()
		{
			return View();
		}
	}
}
