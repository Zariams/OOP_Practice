using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_Practice
{
    public class Reservation
    {
        private DateTime? startDate;
        private DateTime? endDate;
        public DateTime? StartDate
        {
            get
            {
                return startDate;
            }
            private set
            {
                if (value< DateTime.Now.AddMinutes(-1))
                    throw new ArgumentException("Початкова дата не може бути у минулому!");
                if (endDate == null || value < endDate?.AddDays(-1))
                    startDate = value;
                else
                    throw new ArgumentException("Початкова дата бронювання має передувати кінцевій даті більше, ніж на добу!");
            }
        }
        public DateTime? EndDate { 
            get
            {
                return endDate;
            }
            private set 
            {
                if (value < DateTime.Now.AddMinutes(-1))
                    throw new ArgumentException("Кінцева дата не може бути у минулому!");

                if (startDate == null || startDate?.AddDays(1) < value)
                    endDate = value;
                else
                    throw new ArgumentException("Кінцева дата бронювання не може передувати початковій даті менше, ніж на добу!");
            } 
        }
        public int TenantID { get; set; }
        public int RoomID { get; set; }
        public bool IsActiveToday { 
            get 
            {
                return (StartDate <= DateTime.Now && DateTime.Now <= EndDate);
            }
        }
        public bool IsDeleted { get; set; }

        public Reservation(int tenantId, int roomId, DateTime startTime, DateTime endTime) { 
            TenantID = tenantId;
            RoomID = roomId;
            this.StartDate = startTime;
            this.EndDate = endTime; 
        }
    }
}