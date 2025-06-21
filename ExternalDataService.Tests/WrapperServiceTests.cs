using ExternalDataService.Configuration;
using ExternalDataService.Interfaces;
using ExternalDataService.Models;
using ExternalDataService.Models.Dto;
using ExternalDataService.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExternalDataService.Tests
{
    [TestClass]
    public sealed class WrapperServiceTests
    {
        [TestMethod]
        public async Task GetAllUsersAsync_ReturnsConvertedUsers()
        {
            var mockClient = new Mock<IExternalDataClient>();
            var userDtos = new List<UserDto>
                    {
                        new UserDto { Id = 1, FirstName = "Test", LastName="User" }
                    };
            mockClient.Setup(c => c.GetAllUsersAsync()).ReturnsAsync(userDtos);

            var mockCache = new Mock<IMemoryCache>();
            var mockCacheSettings = new Mock<IOptions<CacheSettings>>();
            var mockLogger = new Mock<ILogger<ThirdPartyUserService>>();
            var service = new ThirdPartyUserService(mockClient.Object,
                mockCache.Object,
                mockCacheSettings.Object,
                mockLogger.Object);

            var result = (await service.GetAllUsersAsync()).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(userDtos[0].Id, result[0].Id);
            Assert.AreEqual(userDtos[0].FirstName, result[0].FirstName);
            Assert.AreEqual(userDtos[0].LastName, result[0].LastName);
        }

        [TestMethod]
        public async Task GetUserByIdAsync_ReturnsConvertedUser_WhenUserExists()
        {
            var mockClient = new Mock<IExternalDataClient>();
            var userDto = new UserDto { Id = 2, FirstName = "Jane", LastName = "Doe" };
            mockClient.Setup(c => c.GetUserByIdAsync(2)).ReturnsAsync(userDto);

            var mockCache = new Mock<IMemoryCache>();
            var mockCacheSettings = new Mock<IOptions<CacheSettings>>();
            var mockLogger = new Mock<ILogger<ThirdPartyUserService>>();
            var service = new ThirdPartyUserService(mockClient.Object,
                 mockCache.Object,
                 mockCacheSettings.Object,
                 mockLogger.Object);

            var result = await service.GetUserByIdAsync(2);

            Assert.IsNotNull(result);
            Assert.AreEqual(userDto.Id, result.Id);
            Assert.AreEqual(userDto.FirstName, result.FirstName);
            Assert.AreEqual(userDto.LastName, result.LastName);
        }

        [TestMethod]
        public async Task GetUserByIdAsync_ThrowsException_WhenUserNotFound()
        {
            var mockClient = new Mock<IExternalDataClient>();
            mockClient.Setup(c => c.GetUserByIdAsync(99)).ReturnsAsync((UserDto)null);

            var mockCache = new Mock<IMemoryCache>();
            var mockCacheSettings = new Mock<IOptions<CacheSettings>>();
            var mockLogger = new Mock<ILogger<ThirdPartyUserService>>();
            var service = new ThirdPartyUserService(mockClient.Object,
                mockCache.Object,
                mockCacheSettings.Object,
                mockLogger.Object);

            var ex = await Assert.ThrowsExceptionAsync<Exception>(() => service.GetUserByIdAsync(99));
            Assert.IsTrue(ex.Message.Contains("User with ID 99 not found."));
        }
    }
}
