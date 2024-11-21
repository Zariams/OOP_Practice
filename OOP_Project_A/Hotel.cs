
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_Practice
{
    
    public class Hotel
    {
        private string name;
        private string address;
        private DateTime lastWithdrawal;
        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("Ім'я не може бути порожнім!");
                if (value.Length < 3 || value.Length > 30)
                    throw new ArgumentException($"Ім'я не може бути коротшим за 3 символи та довшим за 30 символів (Введено: {value.Length})!");

                foreach (char c in value)
                {
                    if ((c < 'a' || c > 'z') && (c < 'A' || c > 'Z') && c != ' ')
                        throw new ArgumentException("Ім'я містить непідтримувані символи!");
                }
                name = value;
            }
        }
        public string Address
        {
            get
            {
                return address;
            }
            private set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("Адреса не може бути порожньою!");
                List<string> parts = value.Split(',').ToList();
                if (parts.Count != 4)
                    throw new ArgumentException("Адреса має складатися з 4 частин, розділених комою: номер будинку, назва вулиці, назва міста, назва країни!");
                string house = parts[0].Trim();
                if (house.Length > 4 || house.Length < 1)
                    throw new ArgumentException("Номер будинку має складатися менше, ніж з 5 символів та не може бути порожнім");
                foreach (char c in house)
                    if ((c < 'a' || c > 'z') && (c < 'A' || c > 'Z') && (c > '9' || c < '0'))
                        throw new ArgumentException("Номер будинку містить непідтримувані символи!");
                string street = parts[1].Trim();
                if (street.Length > 20 || street.Length < 5)
                    throw new ArgumentException("Назва вулиці має містити від 5 до 20 символів!");
                foreach (char c in street)
                    if ((c < 'a' || c > 'z') && (c < 'A' || c > 'Z') && c!=' '&&c!='.')
                        throw new ArgumentException("Назва вулиці містить непідтримувані символи!");
                string city = parts[2].Trim();
                if (city.Length > 12 || city.Length < 3)
                    throw new ArgumentException("Назва міста має містити від 3 до 12 символів!");
                foreach (char c in city)
                    if ((c < 'a' || c > 'z') && (c < 'A' || c > 'Z') && c != ' ')
                        throw new ArgumentException("Назва міста містить непідтримувані символи!");
                string country = parts[3].Trim();
                if (country.Length > 12 || country.Length < 3)
                    throw new ArgumentException("Назва країни має містити від 3 до 12 символів!");
                foreach (char c in country)
                    if ((c < 'a' || c > 'z') && (c < 'A' || c > 'Z') && c != ' ')
                        throw new ArgumentException("Назва країни містить непідтримувані символи!");
                address = value;
                //address = value;
            }
        }
        public List<Tenant> Tenants { get; private set; }

        // public Dictionary<HotelRoom,List<Reservation>> Rooms { get; private set; }
        // public Account HotelFunds { get; set; }
        public List<Room> Rooms { get; private set; }
        public List<Reservation> Reservations { get; private set; }
        public List<StaffMember> Staff { get; private set; }
        public Account Account { get; private set; }
        public DateTime LastFeeWithdrawal
        {
            get
            {
                return lastWithdrawal;
            }
            set
            {
                if (value > DateTime.Now) throw new ArgumentException("Дата останнього зняття орендної плати не може бути у майбутньому!");
                lastWithdrawal = value;
            }
        }

        public Hotel(string name, string address)
        {
            Name = name;
            Address = address;
            lastWithdrawal = DateTime.Now;
            Tenants = new List<Tenant>();
            Rooms = new List<Room>();
            Reservations = new List<Reservation>();
            Staff = new List<StaffMember>();
            Account = new Account();

        }

        public List<Room> GetAvailableRooms()
        {
           return GetAvailableRoomsForDates(DateTime.Now.AddSeconds(1),DateTime.Now.AddDays(1).AddSeconds(1));
        }
        public List<Room> GetAvailableRoomsForDates(DateTime dateStart, DateTime dateEnd)
        {
            if (dateEnd < DateTime.Now.AddDays(1))
                throw new ArgumentException("Забронювати номер можна тільки за день!");
            if (dateStart < DateTime.Now)
                throw new ArgumentException("Бронювання номеру не може починатися у минулому!");
            if (dateEnd < dateStart.AddDays(1))
                throw new ArgumentException("Початок бронювання номер має передувати кінцю бронювання не менш, ніж на день");

            List<Room> result = new List<Room>();
            List<Reservation> inactiveReservations = Reservations.FindAll(book => !(book.StartDate < dateEnd && book.EndDate > dateStart)&&!book.IsDeleted);
            foreach (Reservation book in inactiveReservations)
            {
                Room room = Rooms.Find(r => r.ID == book.RoomID);
                if (room is Room)
                    result.Add(room);
            }
            return result;
        }
        
        public void BookARoom(int tenantId, int roomId, DateTime dateStart, DateTime dateEnd)
        {
            bool tenantRegistered = Tenants.Find(x => x.ID == tenantId) is Tenant;
            if (!tenantRegistered)
                throw new ArgumentException("Жильця готелю з цим ідентифікаційним номером ще не зареєстровано!");
            bool roomRegistered = Rooms.Find(x => x.ID == roomId) is Room;
            if (!roomRegistered)
                throw new ArgumentException("Кімнати готелю з цим ідентифікаційним номером ще не зареєстровано!");
            Reservation newReservation = new Reservation(tenantId,roomId,dateStart,dateEnd);
            Reservations.Add(newReservation);
        }
        public void CancelRoomReservation(int tenantId, int roomId)
        {
            bool tenantRegistered = Tenants.Find(x => x.ID == tenantId) is Tenant;
            if (!tenantRegistered)
                throw new KeyNotFoundException("Жильця готелю з цим ідентифікаційним номером ще не зареєстровано!");
            bool roomRegistered = Rooms.Find(x => x.ID == roomId) is Room;
            if (!roomRegistered)
                throw new KeyNotFoundException("Кімнати готелю з цим ідентифікаційним номером ще не зареєстровано!");

            List<Reservation> reservations = Reservations.FindAll(x => x.TenantID == tenantId && x.RoomID == roomId && x.IsDeleted == false);
            if (reservations.Count == 0)
                throw new Exception($"Жилець під номером {tenantId} не має активного бронювання кімнати {roomId}");
            for (int i = 0; i < reservations.Count; i++)
                reservations[i].IsDeleted = true;
        }

        public void RegisterTenant(string firstName, string lastName, DateTime birthDate)
        {
            bool tenantRegistered = Tenants.Find(x => x.FirstName == firstName && x.LastName == lastName && x.BirthDate == birthDate) is Tenant;
            if (tenantRegistered)
                throw new ArgumentException("Жильця готелю з цими даними вже зареєстровано!");
            Tenant newTenant = new Tenant(firstName, lastName, birthDate);
            Tenants.Add(newTenant);
        }
        public void RegisterTenant(Tenant tenant)
        {
            bool tenantRegistered = Tenants.Find(x => x.ID == tenant.ID) is Tenant;
            if (tenantRegistered)
                throw new ArgumentException("Жильця готелю з цим ідентифікаційним номером вже зареєстровано!");
            Tenants.Add(tenant);
        }

        public void RegisterNewRoom(Room room)
        {
            bool roomRegistered = Rooms.Find(x => x.ID == room.ID) is Room;
            if (roomRegistered)
                throw new ArgumentException("Кімнату готелю з цим ідентифікаційним номером вже зареєстровано!");
            Rooms.Add(room);
        }

        public void HireStaff(StaffMember staff)
        {
            bool staffRegistered = Staff.Find(x => x.ID == staff.ID && !x.IsFired) is StaffMember;
            if (staffRegistered)
                throw new Exception("Працівника готелю з цим ідентифікаційним номером вже зареєстровано!");
            Staff.Add(staff);
        }
        public void FireStaff(int ID)
        {
            StaffMember staff = Staff.Find(x => x.ID == ID && !x.IsFired);
            if (!(staff is StaffMember))
                throw new Exception("Працівника готелю з цим ідентифікаційним номером не зареєстровано!");
            staff.IsFired = true;
        }
        public List<Reservation> WithdrawDailyFee()
        {
            List<Reservation> activeReservations = Reservations.FindAll(book => book.IsActiveToday && !book.IsDeleted);
            for (int i = 0; i < activeReservations.Count; i++)
            {
                Reservation book = activeReservations[i];
                Tenant tenant = Tenants.Find(t => t.ID == book.TenantID);
                Room room = Rooms.Find(r => r.ID == book.RoomID);
                if (tenant.Account.Withdraw(room.DailyCost))
                    Account.Deposit(room.DailyCost);
                else
                    book.IsDeleted = true;
            }
            List<Reservation> cancelledReservations = activeReservations.FindAll(book =>book.IsDeleted);
            return cancelledReservations;
        }
        public void PayStaffSalaries()
        {

            List<StaffMember> activeWorkers = Staff.FindAll(x => !x.IsFired);
            double sum = 0;
            foreach (StaffMember worker in activeWorkers)
                sum += worker.DailyRate * Math.Floor((DateTime.Now - worker.LastSalaryPay).TotalDays);
            double availableFundsCoefficient = Math.Min(Account.Balance / sum, 1);
            for (int i = 0; i < activeWorkers.Count; i++)
            {
                StaffMember worker = activeWorkers[i];
                double percent = (worker.DailyRate * Math.Floor((DateTime.Now - worker.LastSalaryPay).TotalDays) / sum);
                double coveredDays = Math.Floor(percent * availableFundsCoefficient* sum / worker.DailyRate);
                if (Account.Withdraw(coveredDays * worker.DailyRate))
                {
                    worker.Account.Deposit(coveredDays * worker.DailyRate);
                    worker.LastSalaryPay = worker.LastSalaryPay.AddDays(coveredDays);
                }
            }
        }
    }
}