using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using PharmacyNetwork.Web.ViewModels;

namespace PharmacyNetwork.Web.Features.Pharmacy
{
    public class GetTransferViewModel : IRequest<TransferViewModel>
    {
        public GetTransferViewModel(int pharmacyId, int medItemId)
        {
            PharmacyId = pharmacyId;
            MedItemId = medItemId;
        }

        public int MedItemId { get; set; }

        public int PharmacyId { get; set; }
    }
}
