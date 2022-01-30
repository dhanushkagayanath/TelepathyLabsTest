using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interface
{
    public interface IRoomService
    {
        IList<ComboItem> GetAvailableRooms();
        IList<ComboItem> GetOccupiedRooms();
        IList<ComboItem> GetVacantRooms();
        IList<ComboItem> GetRepairingRooms();
        
        bool MarkRoomCleaned(string roomNumber);
        bool MarkRoomForRepair(string roomNumber);
        bool MarkRepairCompleted(string roomNumber);
        bool ResetAllRoomStatuses();

        bool CheckoutRoom(string roomNumber);
        string AssignRoom();
    }
}
