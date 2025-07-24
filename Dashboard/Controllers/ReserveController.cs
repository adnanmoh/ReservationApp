using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    public class ReserveController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
