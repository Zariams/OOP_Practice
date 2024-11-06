using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_Practice
{
    public class Hotel
    {
        public string Name { get; private set; } = "WIP";
        public string Address { get; private set; } = "WIP";
        private List<Tenant> Tenants { get; set; }
        public Dictionary<HotelRoom,List<Reservation>> Rooms { get; private set; }
       // public Account HotelFunds { get; set; }
       // public List<Room> Rooms { get; set; }

        public Hotel(string name, string address)
        {
            throw new NotImplementedException();
        }

        public List<HotelRoom> GetAvailableRooms()
        {
            throw new NotImplementedException();
        }
        public List<HotelRoom> GetAvailableRoomsForDates(DateTime dateStart, DateTime dateEnd)
        {
            throw new NotImplementedException();
        }
        
        public void BookARoom(Tenant tenant, int roomId, DateTime dateStart, DateTime dateEnd)
        {
            throw new NotImplementedException();
        }
        public void CancelRoomReservation(Tenant tenant, int roomId)
        {
            throw new NotImplementedException();
        }
        public void CancelRoomReservation(Reservation reservation)
        {
            throw new NotImplementedException();
        }
        public void RegisterTenant(string name, DateTime birthDate)
        {
            throw new NotImplementedException();
        }
        public void WithdrawDailyFee()
        {
            throw new NotImplementedException();
        }
    }
}