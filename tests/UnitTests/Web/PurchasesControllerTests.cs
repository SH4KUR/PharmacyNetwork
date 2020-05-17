using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PharmacyNetwork.ApplicationCore.Entities;
using PharmacyNetwork.ApplicationCore.Interfaces;
using PharmacyNetwork.Web.Controllers;
using PharmacyNetwork.Web.Features.MedicalItems;
using PharmacyNetwork.Web.Features.Purchases;
using PharmacyNetwork.Web.ViewModels;
using Xunit;

namespace UnitTests.Web
{
    public class PurchasesControllerTests
    {
        private List<Purchase> GetPurchases()
        {
            return new List<Purchase>()
            {
                new Purchase() { PurchId = 1, PharmId = 1, PurchDate = DateTime.Now, PurchAmount = 100, PurchDiscountPercent = 1 },
                new Purchase() { PurchId = 2, PharmId = 2, PurchDate = DateTime.Now, PurchAmount = 200, PurchDiscountPercent = 2 },
                new Purchase() { PurchId = 3, PharmId = 3, PurchDate = DateTime.Now, PurchAmount = 300, PurchDiscountPercent = 3 },
                new Purchase() { PurchId = 4, PharmId = 4, PurchDate = DateTime.Now, PurchAmount = 400, PurchDiscountPercent = 4 }
            };
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async void Detail_GetCurrentItem(int? id)
        {
            // Arrange

            var mockRepo = new Mock<IAsyncRepository<Purchase>>();
            var mockMediator = new Mock<IMediator>();
            mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetPurchases().Find(i => i.PurchId == id));

            mockMediator
                .Setup(m => m.Send(It.IsAny<GetPurchaseCheck>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PurchaseCheckViewModel() { Purchase = GetPurchases().Find(i => i.PurchId == id) })
                .Verifiable();

            var controller = new PurchasesController(mockRepo.Object, mockMediator.Object);

            // Act

            var result = await controller.Details(id);

            // Assert

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<PurchaseCheckViewModel>(viewResult.Model);
            Assert.Equal(id, model.Purchase.PurchId);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(7)]
        public async void Detail_ReturnNotFound(int? id)
        {
            // Arrange

            var mockRepo = new Mock<IAsyncRepository<Purchase>>();
            var mockMediator = new Mock<IMediator>();
            mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetPurchases().Find(i => i.PurchId == id));

            mockMediator
                .Setup(m => m.Send(It.IsAny<GetPurchaseCheck>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PurchaseCheckViewModel() { Purchase = GetPurchases().Find(i => i.PurchId == id) })
                .Verifiable();

            var controller = new PurchasesController(mockRepo.Object, mockMediator.Object);

            // Act

            var result = await controller.Details(id);

            // Assert

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
