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
        public Tenant Tenant { get; private set; }
        public bool IsActive { get; }

        public Reservation(Tenant tenant, DateTime startTime, DateTime endTime) { 
            throw new NotImplementedException();
        }
    }
}