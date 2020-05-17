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
    public class ProductCategoriesControllerTests
    {
        private List<ProductCategory> GetCategories()
        {
            return new List<ProductCategory>()
            {
                new ProductCategory() { CategId = 1, CategName = "Category1", CategMarkup = 1 },
                new ProductCategory() { CategId = 2, CategName = "Category2", CategMarkup = 2 },
                new ProductCategory() { CategId = 3, CategName = "Category3", CategMarkup = 3 }
            };
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void Edit_GetCurrentItem(int? id)
        {
            // Arrange

            var mockRepo = new Mock<IAsyncRepository<ProductCategory>>();
            mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetCategories().Find(i => i.CategId == id));

            var controller = new ProductCategoriesController(mockRepo.Object);

            // Act

            var result = await controller.Edit(id);

            // Assert

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProductCategory>(viewResult.Model);
            Assert.Equal(id, model.CategId);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(7)]
        public async void Edit_ReturnNotFound(int? id)
        {
            // Arrange

            var mockRepo = new Mock<IAsyncRepository<ProductCategory>>();
            mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetCategories().Find(i => i.CategId == id));

            var controller = new ProductCategoriesController(mockRepo.Object);

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

            var mockRepo = new Mock<IAsyncRepository<ProductCategory>>();
            mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetCategories().Find(i => i.CategId == id));

            var controller = new ProductCategoriesController(mockRepo.Object);

            // Act

            var result = await controller.Delete(id);

            // Assert

            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void Delete_DeleteItems(int? id)
        {
            // Arrange

            var mockRepo = new Mock<IAsyncRepository<ProductCategory>>();
            mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetCategories().Find(i => i.CategId == id));

            var controller = new ProductCategoriesController(mockRepo.Object);

            // Act

            var result = await controller.Delete(id);

            // Assert

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProductCategory>(viewResult.Model);
            Assert.Equal(id, model.CategId);
        }
    }
}
