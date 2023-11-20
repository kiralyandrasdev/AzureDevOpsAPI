using System;
using AzureDevOps.Application.Factories;
using FluentAssertions;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AzureDevOps.Application.UnitTests.Factories
{
    [TestClass]
    public class PullRequestSearchCriteriaFactoryTests
    {
        [TestMethod]
        [DataRow("", PullRequestStatus.All)]
        [DataRow("all", PullRequestStatus.All)]
        [DataRow("active", PullRequestStatus.Active)]
        [DataRow("completed", PullRequestStatus.Completed)]
        [DataRow("abandoned", PullRequestStatus.Abandoned)]
        [DataRow("notset", PullRequestStatus.NotSet)]
        public void Create_ShouldReturnPullRequestSearchCriteria_WhenPullRequestStatusIsValid(string pullRequestStatus, PullRequestStatus expectedStatus)
        {
            // Arrange

            // Act
            var result = PullRequestSearchCriteriaFactory.Create(pullRequestStatus);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(expectedStatus);
        }

        [TestMethod]
        [DataRow("invalid")]
        public void Create_ShouldThrowArgumentException_WhenPullRequestStatusIsInvalid(string pullRequestStatus)
        {
            // Arrange

            // Act & Assert
            Action act = () => PullRequestSearchCriteriaFactory.Create(pullRequestStatus);

            act.Should().Throw<ArgumentException>();
        }
    }
}