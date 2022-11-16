using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace XYZEngineeringProject.Web.Controllers
{
    [Authorize]
    public class AppUserControllerTest : Controller
    {
        // GET: AppUserControllerTest
        public ActionResult Index()
        {
            return View();
        }

        // GET: AppUserControllerTest/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AppUserControllerTest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppUserControllerTest/Create
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

        // GET: AppUserControllerTest/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AppUserControllerTest/Edit/5
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

        // GET: AppUserControllerTest/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AppUserControllerTest/Delete/5
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
