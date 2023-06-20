using Actio.Api.Controllers;
using Actio.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;
using System.Security.Claims;
using Microsoft.Extensions.Configuration.UserSecrets;
using Actio.Common.Commands;
using System.Threading.Tasks;
using FluentAssertions;

namespace Actio.Api.Tests.Unit.Controller
{
    public class ActivityControllerTest
    {

        [Fact]
        public async Task activities_controller_post_should_return_accepted()
        {
            //Arrange
            var busClientMock = new Mock<IBusClient>();
            var activityRespositoryMock = new Mock<IActivityRepository>();
            var controller = new ActivitiesController(busClientMock.Object, activityRespositoryMock.Object);

            var userId = Guid.NewGuid();
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    /*User = new ClaimsPrincipal(new List<>ClaimsIdentity(
                        new Claim[] { new Claim(ClaimTypes.Name, userId.ToString()) }
                    ), "test")*/
                }
            };

            var command = new CreateActivity
            {
                Id = Guid.NewGuid(),
                UserId = userId
            };

            //Act
            var result = await controller.Post(command);

            //Assert
            var contentResult = result as AcceptedResult;
            contentResult.Should().NotBeNull();
            contentResult.Location.Should().BeSameAs($"activities/{command.Id}");
        }
    }
}
