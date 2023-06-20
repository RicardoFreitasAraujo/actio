﻿using Actio.Common.Auth;
using Actio.Services.Identity.Domain.Models;
using Actio.Services.Identity.Domain.Repositories;
using Actio.Services.Identity.Domain.Services;
using Actio.Services.Identity.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using Xunit;

namespace Actio.Services.Identity.Tests.Unit.Services
{
    public class UserServiceTest
    {
        [Fact]
        public async Task user_service_should_return_jwt()
        {
            //ARRANGE
            var email = "test@test.com";
            var password = "secret";
            var name = "name";
            var salt = "salt";
            var hash = "hash";
            var token = "token";
            var userRepositoryMock = new Mock<IUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            var jwtHandleMock = new Mock<IJwtHandler>();
            encrypterMock.Setup(x => x.GetSalt(salt)).Returns(salt);
            encrypterMock.Setup(x => x.Gethash(password, salt)).Returns(hash);
            jwtHandleMock.Setup(x => x.Create(It.IsAny<Guid>())).Returns(new JsonWebToken
            {
                Token = token
            });

            var user = new User(email, name);
            user.SetPassword(password, encrypterMock.Object);
            userRepositoryMock.Setup(x => x.GetAsync(email)).ReturnsAsync(user);

            //ACT
            var userService = new UserService(userRepositoryMock.Object, encrypterMock.Object, jwtHandleMock.Object);
            var jwt = await userService.LoginAsync(email, password);

            //ASSERT
            userRepositoryMock.Verify(x => x.GetAsync(email), Times.Once);
            jwtHandleMock.Verify(x => x.Create(It.IsAny<Guid>()), Times.Once);
            jwt.Should().NotBeNull();
            jwt.Token.Should().BeEquivalentTo(token);
        }
    }
}
