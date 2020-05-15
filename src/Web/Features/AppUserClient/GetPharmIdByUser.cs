using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PharmacyNetwork.Web.Features.AppUserClient
{
    public class GetPharmIdByUser : IRequest<int?>
    {
        public GetPharmIdByUser(ClaimsPrincipal userClaimsPrincipal)
        {
            UserClaimsPrincipal = userClaimsPrincipal;
        }

        public ClaimsPrincipal UserClaimsPrincipal { get; set; }
    }
}
