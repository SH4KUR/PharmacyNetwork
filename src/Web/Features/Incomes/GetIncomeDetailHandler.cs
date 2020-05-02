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

namespace PharmacyNetwork.Web.Features.Incomes
{
    public class GetIncomeDetailHandler : IRequestHandler<GetIncomeDetail, IncomeDetailViewModel>
    {
        private readonly IAsyncRepository<IncomeDetail> _incomeDetailRepository;
        private readonly IAsyncRepository<Income> _incomeRepository;

        public GetIncomeDetailHandler(IAsyncRepository<IncomeDetail> incomeDetailRepository, IAsyncRepository<Income> incomeRepository)
        {
            _incomeDetailRepository = incomeDetailRepository;
            _incomeRepository = incomeRepository;
        }

        public async Task<IncomeDetailViewModel> Handle(GetIncomeDetail request, CancellationToken cancellationToken)
        {
            var incomesDetailSpecification = new IncomesDetailSpecification(request.Id);
            var incomesDetail = await _incomeDetailRepository.ListAsync(incomesDetailSpecification);

            var income = await _incomeRepository.GetByIdAsync(request.Id);

            var incomeDetailViewModel = new IncomeDetailViewModel()
            {
                Income = income,
                IncomeDetails = incomesDetail
            };

            return incomeDetailViewModel;
        }
    }
}
