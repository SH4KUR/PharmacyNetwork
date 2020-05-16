using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using PharmacyNetwork.ApplicationCore.Entities;

namespace PharmacyNetwork.Web.Features.Pharmacy
{
    public class GetMedItemsListInPharm : IRequest<List<PharmacyWharehouse>>
    {
        public GetMedItemsListInPharm(int pharmId)
        {
            PharmId = pharmId;
        }

        public int PharmId { get; set; }
    }
}
