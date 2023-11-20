using System;
using System.Threading.Tasks;
using AzureDevOps.Application.Interfaces.Infrastructure.Connection;
using AzureDevOps.Domain.Misc;
using AzureDevOps.Infrastructure.AzureDevOps.Connection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AzureDevOps.Infrastructure.UnitTests.AzureDevOps.Connection
{
    [TestClass]
    public class AzureDevOpsConnectionServiceTests
    {
        private AzureDevOpsConnectionService _sut;
        private Mock<IAzureDevOpsConnectionProvider> _connectionProviderMock;

        [TestInitialize]
        public void SetUp()
        {
            _connectionProviderMock = new Mock<IAzureDevOpsConnectionProvider>();
            _sut = new AzureDevOpsConnectionService(_connectionProviderMock.Object);
        }

        [TestMethod]
        public async Task ConnectToAzureDevOpsAsync_ShouldThrowArgumentNullException_WhenUrlIsNull()
        {
            // Arrange
            string url = null;
            const string accessToken = "access_token";

            // Act 
            Func<Task> act = () => _sut.ConnectToAzureDevOpsAsync(url, accessToken);

            //Assert 
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [TestMethod]
        public async Task ConnectToAzureDevOpsAsync_ShouldThrowArgumentNullException_WhenAccessTokenIsNull()
        {
            // Arrange
            const string url = "https://dev.azure.com/";
            string accessToken = null;

            // Act 
            Func<Task> act = () => _sut.ConnectToAzureDevOpsAsync(url, accessToken);

            //Assert 
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [TestMethod]
        public void ValidateAzureDevOpsConnection_ShouldReturnSuccessTrueAndConnectionSuccessfullyEstablishedMessage_WhenConnectionHasAuthenticated()
        {
            // Arrange
            var connectionMock = new Mock<IAzureDevOpsConnection>();
            connectionMock.SetupGet(c => c.HasAuthenticated).Returns(true);
            var expectedValidationResult = new AzureDevOpsConnectionValidationResult
            {
                Success = true,
                Message = "Connection to Azure DevOps successfully established."
            };

            // Act
            var actualValidationResult = _sut.ValidateAzureDevOpsConnection(connectionMock.Object);

            // Assert
            actualValidationResult.Should().BeEquivalentTo(expectedValidationResult);
        }

        [TestMethod]
        public void ValidateAzureDevOpsConnection_ShouldReturnSuccessFalseAndConnectionFailedMessage_WhenConnectionHasNotAuthenticated()
        {
            // Arrange
            var connectionMock = new Mock<IAzureDevOpsConnection>();
            connectionMock.SetupGet(c => c.HasAuthenticated).Returns(false);
            var expectedValidationResult = new AzureDevOpsConnectionValidationResult
            {
                Success = false,
                Message = "Connection to Azure DevOps failed."
            };

            // Act
            var actualValidationResult = _sut.ValidateAzureDevOpsConnection(connectionMock.Object);

            // Assert
            actualValidationResult.Should().BeEquivalentTo(expectedValidationResult);
        }
        [TestMethod]
        public void SaveAzureDevOpsConnection_ShouldCallSaveConnectionMethodOfConnectionProvider_WhenConnectionIsProvided()
        {
            // Arrange
            var connectionMock = new Mock<IAzureDevOpsConnection>();

            // Act
            _sut.SaveAzureDevOpsConnection(connectionMock.Object);

            // Assert
            _connectionProviderMock.Verify(c => c.SaveConnection(connectionMock.Object), Times.Once);
        }
    }
}