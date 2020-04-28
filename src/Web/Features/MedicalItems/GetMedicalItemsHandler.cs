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

namespace PharmacyNetwork.Web.Features.MedicalItems
{
    public class GetMedicalItemsHandler : IRequestHandler<GetMedicalItems, MedicalItemsViewModel>
    {
        private IAsyncRepository<MedicalItem> _repository;

        public GetMedicalItemsHandler(IAsyncRepository<MedicalItem> repository)
        {
            _repository = repository;
        }

        public async Task<MedicalItemsViewModel> Handle(GetMedicalItems request, CancellationToken cancellationToken)
        {
            var medicalItemsPaginatedSpecification = new MedicalItemsPaginatedSpecification(Constants.ITEMS_PER_PAGE * request.PageIndex,
                Constants.ITEMS_PER_PAGE, null, null);      // TODO: Add filter categ, firm

            var itemsOnPage = await _repository.ListAsync(medicalItemsPaginatedSpecification);
            var totalItems = _repository.GetAllAsync().Result.Count;

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
                Pagination = paginationViewModel
            };

            return medicalItemsViewModel;
        }
    }
}
