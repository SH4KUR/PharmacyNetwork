using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using PharmacyNetwork.ApplicationCore.Entities;

namespace PharmacyNetwork.Web.Features.MedicalItems
{
    public class GetMedItemsInAllPharm : IRequest<List<PharmacyWharehouse>>
    {
        public GetMedItemsInAllPharm(int? medItemsId)
        {
            MedItemsId = medItemsId;
        }

        public int? MedItemsId { get; set; }
    }
}
