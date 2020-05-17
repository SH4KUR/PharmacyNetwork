using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PharmacyNetwork.ApplicationCore.Entities;
using PharmacyNetwork.ApplicationCore.Interfaces;
using PharmacyNetwork.Web.Controllers;
using PharmacyNetwork.Web.Features.Incomes;
using PharmacyNetwork.Web.Features.Purchases;
using PharmacyNetwork.Web.ViewModels;
using Xunit;

namespace UnitTests.Web
{
    public class IncomesControllerTests
    {
        private List<Income> GetIncomes()
        {
            return new List<Income>()
            {
                new Income() { IncomeId = 1, PharmId = 1, IncomeDate = DateTime.Now },
                new Income() { IncomeId = 2, PharmId = 2, IncomeDate = DateTime.Now },
                new Income() { IncomeId = 3, PharmId = 3, IncomeDate = DateTime.Now },
                new Income() { IncomeId = 4, PharmId = 4, IncomeDate = DateTime.Now },
            };
        }

        [Fact]
        public async void Index_GetAllItems()
        {
            // Arrange

            var mockRepo = new Mock<IAsyncRepository<Income>>();
            var mockMediator = new Mock<IMediator>();
            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetIncomes());

            var controller = new IncomesController(mockRepo.Object, mockMediator.Object);

            // Act

            var result = await controller.Index();

            // Assert

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Income>>(viewResult.Model);
            Assert.Equal(GetIncomes().Count, model.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async void Detail_GetCurrentItem(int? id)
        {
            // Arrange

            var mockRepo = new Mock<IAsyncRepository<Income>>();
            var mockMediator = new Mock<IMediator>();
            mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetIncomes().Find(i => i.IncomeId == id));

            mockMediator
                .Setup(m => m.Send(It.IsAny<GetIncomeDetail>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new IncomeDetailViewModel() { Income = GetIncomes().Find(i => i.IncomeId == id) })
                .Verifiable();

            var controller = new IncomesController(mockRepo.Object, mockMediator.Object);

            // Act

            var result = await controller.Details(id);

            // Assert

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IncomeDetailViewModel>(viewResult.Model);
            Assert.Equal(id, model.Income.IncomeId);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(7)]
        public async void Detail_ReturnNotFound(int? id)
        {
            // Arrange

            var mockRepo = new Mock<IAsyncRepository<Income>>();
            var mockMediator = new Mock<IMediator>();
            mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetIncomes().Find(i => i.IncomeId == id));

            mockMediator
                .Setup(m => m.Send(It.IsAny<GetIncomeDetail>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new IncomeDetailViewModel() { Income = GetIncomes().Find(i => i.IncomeId == id) })
                .Verifiable();

            var controller = new IncomesController(mockRepo.Object, mockMediator.Object);

            // Act

            var result = await controller.Details(id);

            // Assert

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
