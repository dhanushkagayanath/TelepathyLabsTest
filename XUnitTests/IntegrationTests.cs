using Calculator;
using FluentAssertions;
using Models;
using System;
using Xunit;

namespace XUnitTests
{
    [Trait("Calculator", "IntegrationTests")]
    public class IntegrationTests
    {
        [Theory]
        [InlineData("1+1", "2")]
        [InlineData("(100+1)x2", "202")]
        [InlineData("1x0", "0")]
        [InlineData("((15 ÷ (7 - (1 + 1) ) ) x -3 ) - (2 + (1 + 1))", "-13")]
        public void ProcessString(string input, string expectedResult)
        {
            // Arrange
            var subject = new Processor();

            // Act
            CalResult response = subject.ProcessString(input);
            string actualResult = response.Result;

            // Assert
            response.Should().NotBeNull();
            actualResult.Should().Be(expectedResult);
        }
    }
}
