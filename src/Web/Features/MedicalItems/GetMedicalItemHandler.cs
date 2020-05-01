using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PharmacyNetwork.ApplicationCore.Entities;
using PharmacyNetwork.ApplicationCore.Interfaces;
using PharmacyNetwork.Web.ViewModels;

namespace PharmacyNetwork.Web.Features.MedicalItems
{
    public class GetMedicalItemHandler : IRequestHandler<GetMedicalItem, MedicalItemViewModel>
    {
        private readonly IAsyncRepository<MedicalItem> _medicalItemsRepository;
        private readonly IAsyncRepository<Firm> _firmsRepository;
        private readonly IAsyncRepository<ProductCategory> _categoriesRepository;

        public GetMedicalItemHandler(IAsyncRepository<MedicalItem> medicalItemsRepository, IAsyncRepository<Firm> firmsRepository, IAsyncRepository<ProductCategory> categoriesRepository)
        {
            _medicalItemsRepository = medicalItemsRepository;
            _firmsRepository = firmsRepository;
            _categoriesRepository = categoriesRepository;
        }

        public async Task<MedicalItemViewModel> Handle(GetMedicalItem request, CancellationToken cancellationToken)
        {
            var medicalItemViewModel = new MedicalItemViewModel();

            if (request.Id.HasValue)
            {
                medicalItemViewModel.MedicalItem = await _medicalItemsRepository.GetByIdAsync(request.Id);
            }

            medicalItemViewModel.Categories = await GetCategoriesSelectListItem();
            medicalItemViewModel.Firms = await GetFirmsSelectListItem();

            return medicalItemViewModel;
        }

        private async Task<IEnumerable<SelectListItem>> GetFirmsSelectListItem()
        {
            var firms = await _firmsRepository.GetAllAsync();
            var list = firms.Select(firm => new SelectListItem(firm.FirmName, firm.FirmId.ToString())).ToList();
            return list;
        }

        private async Task<IEnumerable<SelectListItem>> GetCategoriesSelectListItem()
        {
            var categories = await _categoriesRepository.GetAllAsync();
            var list = categories.Select(category => new SelectListItem(category.CategName, category.CategId.ToString())).ToList();
            return list;
        }
    }
}
