using Actio.Api.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Actio.Api.Tests.Unit.Controller
{
    public class HomeControllerTests
    {
        [Fact]
        public void home_controller_get_shold_return_string_content()
        {
            var controller = new HomeController();
            var result = controller.Get();
            var contentResult = result as ContentResult;
            contentResult.Should().NotBeNull();
            contentResult.Content.Should().BeSameAs("Hello from Action API!");
        }
    }
}
