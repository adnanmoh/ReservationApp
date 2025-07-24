using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    public class AppUserController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
