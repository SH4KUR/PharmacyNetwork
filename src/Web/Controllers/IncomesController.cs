using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmacyNetwork.ApplicationCore.Entities;
using PharmacyNetwork.ApplicationCore.Interfaces;
using PharmacyNetwork.Infrastructure.Data;
using PharmacyNetwork.Web.Features.Incomes;
using PharmacyNetwork.Web.ViewModels;

namespace PharmacyNetwork.Web.Controllers
{
    [Authorize]
    public class IncomesController : Controller
    {
        private readonly IAsyncRepository<Income> _repository;
        private IMediator Mediator;

        public IncomesController(IAsyncRepository<Income> repository, IMediator mediator)
        {
            _repository = repository;
            Mediator = mediator;
        }

        // GET: Incomes
        public async Task<IActionResult> Index()
        {
            var incomes = await _repository.GetAllAsync();
            return View(incomes);
        }

        // GET: Incomes/Create
        public IActionResult Create() => View();

        //// POST: Incomes/Create
        //public async Task<IActionResult> Create()
        //{

        //}

        // GET: Incomes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var incomeDetailViewModel = await Mediator.Send(new GetIncomeDetail(id));

            if (incomeDetailViewModel.Income == null) return NotFound();

            return View(incomeDetailViewModel);
        }
    }
}
