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
        private IMediator Mediator { get; }

        public MedicalItemsController(IAsyncRepository<MedicalItem> repository, IMediator mediator)
        {
            _repository = repository;
            Mediator = mediator;
        }

        // GET: MedicalItems
        public async Task<IActionResult> Index(MedicalItemsViewModel medicalItems, int? pageId)
        {
            var medicalItemsViewModel = await Mediator.Send(new GetMedicalItems(pageId ?? 0));  // TODO: Add filter for medicalItems

            return View(medicalItemsViewModel);
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
        public IActionResult Create()
        {
            //ViewData["FirmId"] = new SelectList(_context.Firm, "FirmId", "FirmAddress");
            return View();
        }

        //// POST: MedicalItems/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("MedItemId,FirmId,CategId,MedItemName,MedItemDescrip,MedItemPrice,MedItemPriceMarkup")] MedicalItem medicalItem)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(medicalItem);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CategId"] = new SelectList(_context.ProductCategory, "CategId", "CategName", medicalItem.CategId);
        //    ViewData["FirmId"] = new SelectList(_context.Firm, "FirmId", "FirmAddress", medicalItem.FirmId);
        //    return View(medicalItem);
        //}

        //// GET: MedicalItems/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var medicalItem = await _context.MedicalItem.FindAsync(id);
        //    if (medicalItem == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CategId"] = new SelectList(_context.ProductCategory, "CategId", "CategName", medicalItem.CategId);
        //    ViewData["FirmId"] = new SelectList(_context.Firm, "FirmId", "FirmAddress", medicalItem.FirmId);
        //    return View(medicalItem);
        //}

        //// POST: MedicalItems/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("MedItemId,FirmId,CategId,MedItemName,MedItemDescrip,MedItemPrice,MedItemPriceMarkup")] MedicalItem medicalItem)
        //{
        //    if (id != medicalItem.MedItemId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(medicalItem);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!MedicalItemExists(medicalItem.MedItemId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CategId"] = new SelectList(_context.ProductCategory, "CategId", "CategName", medicalItem.CategId);
        //    ViewData["FirmId"] = new SelectList(_context.Firm, "FirmId", "FirmAddress", medicalItem.FirmId);
        //    return View(medicalItem);
        //}

        //// GET: MedicalItems/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var medicalItem = await _context.MedicalItem
        //        .Include(m => m.Categ)
        //        .Include(m => m.Firm)
        //        .FirstOrDefaultAsync(m => m.MedItemId =   = id);
        //    if (medicalItem == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(medicalItem);
        //}

        //// POST: MedicalItems/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var medicalItem = await _context.MedicalItem.FindAsync(id);
        //    _context.MedicalItem.Remove(medicalItem);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool MedicalItemExists(int id)
        //{
        //    return _context.MedicalItem.Any(e => e.MedItemId == id);
        //}
    }
}
