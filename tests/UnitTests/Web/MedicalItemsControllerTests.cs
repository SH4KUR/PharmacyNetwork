using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PharmacyNetwork.ApplicationCore.Entities;
using PharmacyNetwork.ApplicationCore.Interfaces;
using PharmacyNetwork.Web.Controllers;
using PharmacyNetwork.Web.Features.MedicalItems;
using PharmacyNetwork.Web.ViewModels;
using Xunit;

namespace UnitTests.Web
{
    public class MedicalItemsControllerTests
    {
        private List<MedicalItem> GetMedItems()
        {
            return new List<MedicalItem>()
            {
                new MedicalItem() {MedItemName = "Item1", MedItemId = 1, CategId = 1, FirmId = 1},
                new MedicalItem() {MedItemName = "Item2", MedItemId = 2, CategId = 2, FirmId = 2},
                new MedicalItem() {MedItemName = "Item3", MedItemId = 3, CategId = 3, FirmId = 3},
                new MedicalItem() {MedItemName = "Item4", MedItemId = 4, CategId = 4, FirmId = 1},
                new MedicalItem() {MedItemName = "Item5", MedItemId = 5, CategId = 5, FirmId = 2}
            };
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public async void Detail_GetCurrentItem(int? id)
        {
            // Arrange

            var mockRepo = new Mock<IAsyncRepository<MedicalItem>>();
            var mockMediator = new Mock<IMediator>();
            mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetMedItems().Find(i => i.MedItemId == id));

            var controller = new MedicalItemsController(mockRepo.Object, mockMediator.Object);

            // Act

            var result = await controller.Details(id);

            // Assert

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<MedicalItemsDetailViewModel>(viewResult.Model);
            Assert.Equal(id, model.MedicalItem.MedItemId);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(7)]
        public async void Detail_ReturnNotFound(int? id)
        {
            // Arrange

            var mockRepo = new Mock<IAsyncRepository<MedicalItem>>();
            var mockMediator = new Mock<IMediator>();
            mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetMedItems().Find(i => i.MedItemId == id));

            var controller = new MedicalItemsController(mockRepo.Object, mockMediator.Object);

            // Act

            var result = await controller.Details(id);

            // Assert

            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public async void Edit_GetCurrentItem(int? id)
        {
            // Arrange

            var mockRepo = new Mock<IAsyncRepository<MedicalItem>>();
            var mockMediator = new Mock<IMediator>();
            mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetMedItems().Find(i => i.MedItemId == id));

            mockMediator
                .Setup(m => m.Send(It.IsAny<GetMedicalItem>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new MedicalItemViewModel() { MedicalItem = GetMedItems().Find(i => i.MedItemId == id) })
                .Verifiable();

            var controller = new MedicalItemsController(mockRepo.Object, mockMediator.Object);

            // Act

            var result = await controller.Edit(id);

            // Assert

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<MedicalItemViewModel>(viewResult.Model);
            Assert.Equal(id, model.MedicalItem.MedItemId);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(7)]
        public async void Edit_ReturnNotFound(int? id)
        {
            // Arrange

            var mockRepo = new Mock<IAsyncRepository<MedicalItem>>();
            var mockMediator = new Mock<IMediator>();
            mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetMedItems().Find(i => i.MedItemId == id));

            var controller = new MedicalItemsController(mockRepo.Object, mockMediator.Object);

            // Act

            var result = await controller.Edit(id);

            // Assert

            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(7)]
        public async void Delete_ReturnNotFound(int? id)
        {
            // Arrange

            var mockRepo = new Mock<IAsyncRepository<MedicalItem>>();
            var mockMediator = new Mock<IMediator>();
            mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetMedItems().Find(i => i.MedItemId == id));

            var controller = new MedicalItemsController(mockRepo.Object, mockMediator.Object);

            // Act

            var result = await controller.Delete(id);

            // Assert

            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public async void Delete_DeleteItems(int? id)
        {
            // Arrange

            var mockRepo = new Mock<IAsyncRepository<MedicalItem>>();
            var mockMediator = new Mock<IMediator>();
            mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetMedItems().Find(i => i.MedItemId == id));

            var controller = new MedicalItemsController(mockRepo.Object, mockMediator.Object);

            // Act

            var result = await controller.Delete(id);

            // Assert

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<MedicalItem>(viewResult.Model);
            Assert.Equal(id, model.MedItemId);
        }
    }
}
