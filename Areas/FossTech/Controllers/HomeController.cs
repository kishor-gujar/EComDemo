using Microsoft.AspNetCore.Mvc;
using WebApplication1.Areas.FossTech.ViewModels;

namespace WebApplication1.Areas.FossTech.Controllers
{
    [Area("FossTech")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var data = new AdminHomeViewModel
            {
                EarningsM = 40000,
                EarningsA = 215000,
                Tasks = 50,
                PendingRequests = 10,
            };

            return View(data);
        }
    }
}
