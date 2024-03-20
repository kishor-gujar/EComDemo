using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace EComDemo.Controllers
{
	public class ProductsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ProductsController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> Details(int id)
		{
			var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

			if (product == null)
			{
				return NotFound();
			}
			return View(product);
		}
	}
}
