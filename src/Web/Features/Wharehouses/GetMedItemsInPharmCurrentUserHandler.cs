using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PharmacyNetwork.ApplicationCore.Entities;
using PharmacyNetwork.ApplicationCore.Interfaces;
using PharmacyNetwork.ApplicationCore.Specifications;
using PharmacyNetwork.Infrastructure.Identity;
using PharmacyNetwork.Web.Extensions;

namespace PharmacyNetwork.Web.Features.Wharehouses
{
    public class GetMedItemsInPharmCurrentUserHandler : IRequestHandler<GetMedItemsInPharmCurrentUser, IEnumerable<PharmacyWharehouse>>
    {
        private readonly IAsyncRepository<PharmacyWharehouse> _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetMedItemsInPharmCurrentUserHandler(IAsyncRepository<PharmacyWharehouse> repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<IEnumerable<PharmacyWharehouse>> Handle(GetMedItemsInPharmCurrentUser request, CancellationToken cancellationToken)
        {
            var idPharm = await _userManager.GetPharmacyId(request.CurrentUser);

            var medItemsByPharmWharehouseSpecification = new MedItemsByPharmWharehouseSpecification(idPharm);

            var list = await _repository.ListAsync(medItemsByPharmWharehouseSpecification);

            return list;
        }
    }
}
