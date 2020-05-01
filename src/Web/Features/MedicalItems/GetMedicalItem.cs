using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using PharmacyNetwork.Web.ViewModels;

namespace PharmacyNetwork.Web.Features.MedicalItems
{
    public class GetMedicalItem : IRequest<MedicalItemViewModel>
    {
        public int? Id { get; set; }

        public GetMedicalItem(int? id)
        {
            Id = id;
        }

        public GetMedicalItem()
        {
            Id = null;
        }
    }
}
