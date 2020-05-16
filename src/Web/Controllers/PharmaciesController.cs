using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmacyNetwork.ApplicationCore.Constants;
using PharmacyNetwork.ApplicationCore.Entities;
using PharmacyNetwork.ApplicationCore.Interfaces;
using PharmacyNetwork.Infrastructure.Data;
using PharmacyNetwork.Web.Features.Pharmacy;
using PharmacyNetwork.Web.ViewModels;

namespace PharmacyNetwork.Web.Controllers
{
    [Authorize]
    public class PharmaciesController : Controller
    {
        private readonly IAsyncRepository<Pharmacy> _repository;
        private readonly IMediator _mediator;

        public PharmaciesController(IAsyncRepository<Pharmacy> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        // GET: Pharmacies
        public async Task<IActionResult> Index()
        {
            var pharmacy = await _repository.GetAllAsync();
            return View(pharmacy);
        }

        // GET: Pharmacies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var pharmacy = await _repository.GetByIdAsync(id);
            if (pharmacy == null) return NotFound();

            var viewModel = new PharmacyViewModel()
            {
                Pharmacy = pharmacy,
                MedicalItems = await _mediator.Send(new GetMedItemsListInPharm(pharmacy.PharmId))
            };

            return View(viewModel);
        }

        // GET: Pharmacies/Create
        [Authorize(Roles = AuthorizationConstants.Roles.ADMINSTRATORS)]
        public IActionResult Create() => View();

        // POST: Pharmacies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AuthorizationConstants.Roles.ADMINSTRATORS)]
        public async Task<IActionResult> Create(Pharmacy pharmacy)
        {
            if (!ModelState.IsValid) return View(pharmacy);

            await _repository.AddAsync(pharmacy);
            return RedirectToAction(nameof(Index));
        }

        // GET: Pharmacies/Edit/5
        [Authorize(Roles = AuthorizationConstants.Roles.ADMINSTRATORS)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var pharmacy = await _repository.GetByIdAsync(id);
            if (pharmacy == null) return NotFound();
            return View(pharmacy);
        }

        // POST: Pharmacies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AuthorizationConstants.Roles.ADMINSTRATORS)]
        public async Task<IActionResult> Edit(Pharmacy pharmacy)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateAsync(pharmacy);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PharmacyExists(pharmacy.PharmId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pharmacy);
        }

        // GET: Pharmacies/Delete/5
        [Authorize(Roles = AuthorizationConstants.Roles.ADMINSTRATORS)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var pharmacy = await _repository.GetByIdAsync(id);
            if (pharmacy == null) return NotFound();

            return View(pharmacy);
        }

        // POST: Pharmacies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AuthorizationConstants.Roles.ADMINSTRATORS)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pharmacy = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(pharmacy);
            return RedirectToAction(nameof(Index));
        }

        private bool PharmacyExists(int id)
        {
            var list = _repository.GetAllAsync().Result;
            return list.Any(e => e.PharmId == id);
        }

        public async Task<IActionResult> Transfer(int medItemId, int pharmId)
        {
            var viewModel = await _mediator.Send(new GetTransferViewModel(pharmId, medItemId));
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmTransfer(TransferViewModel transferViewModel)
        {
            var query = $"EXEC transfer_med_item_beetween_pharmacy " +
                        $"{transferViewModel.Pharmacy.PharmId}, {transferViewModel.TransferPharmId}, {transferViewModel.MedicalItem.MedItemId}, {transferViewModel.TransferItemCount};";
            
            await _repository.ExecuteSqlRawAsync(query);

            return RedirectToAction("Details", new {id = transferViewModel.Pharmacy.PharmId});
        }
    }
}
