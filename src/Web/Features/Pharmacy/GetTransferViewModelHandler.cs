using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using PharmacyNetwork.ApplicationCore.Entities;
using PharmacyNetwork.ApplicationCore.Interfaces;
using PharmacyNetwork.Web.ViewModels;

namespace PharmacyNetwork.Web.Features.Pharmacy
{
    public class GetTransferViewModelHandler : IRequestHandler<GetTransferViewModel, TransferViewModel>
    {
        private readonly IAsyncRepository<PharmacyWharehouse> _pharmWharehouseRepository;
        private readonly IAsyncRepository<ApplicationCore.Entities.Pharmacy> _pharmRepository;


        public GetTransferViewModelHandler(IAsyncRepository<PharmacyWharehouse> repository, IAsyncRepository<ApplicationCore.Entities.Pharmacy> pharmRepository)
        {
            _pharmWharehouseRepository = repository;
            _pharmRepository = pharmRepository;
        }

        public async Task<TransferViewModel> Handle(GetTransferViewModel request, CancellationToken cancellationToken)
        {
            var pharmacyWharehouses = await _pharmWharehouseRepository.GetAllAsync();

            var pharmacy = pharmacyWharehouses.Find(p => p.PharmId == request.PharmacyId).Pharm;
            var medItem = pharmacyWharehouses.Find(i => i.MedItemId == request.MedItemId).MedItem;

            var maxItemCount = pharmacyWharehouses
                .Find(i => request.MedItemId == i.MedItemId && request.PharmacyId == i.PharmId).ItemCount;

            var viewModel = new TransferViewModel()
            {
                Pharmacy = pharmacy,
                MedicalItem = medItem,
                MaxItemCount = maxItemCount,
                Pharmacies = await GetCategoriesSelectListItem(request.PharmacyId),
                TransferItemCount = 1
            };

            return viewModel;
        }

        private async Task<IEnumerable<SelectListItem>> GetCategoriesSelectListItem(int pharmId)
        {
            var pharmacies = await _pharmRepository.GetAllAsync();

            var list = (from item in pharmacies 
                        where item.PharmId != pharmId 
                        select new SelectListItem(item.PharmName, item.PharmId.ToString()))
                .ToList();

            return list;
        }
    }
}
