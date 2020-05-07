using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using PharmacyNetwork.ApplicationCore.Entities;

namespace PharmacyNetwork.Web.Features.Wharehouses
{
    public class GetMedItemsInPharmCurrentUser : IRequest<IEnumerable<PharmacyWharehouse>>
    {
        public ClaimsPrincipal CurrentUser { get; set; }

        public GetMedItemsInPharmCurrentUser(ClaimsPrincipal currentUser)
        {
            CurrentUser = currentUser;
        }
    }
}
