using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interface
{
    public interface IDataService
    {
        IList<Room> Rooms { get; set; }
    }
}
