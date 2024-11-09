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
        // public Tenant Tenant { get; private set; }
        public int TenantID;
        public int RoomID;
        public bool IsActive { get; }

        public Reservation(int tenantId, int roomId, DateTime startTime, DateTime endTime) { 
            throw new NotImplementedException();
        }
    }
}