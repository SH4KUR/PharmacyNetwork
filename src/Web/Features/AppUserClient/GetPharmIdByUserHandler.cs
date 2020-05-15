using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PharmacyNetwork.Infrastructure.Identity;
using PharmacyNetwork.Web.Extensions;

namespace PharmacyNetwork.Web.Features.AppUserClient
{
    public class GetPharmIdByUserHandler : IRequestHandler<GetPharmIdByUser, int?>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetPharmIdByUserHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<int?> Handle(GetPharmIdByUser request, CancellationToken cancellationToken)
        {
            var id = await _userManager.GetPharmacyId(request.UserClaimsPrincipal);

            return id;
        }
    }
}
