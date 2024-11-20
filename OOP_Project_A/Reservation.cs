using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_Practice
{
    public class Reservation
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public int TenantID;
        public int RoomID;
        public bool IsActiveToday { get; }
        public bool IsDeleted { get; set; }

        public Reservation(int tenantId, int roomId, DateTime startTime, DateTime endTime) { 
            throw new NotImplementedException();
        }
    }
}