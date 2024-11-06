using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_Practice
{
    public class HotelRoom
    {
        public static int Counter { get; private set; }
        public Hotel Hotel { get; set; }
        public int RoomID { get; private set; }
        public RoomType Type { get; set; }
        public int DailyCost { get; set; } 
        //   public Tenant Tenant { get; set; }
        //   public List<RoomReservation> Reservations { get; set; }

        public HotelRoom(int cost, RoomType roomType)
        {
            throw new NotImplementedException();
        }
        
    }
}