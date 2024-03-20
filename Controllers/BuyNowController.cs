using EComDemo.Models;
using EComDemo.ViewModels;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace EComDemo.Controllers
{
	public class BuyNowController : Controller
	{
		private readonly ApplicationDbContext _context;

		public BuyNowController(ApplicationDbContext context)
		{
			_context = context;
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
				var order = new Order
				{
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
