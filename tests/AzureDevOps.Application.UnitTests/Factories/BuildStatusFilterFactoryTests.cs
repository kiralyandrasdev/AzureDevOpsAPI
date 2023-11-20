using System;
using AzureDevOps.Application.Factories;
using FluentAssertions;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AzureDevOps.Application.UnitTests.Factories
{
    [TestClass]
    public class BuildStatusFilterFactoryTests
    {
        [TestMethod]
        [DataRow("", BuildStatus.All)]
        [DataRow("all", BuildStatus.All)]
        [DataRow("completed", BuildStatus.Completed)]
        [DataRow("inprogress", BuildStatus.InProgress)]
        [DataRow("cancelling", BuildStatus.Cancelling)]
        [DataRow("notstarted", BuildStatus.NotStarted)]
        [DataRow("postponed", BuildStatus.Postponed)]
        [DataRow("none", BuildStatus.None)]
        public void Create_ShouldReturnBuildStatusFilter_WhenBuildStatusIsValid(string buildStatus, BuildStatus expectedStatus)
        {
            // Arrange

            // Act
            var result = BuildStatusFilterFactory.Create(buildStatus);

            // Assert
            result.Should().NotBeNull();
            result.BuildStatus.Should().Be(expectedStatus);
        }

        [TestMethod]
        [DataRow("invalid")]
        public void Create_ShouldThrowArgumentException_WhenBuildStatusIsInvalid(string buildStatus)
        {
            // Arrange

            // Act
            Action act = () => BuildStatusFilterFactory.Create(buildStatus);

            // Assert
            act.Should().Throw<ArgumentException>();
        }
    }
}