using Models;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class RoomService : IRoomService
    {
        IDataService _dataService;
        public RoomService(IDataService dataService)
        {
            _dataService = dataService;
        }
        public string AssignRoom()
        {
            Room room = _dataService.Rooms.Where(x=>x.RoomStatus == RoomStatus.Available).OrderBy(x=>x).FirstOrDefault();
            if (room != null) {
                room.RoomStatus = RoomStatus.Occupied;
                return room.Level + room.Letter;
            }
            return null;
        }

        public bool CheckoutRoom(string roomNumber)
        {
            int level = Convert.ToInt32(roomNumber.Substring(0, 1));
            string letter = roomNumber.Substring(1, 1);
            Room room = _dataService.Rooms.Where(x => x.Level == level && x.Letter== letter && x.RoomStatus == RoomStatus.Occupied).FirstOrDefault();
            if (room != null)
            {
                room.RoomStatus = RoomStatus.Vacant;
                return true;
            }
            return false;
        }

        public IList<ComboItem> GetAvailableRooms()
        {
            var list = _dataService.Rooms.Where(x=>x.RoomStatus==RoomStatus.Available);
            IList<ComboItem> result=  list.Select(l => new ComboItem { Label = l.Level+l.Letter, Value = l.Level + l.Letter }).ToList();
            return result;
        }

        public IList<ComboItem> GetOccupiedRooms()
        {
            var list = _dataService.Rooms.Where(x => x.RoomStatus == RoomStatus.Occupied);
            IList<ComboItem> result = list.Select(l => new ComboItem { Label = l.Level + l.Letter, Value = l.Level + l.Letter }).ToList();
            return result;
        }

        public IList<ComboItem> GetRepairingRooms()
        {
            var list = _dataService.Rooms.Where(x => x.RoomStatus == RoomStatus.Repair);
            IList<ComboItem> result = list.Select(l => new ComboItem { Label = l.Level + l.Letter, Value = l.Level + l.Letter }).ToList();
            return result;
        }

        public IList<ComboItem> GetVacantRooms()
        {
            var list = _dataService.Rooms.Where(x => x.RoomStatus == RoomStatus.Vacant);
            IList<ComboItem> result = list.Select(l => new ComboItem { Label = l.Level + l.Letter, Value = l.Level + l.Letter }).ToList();
            return result;
        }

        public bool MarkRepairCompleted(string roomNumber)
        {
            int level = Convert.ToInt32(roomNumber.Substring(0, 1));
            string letter = roomNumber.Substring(1, 1);
            Room room = _dataService.Rooms.Where(x => x.Level == level && x.Letter == letter && x.RoomStatus == RoomStatus.Repair).FirstOrDefault();
            if (room != null)
            {
                room.RoomStatus = RoomStatus.Vacant;
                return true;
            }
            return false;
        }

        public bool MarkRoomCleaned(string roomNumber)
        {
            int level = Convert.ToInt32(roomNumber.Substring(0, 1));
            string letter = roomNumber.Substring(1, 1);
            Room room = _dataService.Rooms.Where(x => x.Level == level && x.Letter == letter && x.RoomStatus == RoomStatus.Vacant).FirstOrDefault();
            if (room != null)
            {
                room.RoomStatus = RoomStatus.Available;
                return true;
            }
            return false;
        }

        public bool MarkRoomForRepair(string roomNumber)
        {
            int level = Convert.ToInt32(roomNumber.Substring(0, 1));
            string letter = roomNumber.Substring(1, 1);
            Room room = _dataService.Rooms.Where(x => x.Level == level && x.Letter == letter && x.RoomStatus == RoomStatus.Vacant).FirstOrDefault();
            if (room != null)
            {
                room.RoomStatus = RoomStatus.Repair;
                return true;
            }
            return false;
        }

        public bool ResetAllRoomStatuses()
        {
            var list = _dataService.Rooms.ToList();
            list.ForEach(x => x.RoomStatus = RoomStatus.Available);
            return true;
        }
    }
}
