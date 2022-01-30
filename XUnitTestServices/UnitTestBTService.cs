using FluentAssertions;
using Models;
using Services;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestServices
{
    
    public class UnitTestBTService
    {
        readonly IBTService subject;
        public UnitTestBTService()
        {
            subject = new BTService();
        }

        [Theory]
        [InlineData("1+1", "2")]
        [InlineData("(100+1)x2", "202")]
        [InlineData("1x0", "0")]
        [InlineData("((15 ÷ (7 - (1 + 1) ) ) x -3 ) - (2 + (1 + 1))", "-13")]
        public void ProcessString(string input, string expectedResult)
        {

            // Act
            CalResult response = subject.ProcessString(input);
            string actualResult = response.Result;

            // Assert
            response.Should().NotBeNull();
            actualResult.Should().Be(expectedResult);
        }
    }
}
