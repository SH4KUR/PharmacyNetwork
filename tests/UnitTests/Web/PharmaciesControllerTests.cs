using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PharmacyNetwork.ApplicationCore.Entities;
using PharmacyNetwork.ApplicationCore.Interfaces;
using PharmacyNetwork.Web.Controllers;
using PharmacyNetwork.Web.ViewModels;
using Xunit;

namespace UnitTests.Web
{
    public class PharmaciesControllerTests
    {
        private List<Pharmacy> GetPharmacies()
        {
            return new List<Pharmacy>()
            {
                new Pharmacy() { PharmId = 1, PharmName = "Pharm1", PharmAddress = "Address1" },
                new Pharmacy() { PharmId = 2, PharmName = "Pharm2", PharmAddress = "Address2" },
                new Pharmacy() { PharmId = 3, PharmName = "Pharm3", PharmAddress = "Address3" }
            };
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public async void Detail_GetCurrentItem(int? id)
        {
            // Arrange

            var mockRepo = new Mock<IAsyncRepository<Pharmacy>>();
            var mockMediator = new Mock<IMediator>();
            mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetPharmacies().Find(i => i.PharmId == id));

            var controller = new PharmaciesController(mockRepo.Object, mockMediator.Object);

            // Act

            var result = await controller.Details(id);

            // Assert

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<PharmacyViewModel>(viewResult.Model);
            Assert.Equal(id, model.Pharmacy.PharmId);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(7)]
        public async void Detail_ReturnNotFound(int? id)
        {
            // Arrange

            var mockRepo = new Mock<IAsyncRepository<Pharmacy>>();
            var mockMediator = new Mock<IMediator>();
            mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetPharmacies().Find(i => i.PharmId == id));

            var controller = new PharmaciesController(mockRepo.Object, mockMediator.Object);

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

            var mockRepo = new Mock<IAsyncRepository<Pharmacy>>();
            var mockMediator = new Mock<IMediator>();
            mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetPharmacies().Find(i => i.PharmId == id));

            var controller = new PharmaciesController(mockRepo.Object, mockMediator.Object);

            // Act

            var result = await controller.Edit(id);

            // Assert

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Pharmacy>(viewResult.Model);
            Assert.Equal(id, model.PharmId);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(7)]
        public async void Edit_ReturnNotFound(int? id)
        {
            // Arrange

            var mockRepo = new Mock<IAsyncRepository<Pharmacy>>();
            var mockMediator = new Mock<IMediator>();
            mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetPharmacies().Find(i => i.PharmId == id));

            var controller = new PharmaciesController(mockRepo.Object, mockMediator.Object);

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

            var mockRepo = new Mock<IAsyncRepository<Pharmacy>>();
            var mockMediator = new Mock<IMediator>();
            mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetPharmacies().Find(i => i.PharmId == id));

            var controller = new PharmaciesController(mockRepo.Object, mockMediator.Object);

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

            var mockRepo = new Mock<IAsyncRepository<Pharmacy>>();
            var mockMediator = new Mock<IMediator>();
            mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetPharmacies().Find(i => i.PharmId == id));

            var controller = new PharmaciesController(mockRepo.Object, mockMediator.Object);

            // Act

            var result = await controller.Delete(id);

            // Assert

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Pharmacy>(viewResult.Model);
            Assert.Equal(id, model.PharmId);
        }
    }
}
