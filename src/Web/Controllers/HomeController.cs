using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PharmacyNetwork.ApplicationCore.Entities;
using PharmacyNetwork.ApplicationCore.Interfaces;
using Web.Models;

namespace PharmacyNetwork.Web.Controllers
{
    public class HomeController : Controller
    {
        private IAsyncRepository<MedicalItem> _repository;
        private readonly IAppLogger<HomeController> _logger;

        public HomeController(IAsyncRepository<MedicalItem> repository, IAppLogger<HomeController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _repository.GetAllAsync();

            _logger.LogInformation("Medical Items on view");

            return View(list);
        }
    }
}
