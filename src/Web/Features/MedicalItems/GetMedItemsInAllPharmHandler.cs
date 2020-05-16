using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PharmacyNetwork.ApplicationCore.Entities;
using PharmacyNetwork.ApplicationCore.Interfaces;
using PharmacyNetwork.ApplicationCore.Specifications;

namespace PharmacyNetwork.Web.Features.MedicalItems
{
    public class GetMedItemsInAllPharmHandler : IRequestHandler<GetMedItemsInAllPharm, List<PharmacyWharehouse>>
    {
        private readonly IAsyncRepository<PharmacyWharehouse> _repository;

        public GetMedItemsInAllPharmHandler(IAsyncRepository<PharmacyWharehouse> repository)
        {
            _repository = repository;
        }

        public async Task<List<PharmacyWharehouse>> Handle(GetMedItemsInAllPharm request, CancellationToken cancellationToken)
        {
            var specif = new MedItemsInAllPharmSpecification(request.MedItemsId);
            var list = await _repository.ListAsync(specif);
            return list;
        }
    }
}
