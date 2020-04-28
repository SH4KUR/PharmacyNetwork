using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using PharmacyNetwork.Web.ViewModels;

namespace PharmacyNetwork.Web.Features.MedicalItems
{
    public class GetMedicalItems : IRequest<MedicalItemsViewModel>
    {
        public int PageIndex { get; set; }
     
        public GetMedicalItems(int pageIndex)
        {
            PageIndex = pageIndex;
        }
    }
}
