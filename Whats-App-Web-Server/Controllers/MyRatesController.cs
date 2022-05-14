using Microsoft.AspNetCore.Mvc;
using Whats_App_Web_Server.Models;
using Whats_App_Web_Server.Services;

namespace Whats_App_Web_Server.Controllers
{
    public class MyRatesController : Controller
    {
        private IRateService service;

        public MyRatesController()
        {
            service = new RateService();

        }


        public IActionResult Index()
        {
            return View(service.GetAll);
        }


        public IActionResult List()
        {
            return View(service.GetAll);
        }


        public IActionResult Details(int id)
        {
            return View(service.Get(id));
        }


        public IActionResult Create()
        {

            return View();
        }


        [HttpPost]
        public IActionResult Create(int rate, string description)
        {
            service.Create(rate, description);


            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            return View(service.Get(id));
        }


        [HttpPost]
        public IActionResult Edit(int id, int rate, string description)
        {
            service.Edit(id, rate, description);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            return View(service.Get(id));
        }


        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteForReal(int id)
        {
            service.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
