using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmacyNetwork.ApplicationCore.Entities;
using PharmacyNetwork.ApplicationCore.Interfaces;
using PharmacyNetwork.Infrastructure.Data;
using PharmacyNetwork.Web.Features.Purchases;

namespace PharmacyNetwork.Web.Controllers
{
    public class PurchasesController : Controller
    {
        private readonly IAsyncRepository<Purchase> _repository;
        private readonly IMediator _mediator;

        public PurchasesController(IAsyncRepository<Purchase> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        // GET: Purchases
        public async Task<IActionResult> Index()
        {
            // TODO: For User Role add Purchases by Pharmacy
            var purchases = await _repository.GetAllAsync();
            return View(purchases);
        }

        // GET: Purchases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var purchaseCheckViewModel = await _mediator.Send(new GetPurchaseCheck(id));
            if (purchaseCheckViewModel.Purchase == null) return NotFound();

            return View(purchaseCheckViewModel);
        }

        // GET: Purchases/Create
        public IActionResult Create()
        {
            // TODO: Add Create Action
            return View();
        }

        //// POST: Purchases/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("PurchId,PharmId,PurchDate,PurchAmount,PurchDiscountPercent")] Purchase purchase)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(purchase);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["PharmId"] = new SelectList(_context.Pharmacy, "PharmId", "PharmAddress", purchase.PharmId);
        //    return View(purchase);
        //}
    }
}
