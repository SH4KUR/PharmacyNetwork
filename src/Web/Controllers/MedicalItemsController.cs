using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using PharmacyNetwork.ApplicationCore.Constants;
using PharmacyNetwork.ApplicationCore.Entities;
using PharmacyNetwork.ApplicationCore.Interfaces;
using PharmacyNetwork.ApplicationCore.Specifications;
using PharmacyNetwork.Infrastructure.Data;
using PharmacyNetwork.Web.Features.MedicalItems;
using PharmacyNetwork.Web.ViewModels;

namespace PharmacyNetwork.Web.Controllers
{
    [Authorize]
    public class MedicalItemsController : Controller
    {
        private readonly IAsyncRepository<MedicalItem> _repository;
        private readonly IMediator _mediator;
        private IIncomesMedItemsService _incomesMedItemsService;

        public MedicalItemsController(IAsyncRepository<MedicalItem> repository, IMediator mediator, IIncomesMedItemsService incomesMedItemsService)
        {
            _repository = repository;
            _mediator = mediator;
            _incomesMedItemsService = incomesMedItemsService;
        }

        // GET: MedicalItems
        public async Task<IActionResult> Index(MedicalItemsListViewModel medicalItemsViewModel, int? pageId)
        {
            var medicalItems = await _mediator.Send(new GetMedicalItemsList(pageId ?? 0, medicalItemsViewModel.CategoryFilterApplied,
                medicalItemsViewModel.FirmFilterApplied)); 

            return View(medicalItems);
        }

        // GET: MedicalItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var medicalItem = await _repository.GetByIdAsync(id);
            if (medicalItem == null) return NotFound();

            return View(medicalItem);
        }

        // GET: MedicalItems/Create
        [Authorize(Roles = AuthorizationConstants.Roles.ADMINSTRATORS)]
        public async Task<IActionResult> Create()
        {
            var medicalItemViewModel = await _mediator.Send(new GetMedicalItem());
            return View(medicalItemViewModel);
        }

        // POST: MedicalItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AuthorizationConstants.Roles.ADMINSTRATORS)]
        public async Task<IActionResult> Create(MedicalItemViewModel medicalItemViewModel)
        {
            if (!ModelState.IsValid) return View(medicalItemViewModel);

            await _repository.AddAsync(medicalItemViewModel.MedicalItem);
            return RedirectToAction(nameof(Index));
        }

        // GET: MedicalItems/Edit/5
        [Authorize(Roles = AuthorizationConstants.Roles.ADMINSTRATORS)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var medicalItemViewModel = await _mediator.Send(new GetMedicalItem(id));
            if (medicalItemViewModel == null) return NotFound();

            return View(medicalItemViewModel);
        }

        // POST: MedicalItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AuthorizationConstants.Roles.ADMINSTRATORS)]
        public async Task<IActionResult> Edit(MedicalItemViewModel medicalItemViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateAsync(medicalItemViewModel.MedicalItem);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicalItemExists(medicalItemViewModel.MedicalItem.MedItemId))
                    {
                        return NotFound();
                    }
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(medicalItemViewModel);
        }

        // GET: MedicalItems/Delete/5
        [Authorize(Roles = AuthorizationConstants.Roles.ADMINSTRATORS)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var medicalItem = await _repository.GetByIdAsync(id);
            if (medicalItem == null) return NotFound();

            return View(medicalItem);
        }

        // POST: MedicalItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AuthorizationConstants.Roles.ADMINSTRATORS)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicalItem = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(medicalItem);
            return RedirectToAction(nameof(Index));
        }

        private bool MedicalItemExists(int id)
        {
            var list = _repository.GetAllAsync().Result;
            return list.Any(m => m.MedItemId == id);
        }
    }
}
