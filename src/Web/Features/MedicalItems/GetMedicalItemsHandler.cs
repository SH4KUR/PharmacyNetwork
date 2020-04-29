using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using PharmacyNetwork.ApplicationCore.Entities;
using PharmacyNetwork.ApplicationCore.Interfaces;
using PharmacyNetwork.ApplicationCore.Specifications;
using PharmacyNetwork.Web.ViewModels;

namespace PharmacyNetwork.Web.Features.MedicalItems
{
    public class GetMedicalItemsHandler : IRequestHandler<GetMedicalItems, MedicalItemsViewModel>
    {
        private readonly IAsyncRepository<MedicalItem> _medicalItemsRepository;
        private readonly IAsyncRepository<Firm> _firmsRepository;
        private readonly IAsyncRepository<ProductCategory> _categoriesRepository;

        public GetMedicalItemsHandler(IAsyncRepository<MedicalItem> medicalItemsRepository, IAsyncRepository<Firm> firmRepository, IAsyncRepository<ProductCategory> categoryRepository)
        {
            _medicalItemsRepository = medicalItemsRepository;
            _firmsRepository = firmRepository;
            _categoriesRepository = categoryRepository;
        }

        public async Task<MedicalItemsViewModel> Handle(GetMedicalItems request, CancellationToken cancellationToken)
        {
            var medicalItemsSpecification = new MedicalItemsSpecification(request.CategId, request.FirmId);
            var medicalItemsPaginatedSpecification = new MedicalItemsPaginatedSpecification(Constants.ITEMS_PER_PAGE * request.PageIndex,
                Constants.ITEMS_PER_PAGE, request.CategId, request.FirmId);    

            var itemsOnPage = await _medicalItemsRepository.ListAsync(medicalItemsPaginatedSpecification);
            var totalItems = _medicalItemsRepository.ListAsync(medicalItemsSpecification).Result.Count;

            var paginationViewModel = new PaginationViewModel()
            {
                ActualPage = request.PageIndex,
                ItemsPerPage = itemsOnPage.Count,
                TotalItems = totalItems,
                TotalPages = int.Parse(Math.Ceiling((decimal)totalItems / Constants.ITEMS_PER_PAGE).ToString())
            };
            paginationViewModel.Next = (paginationViewModel.ActualPage == paginationViewModel.TotalPages - 1) ? "page-item disabled" : "page-item";
            paginationViewModel.Previous = (paginationViewModel.ActualPage == 0) ? "page-item disabled" : "page-item";

            var medicalItemsViewModel = new MedicalItemsViewModel()
            {
                MedicalItems = itemsOnPage,
                CategoryFilterApplied = request.CategId,
                FirmFilterApplied = request.FirmId,
                Firms = await GetFirmsSelectListItem(),
                Categories = await GetCategoriesSelectListItem(),
                Pagination = paginationViewModel
            };

            return medicalItemsViewModel;
        }

        private async Task<IEnumerable<SelectListItem>> GetFirmsSelectListItem()
        {
            var firms = await _firmsRepository.GetAllAsync();

            var list = new List<SelectListItem>()
            {
                new SelectListItem("Все фирмы", null, true)
            };

            list.AddRange(firms.Select(firm => new SelectListItem(firm.FirmName, firm.FirmId.ToString())));

            return list;
        }

        private async Task<IEnumerable<SelectListItem>> GetCategoriesSelectListItem()
        {
            var categories = await _categoriesRepository.GetAllAsync();

            var list = new List<SelectListItem>()
            {
                new SelectListItem("Все категории", null, true)
            };

            list.AddRange(categories.Select(category => new SelectListItem(category.CategName, category.CategId.ToString())));

            return list;
        }
    }
}
