using Microsoft.AspNetCore.Identity;
using WebApplication1.Models;

namespace EComDemo.Models
{
	public class Cart
	{
		public int Id { get; set; }

		public string UserId { get; set; }
		public IdentityUser User { get; set; }
		public int ProductId { get; set; }
		public Product Product { get; set; }

		public int Qty { get; set; }
	}
}
