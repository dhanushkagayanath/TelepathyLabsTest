using FluentAssertions;
using Hotel;
using System;
using Xunit;

namespace XUnitTestsHotel
{
    public class IntegrationTests
    {
        [Fact]
        public void RestAllRoomStatuses_Test()
        {
            // Arrange
            var subject = new HttpHelper();

            //Act
            bool response = subject.RestAllRoomStatuses();

            //Assert
            response.Should().BeTrue();

        }

        [Fact]
        public void FirstSixRoomAssigments_Test()
        {
            // Arrange
            var subject = new HttpHelper();
            subject.RestAllRoomStatuses();
            string firstAssignExpectedResponse = "1A";
            string secondAssignExpectedResponse = "1B";
            string thirdAssignExpectedResponse = "1C";
            string FourthAssignExpectedResponse = "1D";
            string fifthAssignExpectedResponse = "1E";
            string sixthAssignExpectedResponse = "2E";

            //Act
            string firstAssignActualResponse = subject.AssignRoom();
            string secondAssignActualResponse = subject.AssignRoom();
            string thirdAssignActualResponse = subject.AssignRoom();
            string fourthAssignActualResponse = subject.AssignRoom();
            string fifthAssignActualResponse = subject.AssignRoom();
            string sixthAssignActualResponse = subject.AssignRoom();

            //Assert
            firstAssignActualResponse.Should().Be(firstAssignExpectedResponse);
            secondAssignActualResponse.Should().Be(secondAssignExpectedResponse);
            thirdAssignActualResponse.Should().Be(thirdAssignExpectedResponse);
            fourthAssignActualResponse.Should().Be(FourthAssignExpectedResponse);
            fifthAssignActualResponse.Should().Be(fifthAssignExpectedResponse);
            sixthAssignActualResponse.Should().Be(sixthAssignExpectedResponse);

        }

        [Fact]
        public void Avail_Occu_Vacant_Clean_Avail_Cycle_Test()
        {
            // Arrange
            var subject = new HttpHelper();
            subject.RestAllRoomStatuses();
            string firstAssignExpectedResponse = "1A";

            //Act
            string assignedRoom = subject.AssignRoom();
            bool checkoutActualResponse = subject.CheckoutRoom(assignedRoom);
            bool cleanRoomActualResponse = subject.MarkRoomCleaned(assignedRoom);
            string secondAssignActualResponse = subject.AssignRoom();
;

            //Assert
            assignedRoom.Should().Be(firstAssignExpectedResponse);
            checkoutActualResponse.Should().BeTrue();
            cleanRoomActualResponse.Should().BeTrue();
            secondAssignActualResponse.Should().Be(firstAssignExpectedResponse);

        }

        [Fact]
        public void Avail_Occu_Vacant_Repair_Vacant_Avail_Cycle_Test()
        {
            // Arrange
            var subject = new HttpHelper();
            subject.RestAllRoomStatuses();
            string firstAssignExpectedResponse = "1A";

            //Act
            string assignedRoom = subject.AssignRoom();
            bool checkoutActualResponse = subject.CheckoutRoom(assignedRoom);
            bool markRepairActualResponse = subject.MarkRoomForRepair(assignedRoom);
            bool repairCompletedActualResponse = subject.MarkRepairCompleted(assignedRoom);
            bool cleanRoomActualResponse = subject.MarkRoomCleaned(assignedRoom);
            string secondAssignActualResponse = subject.AssignRoom();

            //Assert
            assignedRoom.Should().Be(firstAssignExpectedResponse);
            checkoutActualResponse.Should().BeTrue();
            markRepairActualResponse.Should().BeTrue();
            repairCompletedActualResponse.Should().BeTrue();
            cleanRoomActualResponse.Should().BeTrue();
            secondAssignActualResponse.Should().Be(firstAssignExpectedResponse);

        }

    }
}
