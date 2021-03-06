﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using PharmacyNetwork.Web.ViewModels;

namespace PharmacyNetwork.Web.Features.MedicalItems
{
    public class GetMedicalItemsList : IRequest<MedicalItemsListViewModel>
    {
        public int PageIndex { get; set; }

        public int? CategId { get; set; }

        public int? FirmId { get; set; }

        public GetMedicalItemsList(int pageIndex, int? categId, int? firmId)
        {
            PageIndex = pageIndex;
            CategId = categId;
            FirmId = firmId;
        }
    }
}
