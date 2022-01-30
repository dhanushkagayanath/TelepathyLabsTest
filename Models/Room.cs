using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Room : IComparable
    {
        public Room(int Level, string Letter)
        {
            this.Level = Level;
            this.Letter = Letter;
            RoomStatus = RoomStatus.Available;

        }
        public int Level { get; set; }
        public string Letter { get; set; }
        public RoomStatus RoomStatus { get; set; }

        public int CompareTo(object obj)
        {
            Room room = (Room)obj;
            if (this.Level != room.Level) return Comparer<int>.Default.Compare(this.Level, room.Level);
            else
            {
                bool evenLevel = this.Level % 2 == 0;
                if (evenLevel) return Comparer<string>.Default.Compare(room.Letter, this.Letter);
                else return Comparer<string>.Default.Compare(this.Letter, room.Letter);
            }
        }
    }

    public enum RoomStatus
    {
        Available,
        Occupied,
        Vacant,
        Repair
    }
}
