using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PharmacyNetwork.ApplicationCore.Entities;
using PharmacyNetwork.ApplicationCore.Interfaces;
using PharmacyNetwork.Infrastructure.Identity;
using PharmacyNetwork.Web.Features.Wharehouses;

namespace PharmacyNetwork.Web.Controllers
{
    public class PharmacyWharehousesController : Controller
    {
        private readonly IAsyncRepository<PharmacyWharehouse> _repository;
        private readonly IMediator _mediator;

        public PharmacyWharehousesController(IAsyncRepository<PharmacyWharehouse> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        // GET: PharmacyWharehouses
        public async Task<IActionResult> Index()
        {
            var currentUser = this.User;
            var medItemsList = await _mediator.Send(new GetMedItemsInPharmCurrentUser(currentUser));
            return View(medItemsList);
        }
    }
}
