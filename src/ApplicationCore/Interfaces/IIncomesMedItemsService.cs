using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyNetwork.ApplicationCore.Interfaces
{
    public interface IIncomesMedItemsService
    {
        Task AddItemToIncome(int idMedItem);
    }
}
