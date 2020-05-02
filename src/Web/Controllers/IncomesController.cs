using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmacyNetwork.ApplicationCore.Entities;
using PharmacyNetwork.ApplicationCore.Interfaces;
using PharmacyNetwork.Infrastructure.Data;

namespace PharmacyNetwork.Web.Controllers
{
    [Authorize]
    public class IncomesController : Controller
    {
        private readonly IAsyncRepository<Income> _repository;

        public IncomesController(IAsyncRepository<Income> repository)
        {
            _repository = repository;
        }

        // GET: Incomes
        public async Task<IActionResult> Index()
        {
            var incomes = await _repository.GetAllAsync();
            return View(incomes);
        }

        //// GET: Incomes/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var income = await _context.Income
        //        .Include(i => i.Pharm)
        //        .FirstOrDefaultAsync(m => m.IncomeId == id);
        //    if (income == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(income);
        //}
    }
}
