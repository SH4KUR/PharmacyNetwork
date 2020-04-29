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
        public ViewResult Index()
        {
            return View();
        }
    }
}
