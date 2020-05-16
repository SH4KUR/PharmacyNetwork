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

namespace PharmacyNetwork.Web.Features.Incomes
{
    public class GetIncomesCreateViewModelHandler : IRequestHandler<GetIncomesCreateViewModel, CreateIncomeViewModel>
    {
        private readonly IAsyncRepository<MedicalItem> _medItemsRepository;
        private readonly IAsyncRepository<ApplicationCore.Entities.Pharmacy> _pharmRepository;

        public GetIncomesCreateViewModelHandler(IAsyncRepository<ApplicationCore.Entities.Pharmacy> pharmRepository, IAsyncRepository<MedicalItem> medItemsRepository)
        {
            _pharmRepository = pharmRepository;
            _medItemsRepository = medItemsRepository;
        }

        public async Task<CreateIncomeViewModel> Handle(GetIncomesCreateViewModel request, CancellationToken cancellationToken)
        {
            var viewModel = new CreateIncomeViewModel()
            {
                ListIncomes = request.IncomesList,
                MedicalItems = await _medItemsRepository.GetAllAsync(),
                Pharmacies = await GetPharmaciesSelectListItem(),
                PharmacyId = request.PharmacyId
            };

            return viewModel;
        }

        private async Task<IEnumerable<SelectListItem>> GetPharmaciesSelectListItem()
        {
            var pharmacies = await _pharmRepository.GetAllAsync();
            var list = new List<SelectListItem>();
            list.AddRange(pharmacies.Select(pharmacy => new SelectListItem(pharmacy.PharmName, pharmacy.PharmId.ToString())));
            return list;
        }
    }
}
