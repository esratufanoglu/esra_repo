using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AltamiraProductWebApp.Models;
using AltamiraShared.Models;
using Microsoft.Extensions.Options;
using AltamiraProductWebApp.Factory;
using AltamiraProductWebApp.Utility;
using AltamiraShared.Contracts;

namespace AltamiraProductWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<ApiSettingsModel> _apiSettings;
        private ILoggerManager _logger;

        public HomeController(IOptions<ApiSettingsModel> apiSettings, ILoggerManager logger)
        {            
            _apiSettings = apiSettings;
            _logger = logger;
            ApplicationSettings.WebApiUrl = _apiSettings.Value.ProductApiBaseUrl;
        }
        public async Task<IActionResult> Index(String keyword)
        {
            // keyword = "" ile Api nin search metodunu çağır
            var data = await ApiClientFactory.Instance.GetProducts();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        public async Task<ActionResult> Update(int id)
        {
            var product = await ApiClientFactory.Instance.GetProduct(id);
            _logger.LogInfo("Here is info message from our values controller.");
            if (product == null)
                throw new Exception("Product not found!");
            return View(product);
        }

        [HttpPost]
        public ActionResult UpdateProduct(Product prd)
        {
            // prd ile apinin update metodunu çağır
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(Product prd)
        {
            var result = await ApiClientFactory.Instance.CreateProduct(prd);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Delete(int id)
        {
            // prd ile apinin delete metodunu çağır
            return RedirectToAction("Index", "Home");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
