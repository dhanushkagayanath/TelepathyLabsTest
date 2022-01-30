using Models;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class DataService : IDataService
    {
        //IList<Room> IDataService.Rooms { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public DataService()
        {
            IList<Room> Rooms = new List<Room>();
            for (int i = 1; i < 5; i++)
            {
                Rooms.Add(new Room(i, "A"));
                Rooms.Add(new Room(i, "B"));
                Rooms.Add(new Room(i, "C"));
                Rooms.Add(new Room(i, "D"));
                Rooms.Add(new Room(i, "E"));
            }
            _rooms = Rooms;

        }

        private IList<Room> _rooms;
        public IList<Room> Rooms
        {
            get { return this._rooms; }
            set { this._rooms = value; }
        }


    }
}
