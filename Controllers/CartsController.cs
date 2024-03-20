using EComDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace EComDemo.Controllers;

[Authorize]
public class CartsController : Controller
{
	private readonly ApplicationDbContext _context;
	private UserManager<IdentityUser> _userManager;
	public CartsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
	{
		_context = context;
		_userManager = userManager;
	}

	public async Task<IActionResult> Index()
	{
		var currentUser = await _userManager.GetUserAsync(User);

		var cartItems = await _context.Carts
			.Where(x => x.UserId == currentUser.Id)
			.Include(x => x.Product)
			.ToListAsync();

		return View(cartItems);
	}

	public async Task<IActionResult> AddToCart(int productId,int qty = 1)
	{
		var product = await _context.Products.FindAsync(productId);
		if (product == null)
		{
			return NotFound();
		}

		var currentUser = await _userManager.GetUserAsync(User);
		if (currentUser == null)
		{
			return NotFound();
		}


		var cartItem = new Cart
		{
			UserId = currentUser.Id,
			ProductId = productId,
			Qty = qty
		};

		_context.Carts.Add(cartItem);
		await _context.SaveChangesAsync();

		return RedirectToAction(nameof(Index));
	}
}