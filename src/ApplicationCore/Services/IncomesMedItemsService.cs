using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PharmacyNetwork.ApplicationCore.Entities;
using PharmacyNetwork.ApplicationCore.Interfaces;

namespace PharmacyNetwork.ApplicationCore.Services
{
    public class IncomesMedItemsService : IIncomesMedItemsService
    {
        private static List<MedicalItem> MedicalItems = new List<MedicalItem>();
        private readonly IAsyncRepository<MedicalItem> _repository;

        public IncomesMedItemsService(IAsyncRepository<MedicalItem> repository)
        {
            _repository = repository;
        }

        public async Task AddItemToIncome(int idMedItem)
        {
            var medicalItem = await _repository.GetByIdAsync(idMedItem);   

            MedicalItems.Add(medicalItem);
        }
    }
}
