using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PharmacyNetwork.ApplicationCore.Entities;
using PharmacyNetwork.ApplicationCore.Interfaces;
using PharmacyNetwork.ApplicationCore.Specifications;

namespace PharmacyNetwork.Web.Features.Pharmacy
{
    public class GetMedItemsListInPharmHandler : IRequestHandler<GetMedItemsListInPharm, List<PharmacyWharehouse>>
    {
        private readonly IAsyncRepository<PharmacyWharehouse> _repository;

        public GetMedItemsListInPharmHandler(IAsyncRepository<PharmacyWharehouse> repository)
        {
            _repository = repository;
        }

        public async Task<List<PharmacyWharehouse>> Handle(GetMedItemsListInPharm request, CancellationToken cancellationToken)
        {
            var medItemsByPharmWharehouseSpecification = new MedItemsByPharmWharehouseSpecification(request.PharmId);
            var list = await _repository.ListAsync(medItemsByPharmWharehouseSpecification);
            return list;
        }
    }
}
