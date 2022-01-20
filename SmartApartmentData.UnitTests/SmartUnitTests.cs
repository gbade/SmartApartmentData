using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using SmartApartmentData.Business.Contracts;
using SmartApartmentData.Business.Managers;
using SmartApartmentData.Controllers;
using SmartApartmentData.Entities.Models;
using Xunit;

namespace SmartApartmentData.UnitTests
{
    public class SmartUnitTests
    {
        private readonly string _gateway;
        private readonly string _authkey;
        private readonly Mock<IPropertiesManager> _propMock = new Mock<IPropertiesManager>();
        private readonly Mock<SmartConfigManager> _smMock = new Mock<SmartConfigManager>();

        public SmartUnitTests()
        {
            string dir = Path.GetFullPath(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"../../..")
            );

            var builder = new ConfigurationBuilder()
                .SetBasePath(dir).AddJsonFile("appsettings.json").Build();

            Configuration = builder;

            _gateway = Configuration.GetSection("AwsGateway")
                            .GetSection("APIGateway").Value;
            _authkey = Configuration.GetSection("AwsGateway")
                            .GetSection("AuthKey").Value;

            _smMock.Object.APIGateway = _gateway;
            _smMock.Object.AuthKey = _authkey;
        }

        public IConfigurationRoot Configuration { get; }

        [Fact]
        public void ShouldGetProperties_Market_PropertyIsNull_Return_BadRequest()
        {
            //Arrange
            var _propertiesController = new PropertiesController(_propMock.Object);
            Properties model = new Properties();

            //Act
            var actionResult = _propertiesController.GetProperties(model);
            var objectResult = actionResult as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public void ShouldGetProperties_Market_PropertyRequired_Return_OkRequest()
        {
            //Arrange
            var _propertiesController = new PropertiesController(_propMock.Object);
            Properties model = new Properties
            {
                market = "austin",
                name = "ranch"
            };

            //Act
            var actionResult = _propertiesController.GetProperties(model);
            var objectResult = actionResult as OkObjectResult;

            //Assert
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public void ShouldGetProperties_PropertyCount_Should_Have_Limit_of_25()
        {
            //Arrange
            var _propManager = new PropertiesManager(_smMock.Object);
            Properties model = new Properties
            {
                market = "austin",
                name = "ranch"
            };

            //Act
            var result = _propManager.GetDistinctProperties(model);
            var propertyCount = result.Count();

            //Assert
            Assert.InRange(propertyCount, 1, 25);
        }
    }
}
