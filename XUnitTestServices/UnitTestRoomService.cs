using FluentAssertions;
using Models;
using NSubstitute;
using Services;
using Services.Interface;
using System;
using System.Collections.Generic;
using Xunit;

namespace XUnitTestServices
{
    public class UnitTestRoomService
    {
        readonly IRoomService subject;
        readonly IDataService dataService;

        public UnitTestRoomService()
        {
            //mock test data
            dataService = Substitute.For<IDataService>();
            IList<Room> Rooms = new List<Room>();
            for (int i = 1; i < 5; i++)
            {
                Rooms.Add(new Room(i, "A"));
                Rooms.Add(new Room(i, "B"));
                Rooms.Add(new Room(i, "C"));
                Rooms.Add(new Room(i, "D"));
                Rooms.Add(new Room(i, "E"));
            }
            dataService.Rooms = Rooms;
            subject = new RoomService(dataService);
        }

        [Fact]
        public void AssignRoom_test()
        {
            //Arrange

            //Act
            string actualResult = subject.AssignRoom();
            //Assert
            actualResult.Should().Be("1A");
        }

        [Fact]
        public void FirstSixRoomAssigments_Test()
        {
            // Arrange
            subject.ResetAllRoomStatuses();
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
            subject.ResetAllRoomStatuses();
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
    }
}
