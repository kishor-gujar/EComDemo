using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace EComDemo.ViewModels
{
	public class BuyNowViewModel
	{
		[Required]
		public string FistName { get; set; }
		[Required]
		public string Lastname { get; set; }
		[Required]
		public string Address { get; set; }
		public Product product { get; set; }
	}
}
