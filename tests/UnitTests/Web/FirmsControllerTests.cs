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
using PharmacyNetwork.Web.ViewModels;
using Xunit;

namespace UnitTests.Web
{
    public class FirmsControllerTests
    {
        private List<Firm> GetFirms()
        {
            return new List<Firm>()
            {
                new Firm() { FirmId = 1, FirmName = "Firm1", FirmAddress = "Address", FirmContact = "Contact", FirmMarkup = 1 },
                new Firm() { FirmId = 2, FirmName = "Firm2", FirmAddress = "Address", FirmContact = "Contact", FirmMarkup = 2 },
                new Firm() { FirmId = 3, FirmName = "Firm3", FirmAddress = "Address", FirmContact = "Contact", FirmMarkup = 3 }
            };
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public async void Detail_GetCurrentItem(int? id)
        {
            // Arrange

            var mockRepo = new Mock<IAsyncRepository<Firm>>();
            mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetFirms().Find(i => i.FirmId == id));

            var controller = new FirmsController(mockRepo.Object);

            // Act

            var result = await controller.Details(id);

            // Assert

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Firm>(viewResult.Model);
            Assert.Equal(id, model.FirmId);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(7)]
        public async void Detail_ReturnNotFound(int? id)
        {
            // Arrange

            var mockRepo = new Mock<IAsyncRepository<Firm>>();
            mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetFirms().Find(i => i.FirmId == id));

            var controller = new FirmsController(mockRepo.Object);

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

            var mockRepo = new Mock<IAsyncRepository<Firm>>();
            mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetFirms().Find(i => i.FirmId == id));

            var controller = new FirmsController(mockRepo.Object);

            // Act

            var result = await controller.Edit(id);

            // Assert

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Firm>(viewResult.Model);
            Assert.Equal(id, model.FirmId);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(7)]
        public async void Edit_ReturnNotFound(int? id)
        {
            // Arrange

            var mockRepo = new Mock<IAsyncRepository<Firm>>();
            mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetFirms().Find(i => i.FirmId == id));

            var controller = new FirmsController(mockRepo.Object);

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

            var mockRepo = new Mock<IAsyncRepository<Firm>>();
            mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetFirms().Find(i => i.FirmId == id));

            var controller = new FirmsController(mockRepo.Object);

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

            var mockRepo = new Mock<IAsyncRepository<Firm>>();
            mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetFirms().Find(i => i.FirmId == id));

            var controller = new FirmsController(mockRepo.Object);

            // Act

            var result = await controller.Delete(id);

            // Assert

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Firm>(viewResult.Model);
            Assert.Equal(id, model.FirmId);
        }
    }
}
