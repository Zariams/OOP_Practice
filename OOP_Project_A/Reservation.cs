using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_Practice
{
    public class Reservation
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Tenant Tenant { get; set; }

        public Reservation(Tenant tenant, DateTime startTime, DateTime endTime) { 
            throw new NotImplementedException();
        }
    }
}