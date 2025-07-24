using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    public class ResortController : BaseController
    {
        // GET: ResortController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ResortController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ResortController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ResortController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ResortController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ResortController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ResortController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ResortController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
