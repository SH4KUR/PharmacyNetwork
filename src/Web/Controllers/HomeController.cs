using System.Diagnostics;
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
        private readonly IAsyncRepository<MedicalItem> _repository;

        public HomeController(IAsyncRepository<MedicalItem> repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _repository.ListAllAsync();

            return View(list);
        }
    }
}
