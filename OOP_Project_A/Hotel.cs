
using OOP_Practice;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;



namespace OOP_Practice
{
    public delegate void DateHandler(DateTime date);
    public class Hotel
    {
        private string name;
        private string address;
        private DateTime lastDayCheck;
        private DateTime lastWeekCheck;
        private DateTime lastMonthCheck;
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
            }
        }

        private List<Person> people { get; set; }
        public List<Tenant> Tenants { 
            get 
            {
                return people.OfType<Tenant>().ToList();
            }
        }
        public List<StaffMember> Staff
        {
            get
            {
                return people.OfType<StaffMember>().ToList();
            }
        }

        public List<Room> Rooms { get; private set; }
        public List<Reservation> Reservations { get; private set; }
       
        public Account Account { get; private set; }
        public DateTime LastRentWithdrawal
        {
            get
            {
                return lastWithdrawal;
            }
            set
            {
                if (value > Clock.Now) throw new ArgumentException("Дата останнього зняття орендної плати не може бути у майбутньому!");
                lastWithdrawal = value;
            }
        }
        public DateTime LastDayCheck
        {
            get => lastDayCheck;
        }
        public DateTime LastWeekCheck
        {
            get => lastWeekCheck;
        }
        public DateTime LastMonthCheck
        {
            get => lastMonthCheck;
        }
        public event DateHandler DayPassed;
        public event DateHandler WeekPassed;
        public event DateHandler MonthPassed;

        public static Action<string,string> Message;
        public Hotel(string name, string address)
        {
            Name = name;
            Address = address;
            lastWithdrawal = Clock.Now;
          //  Tenants = new List<Tenant>();
            people = new List<Person>();    
            Rooms = new List<Room>();
            Reservations = new List<Reservation>();
          //  Staff = new List<StaffMember>();
            Account = new Account();
            lastDayCheck = Clock.Now;
            lastWeekCheck = Clock.Now;
            lastMonthCheck = Clock.Now;
            DayPassed = DayPassedDefault;
            ChooseRentInterval(0);
            ChooseSalaryInterval(0);
        }

        public List<Room> GetAvailableRooms()
        {
           return GetAvailableRoomsForDates(Clock.Now.AddSeconds(1),Clock.Now.AddDays(1).AddSeconds(1));
        }
        public List<Room> GetAvailableRoomsForDates(DateTime dateStart, DateTime dateEnd)
        {
            if (dateStart < Clock.Now.AddMinutes(-1))
                throw new ArgumentException("Бронювання номеру не може починатися у минулому!");
            if (dateEnd < dateStart.AddDays(1))
                throw new ArgumentException("Початок бронювання номер має передувати кінцю бронювання не менш, ніж на день");

            List<Room> result = Rooms.ToList();
            List<Reservation> inactiveReservations = Reservations.FindAll(book => (book.StartDate < dateEnd && book.EndDate > dateStart)&&!book.IsDeleted);
            foreach (Reservation book in inactiveReservations)
            {
                Room room = Rooms.Find(r => r.ID == book.RoomID);
                if (room is Room)
                    result.Remove(room);
            }
            return result;
        }
        public double GetExpectedRentPay()
        {
            double result = 0;
            if (Clock.Now < LastRentWithdrawal.AddDays(1))
                return result;
            for (int offset = 1; offset <= (Clock.Now - LastRentWithdrawal).TotalDays; offset++)
            {
                DateTime date = LastRentWithdrawal.AddDays(offset);
                List<Reservation> activeReservations = Reservations.FindAll(book => book.IsActive(date) && !book.IsDeleted);
                for (int i = 0; i < activeReservations.Count; i++)
                {
                    Reservation book = activeReservations[i];
                    Room room = Rooms.Find(r => r.ID == book.RoomID);
                    result += room.DailyCost;
                }
            }
            return result;

        }
        public double GetExpectedTotalSalary(DateTime date)
        {
            List<StaffMember> activeWorkers = Staff.FindAll(x => !x.IsFired);
            double sum = 0;
            foreach (StaffMember worker in activeWorkers)
                sum += worker.DailyRate * Math.Floor((date - worker.LastSalaryPay).TotalDays);
            return sum;
        }
        public void BookARoom(int tenantId, int roomId, DateTime dateStart, DateTime dateEnd)
        {
            Tenant tenant = Tenants.Find(x => x.ID == tenantId);
            if (!(tenant is Tenant))
                throw new ArgumentException("Жильця готелю з цим ідентифікаційним номером ще не зареєстровано!");
           Room room = Rooms.Find(x => x.ID == roomId);
            if (!(room is Room))
                throw new ArgumentException("Кімнати готелю з цим ідентифікаційним номером ще не зареєстровано!");
            if (GetAvailableRoomsForDates(dateStart, dateEnd).Find(room1 => room1.ID == roomId) == null)
                throw new Exception("На обраний час кімнату вже було заброньовано!");
            Reservation newReservation = new Reservation(tenantId,roomId,dateStart,dateEnd);
            Reservations.Add(newReservation);
            Message?.Invoke($"Бронювання кімнати номер {room.ID} готелю {Name} на ім'я {tenant} було успішно збережено!", "Успіх!");
        }
        public void CancelRoomReservation(int tenantId, int roomId)
        {
            Tenant tenant = Tenants.Find(x => x.ID == tenantId);
            if (!(tenant is Tenant))
                throw new KeyNotFoundException("Жильця готелю з цим ідентифікаційним номером ще не зареєстровано!");
            Room room = Rooms.Find(x => x.ID == roomId);
            if (!(room is Room))
                throw new KeyNotFoundException("Кімнати готелю з цим ідентифікаційним номером ще не зареєстровано!");

            List<Reservation> reservations = Reservations.FindAll(x => x.TenantID == tenantId && x.RoomID == roomId && x.IsDeleted == false);
            if (reservations.Count == 0)
                throw new Exception($"Жилець під номером {tenantId} не має активного бронювання кімнати {roomId}");
            for (int i = 0; i < reservations.Count; i++)
                reservations[i].IsDeleted = true;
            Message?.Invoke($"Бронювання кімнати номер {room.ID} готелю {Name} на ім'я {tenant} було успішно скасовано!", "Успіх!");
        }
        public void CancelRoomReservation(Reservation book)
        {
            if (!Reservations.FindAll(x => x.IsDeleted == false).Contains(book))
                throw new Exception("Такого запису не існує у системі готелю!");
            book.IsDeleted = true;
            Message?.Invoke($"Бронювання кімнати номер {book.RoomID} готелю {Name} на ім'я {book.TenantID} було успішно скасовано!", "Успіх!");
        }
        public void RegisterTenant(string firstName, string lastName, DateTime birthDate)
        {
            bool tenantRegistered = Tenants.Find(x => x.FirstName == firstName && x.LastName == lastName && x.BirthDate == birthDate) is Tenant;
            if (tenantRegistered)
                throw new ArgumentException("Жильця готелю з цими даними вже зареєстровано!");
            Person newTenant = new Tenant(firstName, lastName, birthDate);
            people.Add(newTenant);
            Message?.Invoke($"{newTenant} було успішно додано до списку жильців готелю {Name}","Успіх!");
        }
        public void RegisterTenant(Tenant tenant)
        {
            bool tenantRegistered = Tenants.Find(x => x.ID == tenant.ID) is Tenant;
            if (tenantRegistered)
                throw new ArgumentException("Жильця готелю з цим ідентифікаційним номером вже зареєстровано!");
            people.Add(tenant);
            Message?.Invoke($"{tenant} було успішно додано до списку жильців готелю {Name}", "Успіх!");
        }

        public void RegisterNewRoom(Room room)
        {
            bool roomRegistered = Rooms.Find(x => x.ID == room.ID) is Room;
            if (roomRegistered)
                throw new ArgumentException("Кімнату готелю з цим ідентифікаційним номером вже зареєстровано!");
            Rooms.Add(room);
            Message?.Invoke($"Кімнату було успішно додано до готелю {Name}", "Успіх!");
        }

        public void HireStaff(StaffMember staff)
        {
            bool staffRegistered = Staff.Find(x => x.ID == staff.ID && !x.IsFired) is StaffMember;
            if (staffRegistered)
                throw new Exception("Працівника готелю з цим ідентифікаційним номером вже зареєстровано!");
            people.Add(staff);
            Message?.Invoke($"{staff} було успішно додано до списку робітників готелю {Name}", "Успіх!");
        }
        public void HireStaff(string firstName, string lastName, DateTime birthDate, Job job, double salary)
        {
            bool staffRegistered = Staff.Find(x => x.FirstName == firstName && x.LastName == lastName && x.BirthDate == birthDate && !x.IsFired) is StaffMember;
            if (staffRegistered)
                throw new ArgumentException("Робітника готелю з цими даними вже зареєстровано!");
            StaffMember staff = new StaffMember(firstName, lastName, birthDate,job,salary);
            people.Add(staff);
            Message?.Invoke($"{staff} було успішно додано до списку робітників готелю {Name}", "Успіх!");
        }
        public void FireStaff(int ID)
        {
            StaffMember staff = Staff.Find(x => x.ID == ID && !x.IsFired);
            if (!(staff is StaffMember))
                throw new Exception("Працівника готелю з цим ідентифікаційним номером не зареєстровано!");
            staff.IsFired = true;
            Message?.Invoke($"{staff} було успішно видалено зі списку робітників готелю {Name}", "Успіх!");
        }
        public void WithdrawRoomRent(DateTime date0)
        {
            if (date0 < LastRentWithdrawal.AddDays(1))
                Message?.Invoke("Сьогодні орендна плата вже була знята!", "Помилка!");
            List<Reservation> cancelledReservations = new List<Reservation>();
            for (int offset = 1; offset <= (date0-LastRentWithdrawal).TotalDays; offset++)
            {
                DateTime date = LastRentWithdrawal.AddDays(offset);
                List<Reservation> activeReservations = Reservations.FindAll(book => book.IsActive(date) && !book.IsDeleted);
                for (int i = 0; i < activeReservations.Count; i++)
                {
                    Reservation book = activeReservations[i];
                    Tenant tenant = Tenants.Find(t => t.ID == book.TenantID);
                    Room room = Rooms.Find(r => r.ID == book.RoomID);
                    if (tenant.Account.Withdraw(room.DailyCost))
                        Account.Deposit(room.DailyCost);
                    else
                        book.IsDeleted = true;
                    if ((book.EndDate - date).TotalDays < 1)
                    {
                        book.IsDeleted = true;
                        activeReservations.Remove(book);
                    }
                        
                }
                cancelledReservations.AddRange(activeReservations.FindAll(book => book.IsDeleted));
            }
            LastRentWithdrawal = date0;
            if (cancelledReservations.Count > 0)
                Message?.Invoke($"Через недостаток грошей на рахунках клієнтів, було скасовано {cancelledReservations.Count} оренд(и)", "Увага!");
            else
                Message?.Invoke("Орендна плата була успішно знята у повному обсязі!", "Успіх!");
           // return cancelledReservations;
        }
        public void PayStaffSalaries(DateTime date)
        {

            List<StaffMember> activeWorkers = Staff.FindAll(x => !x.IsFired);
            if (activeWorkers.Count == 0)
                Message?.Invoke("У готелі наразі ніхто не працює","Помилка!");
            double currentBalance = Account.Balance;
            double sum = GetExpectedTotalSalary(date);
            double availableFundsCoefficient = Math.Min(Account.Balance / sum, 1);
            for (int i = 0; i < activeWorkers.Count; i++)
            {
                StaffMember worker = activeWorkers[i];
                double percent = (worker.DailyRate * Math.Floor((date - worker.LastSalaryPay).TotalDays) / sum);
                double coveredDays = Math.Floor(percent * availableFundsCoefficient* sum / worker.DailyRate);
                if (coveredDays != 0&&Account.Withdraw(coveredDays * worker.DailyRate))
                {
                    worker.Account.Deposit(coveredDays * worker.DailyRate);
                    worker.LastSalaryPay = worker.LastSalaryPay.AddDays(coveredDays);
                }
            }
            if (Account.Balance != currentBalance - sum)
            {
               Message?.Invoke($"Через недостаток грошей на рахунку готелю, заробітну плату було виплачено не в повному обсязі!", "Повідомлення");
            }
            else
            {
                Message?.Invoke("Заробітну плату було успішно сплачено!", "Повідомлення");
            }
        }

        private void DayPassedDefault(DateTime date)
        {
            Message?.Invoke($"Настав новий день", "Календар");
            if ((date-lastWeekCheck).TotalDays >= 7)
            {
                Message?.Invoke($"Настав новий тиждень", "Календар");
                WeekPassed?.Invoke(date);
                lastWeekCheck = date;
            }
            if ((date.Month > lastMonthCheck.Month || date.Year > lastMonthCheck.Year) && date.Day >= lastMonthCheck.Day)
            {
                Message?.Invoke($"Настав новий місяць", "Календар");
                MonthPassed?.Invoke(date);
                lastMonthCheck = date;
            }
            lastDayCheck = date;
        }

        public void CheckTime()
        {
            int daysPassed = (int)Math.Floor((Clock.Now-lastDayCheck).TotalDays);
            if (daysPassed <= 0)
                throw new Exception("Сьогодні вже були виконані всі перевірки!");
            for (int i = 0; i < daysPassed; i++)
            {
                DayPassed?.Invoke(lastDayCheck.AddDays(1));
            }
            if (daysPassed == 1)
                Message?.Invoke($"Було успішно виконано всі перевірки за сьогодні", "Успіх!");
            else
                Message?.Invoke($"Було успішно виконано всі перевірки за останні {daysPassed} днів", "Успіх!");
        }

       
        public void ChooseRentInterval(int choiceNumber)
        {
            DayPassed -= WithdrawRoomRent;
            WeekPassed -= WithdrawRoomRent;
            MonthPassed -= WithdrawRoomRent;
            switch (choiceNumber)
            {
                case 0:
                    DayPassed += WithdrawRoomRent;
                    break;
                case 1:
                    WeekPassed += WithdrawRoomRent;
                    break;
                case 2:
                    MonthPassed += WithdrawRoomRent;
                    break;
                default: throw new ArgumentException("Невірно заданий номер!");

            }
        }
        public void ChooseSalaryInterval(int choiceNumber)
        {
            DayPassed -= PayStaffSalaries;
            WeekPassed -= PayStaffSalaries;
            MonthPassed -= PayStaffSalaries;
            switch (choiceNumber)
            {
                case 0:
                    DayPassed += PayStaffSalaries;
                    break;
                case 1:
                    WeekPassed += PayStaffSalaries;
                    break;
                case 2:
                    MonthPassed += PayStaffSalaries;
                    break;
                default: throw new ArgumentException("Невірно заданий номер!");
            }
        }
    }
}