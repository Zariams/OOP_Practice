
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
        public List<Tenant> Tenants { get; private set; }

        // public Dictionary<HotelRoom,List<Reservation>> Rooms { get; private set; }
        // public Account HotelFunds { get; set; }
        public List<Room> Rooms { get; private set; }
        public List<Reservation> Reservations { get; private set; }
        public List<StaffMember> Staff { get; private set; }
        public Account Account { get; private set; }
        public DateTime LastFeeWithdrawal {get; set;}

        public Hotel(string name, string address)
        {
            throw new NotImplementedException();
        }

        public List<Room> GetAvailableRooms()
        {
            throw new NotImplementedException();
        }
        public List<Room> GetAvailableRoomsForDates(DateTime dateStart, DateTime dateEnd)
        {
            throw new NotImplementedException();
        }
        
        public void BookARoom(int tenantId, int roomId, DateTime dateStart, DateTime dateEnd)
        {
            throw new NotImplementedException();
        }
        public void CancelRoomReservation(int tenantId, int roomId)
        {
            throw new NotImplementedException();
        }

        public void RegisterTenant(string firstName, string lastName, DateTime birthDate)
        {
            throw new NotImplementedException();
        }
        public void RegisterTenant(Tenant tenant)
        {
            throw new NotImplementedException();
        }

        public void RegisterNewRoom(Room room)
        {
            throw new NotImplementedException();
        }

        public void HireStaff(StaffMember staff)
        {
            throw new NotImplementedException();   
        }
        public void FireStaff(int ID)
        {
            throw new NotImplementedException();
        }
        public List<Reservation> WithdrawDailyFee()
        {
            throw new NotImplementedException();
        }
        public void PayStaffSalaries()
        {
            throw new NotImplementedException();
        }
    }
}