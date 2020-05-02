using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using PharmacyNetwork.Web.ViewModels;

namespace PharmacyNetwork.Web.Features.Purchases
{
    public class GetPurchaseCheck : IRequest<PurchaseCheckViewModel>
    {
        public int? Id { get; set; }

        public GetPurchaseCheck(int? id)
        {
            Id = id;
        }
    }
}
