using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_Practice
{
    public class Reservation
    {
        private DateTime startDate;
        private DateTime endDate;
        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            private set
            {
                if (value< Clock.Now.AddMinutes(-1))
                    throw new ArgumentException("Початкова дата не може бути у минулому!");
                if (endDate == DateTime.MinValue || value.AddMinutes(1) < endDate.AddDays(-1))
                    startDate = value;
                else
                    throw new ArgumentException("Початкова дата бронювання має передувати кінцевій даті більше, ніж на добу!");
            }
        }
        public DateTime EndDate { 
            get
            {
                return endDate;
            }
            private set 
            {
                if (value < Clock.Now.AddMinutes(-1))
                    throw new ArgumentException("Кінцева дата не може бути у минулому!");

                if (startDate == DateTime.MinValue || startDate.AddDays(1).AddMinutes(1) < value)
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
                return IsActive(Clock.Now);
            }
        }
        public bool IsDeleted { get; set; }

        public Reservation(int tenantId, int roomId, DateTime startTime, DateTime endTime) { 
            TenantID = tenantId;
            RoomID = roomId;
            this.StartDate = startTime;
            this.EndDate = endTime; 
        }
        public bool IsActive(DateTime date)
        {
            return (StartDate <= date.AddMinutes(1) && date <= EndDate.AddMinutes(1));
            // return ((date-(StartDate ?? Clock.Now)).TotalMinutes > 1 && ((EndDate ?? Clock.Now) - date).TotalMinutes > 1);
        }
    }
}