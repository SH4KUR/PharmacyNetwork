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

namespace PharmacyNetwork.Web.Controllers
{
    [Authorize]
    public class FirmsController : Controller
    {
        private readonly IAsyncRepository<Firm> _repository;

        public FirmsController(IAsyncRepository<Firm> repository)
        {
            _repository = repository;
        }

        // GET: Firms
        public async Task<IActionResult> Index()
        {
            var firms = await _repository.GetAllAsync();
            return View(firms);
        }

        // GET: Firms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var firm = await _repository.GetByIdAsync(id);
            if (firm == null)
            {
                return NotFound();
            }

            return View(firm);
        }

        // GET: Firms/Create
        public IActionResult Create() => View();
        
        // POST: Firms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Firm firm)
        {
            if (!ModelState.IsValid)
            {
                return View(firm);
            }

            await _repository.AddAsync(firm);
            return RedirectToAction(nameof(Index));
        }

        // GET: Firms/Edit/5
        [Authorize(Roles = AuthorizationConstants.Roles.ADMINSTRATORS)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var firm = await _repository.GetByIdAsync(id);
            if (firm == null)
            {
                return NotFound();
            }

            return View(firm);
        }

        // POST: Firms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AuthorizationConstants.Roles.ADMINSTRATORS)]
        public async Task<IActionResult> Edit(Firm firm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateAsync(firm);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FirmExists(firm.FirmId))
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
            return View(firm);
        }

        // GET: Firms/Delete/5
        [Authorize(Roles = AuthorizationConstants.Roles.ADMINSTRATORS)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var firm = await _repository.GetByIdAsync(id);
            if (firm == null)
            {
                return NotFound();
            }

            return View(firm);
        }

        // POST: Firms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AuthorizationConstants.Roles.ADMINSTRATORS)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var firm = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(firm);
            return RedirectToAction(nameof(Index));
        }

        private bool FirmExists(int id)
        {
            var list = _repository.GetAllAsync().Result;
            return list.Any(f => f.FirmId == id);
        }
    }
}