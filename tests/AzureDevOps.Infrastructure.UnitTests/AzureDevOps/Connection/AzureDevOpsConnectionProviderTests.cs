using AzureDevOps.Application.Interfaces.Infrastructure.Connection;
using AzureDevOps.Infrastructure.AzureDevOps.Connection;
using AzureDevOps.Infrastructure.Exceptions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AzureDevOps.Infrastructure.UnitTests.AzureDevOps.Connection
{
    [TestClass]
    public class AzureDevOpsConnectionProviderTests
    {
        private AzureDevOpsConnectionProvider _sut;
        private Mock<IAzureDevOpsConnection> _connectionMock;

        [TestInitialize]
        public void Setup()
        {
            _sut = new AzureDevOpsConnectionProvider();
            _connectionMock = new Mock<IAzureDevOpsConnection>();
        }

        [TestMethod]
        public void Connection_ShouldThrowConnectionNotEstablishedException_WhenConnectionIsNull()
        {
            // Arrange
            _sut.SaveConnection(null);

            // Act & Assert
            _sut.Invoking(x => x.Connection).Should().Throw<ConnectionNotEstablishedException>();
        }

        [TestMethod]
        public void Connection_ShouldReturnConnection_WhenConnectionIsNotNull()
        {
            // Arrange
            _sut.SaveConnection(_connectionMock.Object);

            // Act
            var result = _sut.Connection;

            // Assert
            result.Should().Be(_connectionMock.Object);
        }
    }
}