using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using PharmacyNetwork.ApplicationCore.Entities;
using PharmacyNetwork.Web.ViewModels;

namespace PharmacyNetwork.Web.Features.Incomes
{
    public class GetIncomeDetail : IRequest<IncomeDetailViewModel>
    {
        public int? Id { get; set; }

        public GetIncomeDetail(int? id)
        {
            Id = id;
        }
    }
}
