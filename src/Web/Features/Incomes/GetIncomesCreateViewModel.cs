using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using PharmacyNetwork.Web.ViewModels;

namespace PharmacyNetwork.Web.Features.Incomes
{
    public class GetIncomesCreateViewModel : IRequest<CreateIncomeViewModel>
    {
        public GetIncomesCreateViewModel(List<IncomeItem> incomesList, int? pharmacyId)
        {
            IncomesList = incomesList;
            PharmacyId = pharmacyId;
        }

        public List<IncomeItem> IncomesList { get; set; }

        public int? PharmacyId { get; set; }
    }
}
