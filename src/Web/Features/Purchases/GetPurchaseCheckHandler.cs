using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PharmacyNetwork.ApplicationCore.Entities;
using PharmacyNetwork.ApplicationCore.Interfaces;
using PharmacyNetwork.ApplicationCore.Specifications;
using PharmacyNetwork.Web.ViewModels;

namespace PharmacyNetwork.Web.Features.Purchases
{
    public class GetPurchaseCheckHandler : IRequestHandler<GetPurchaseCheck, PurchaseCheckViewModel>
    {
        private readonly IAsyncRepository<Purchase> _purchaseRepository;
        private readonly IAsyncRepository<Check> _checkRepository;

        public GetPurchaseCheckHandler(IAsyncRepository<Purchase> purchaseRepository, IAsyncRepository<Check> checkRepository)
        {
            _purchaseRepository = purchaseRepository;
            _checkRepository = checkRepository;
        }

        public async Task<PurchaseCheckViewModel> Handle(GetPurchaseCheck request, CancellationToken cancellationToken)
        {
            var checkPurchaseSpecification = new CheckPurchaseSpecification(request.Id);
            var check = await _checkRepository.ListAsync(checkPurchaseSpecification);

            var purchase = await _purchaseRepository.GetByIdAsync(request.Id);

            var purchaseCheckViewModel = new PurchaseCheckViewModel()
            {
                Purchase = purchase,
                Check = check
            };

            return purchaseCheckViewModel;
        }
    }
}
