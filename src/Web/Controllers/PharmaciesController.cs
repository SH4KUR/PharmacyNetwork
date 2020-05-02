using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmacyNetwork.ApplicationCore.Constants;
using PharmacyNetwork.ApplicationCore.Entities;
using PharmacyNetwork.ApplicationCore.Interfaces;
using PharmacyNetwork.Infrastructure.Data;

namespace PharmacyNetwork.Web.Controllers
{
    [Authorize]
    public class PharmaciesController : Controller
    {
        private readonly IAsyncRepository<Pharmacy> _repository;

        public PharmaciesController(IAsyncRepository<Pharmacy> repository)
        {
            _repository = repository;
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

            return View(pharmacy);
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
    }
}
