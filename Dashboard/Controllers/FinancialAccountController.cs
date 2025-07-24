using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    public class FinancialAccountController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
