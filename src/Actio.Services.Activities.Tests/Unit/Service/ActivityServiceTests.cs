using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.Repositories;
using Actio.Services.Activities.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Actio.Services.Activities.Tests.Unit.Service
{
    public class ActivityServiceTests
    {
        [Fact]
        public async Task activity_service_add_async_should_succeed()
        {
            //ARRANGE
            var category = "test";

            //Preparando Camada de Repositorio
            var activityRepositoryMock = new Mock<IActivityRepository>();
            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            categoryRepositoryMock.Setup(x => x.GetAsync(category)).ReturnsAsync(new Category(category));

            //Preparando Camada de Serviço
            var activityService = new ActivityService(activityRepositoryMock.Object, categoryRepositoryMock.Object);

            //ACT
            var id = Guid.NewGuid();
            await activityService.AddAsync(id, Guid.NewGuid(), category, "activity", "description", DateTime.UtcNow);

            //ASSERT
            //Verifica se o método do repositório foi chamado pelo menos 1 vez
            categoryRepositoryMock.Verify(x => x.GetAsync(category), Times.Once);
            //Verifica se o método do repositório foi chamado pelo menos 1 vez
            activityRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Activity>()), Times.Once);
        }
    }
}
