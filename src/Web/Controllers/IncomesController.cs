using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmacyNetwork.ApplicationCore.Constants;
using PharmacyNetwork.ApplicationCore.Entities;
using PharmacyNetwork.ApplicationCore.Interfaces;
using PharmacyNetwork.Infrastructure.Data;
using PharmacyNetwork.Web.Extensions;
using PharmacyNetwork.Web.Features.Incomes;
using PharmacyNetwork.Web.Models;
using PharmacyNetwork.Web.ViewModels;

namespace PharmacyNetwork.Web.Controllers
{
    [Authorize]
    public class IncomesController : Controller
    {
        private readonly IAsyncRepository<Income> _repository;
        private readonly IMediator _mediator;

        public IncomesController(IAsyncRepository<Income> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        // GET: Incomes
        public async Task<IActionResult> Index()
        {
            var incomes = await _repository.GetAllAsync();
            return View(incomes);
        }

        // GET: Incomes/Create
        public async Task<IActionResult> Create(int? pharmacyId)
        {
            var listIncomes = HttpContext.Session.Get<List<IncomeItem>>("Incomes");

            if (listIncomes.IsNullOrEmpty())
            {
                listIncomes = new List<IncomeItem>();
                HttpContext.Session.Set<List<IncomeItem>>("Incomes", listIncomes);
            }

            var viewModel = await _mediator.Send(new GetIncomesCreateViewModel(listIncomes, pharmacyId));
            return View(viewModel);
        }

        // GET: Incomes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var incomeDetailViewModel = await _mediator.Send(new GetIncomeDetail(id));
            if (incomeDetailViewModel.Income == null) return NotFound();

            return View(incomeDetailViewModel);
        }

        [Authorize(Roles = AuthorizationConstants.Roles.ADMINSTRATORS)]
        public IActionResult AddToIncome(int medItemId, int count)
        {
            var incomeItem = new IncomeItem()
            {
                MedicalItemId = medItemId,
                Count = count
            };

            var incomeList = HttpContext.Session.Get<List<IncomeItem>>("Incomes");

            if (incomeList.Any(i => i.MedicalItemId == incomeItem.MedicalItemId))
            {
                incomeList.Find(i => i.MedicalItemId == incomeItem.MedicalItemId).Count += incomeItem.Count;
            }
            else
            {
                incomeList.Add(incomeItem);
            }
            
            HttpContext.Session.Set("Incomes", incomeList);

            return RedirectToAction(nameof(Create));
        }

        [Authorize(Roles = AuthorizationConstants.Roles.ADMINSTRATORS)]
        public IActionResult DeleteFromIncome(int id)
        {
            var incomeList = HttpContext.Session.Get<List<IncomeItem>>("Incomes");

            incomeList.RemoveAll(i => i.MedicalItemId == id);

            HttpContext.Session.Set("Incomes", incomeList);

            return RedirectToAction(nameof(Create));
        }

        [Authorize(Roles = AuthorizationConstants.Roles.ADMINSTRATORS)]
        public async Task<IActionResult> CreateIncomes(int idPharm)
        {
            var income = HttpContext.Session.Get<List<IncomeItem>>("Incomes");

            var nowDateTime = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");

            var query = $"INSERT INTO income(pharm_id, income_date) VALUES({idPharm}, '{nowDateTime}');";

            await _repository.ExecuteSqlRawAsync(query);

            var incomes = await _repository.GetAllAsync();

            var idIncome = incomes.First(p => p.IncomeDate.ToString("yyyyMMdd HH:mm:ss") == nowDateTime).IncomeId;

            foreach (var item in income)
            {
                await _repository.ExecuteSqlRawAsync($"EXEC add_income_detail {idIncome}, {item.MedicalItemId}, {item.Count}, {idPharm};");
            }

            ClearIncome();

            return RedirectToAction("Details", "Incomes", new { id = idIncome });
        }

        [Authorize(Roles = AuthorizationConstants.Roles.ADMINSTRATORS)]
        private void ClearIncome()
        {
            var incomeList = HttpContext.Session.Get<List<IncomeItem>>("Incomes");
            incomeList.Clear();
            HttpContext.Session.Set("Incomes", incomeList);
        }
    }
}
