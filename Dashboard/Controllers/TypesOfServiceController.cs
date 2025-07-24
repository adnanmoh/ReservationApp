using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    public class TypesOfServiceController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
