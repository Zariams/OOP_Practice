using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_Practice
{
    public class HotelRoom
    {
        public Hotel Hotel { get; set; }
        public int RoomID { get; set; }
        public RoomType Type { get; set; }
        public int DailyCost { get; set; } 
        public int Capacity { get; set; }

     //   public Tenant Tenant { get; set; }
     //   public bool IsAvailable { get; set; }
     //   public List<RoomReservation> Reservations { get; set; }

        public HotelRoom(int roomID, RoomType roomType, int capacity, Hotel hotel)
        {
            throw new NotImplementedException();
        }
        
    }
}