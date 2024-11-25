using OOP_Practice;
namespace TestProject
{
    [TestClass]
    public class TestHotel
    {
        [TestMethod]
        public void ConstructorTest_correct()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            //Act
            Hotel hotel = new Hotel(name, address);
            //Assert
            Assert.AreEqual(name, hotel.Name);
            Assert.AreEqual(address, hotel.Address);
            Assert.IsNotNull(hotel.Rooms);
            Assert.AreEqual(0, hotel.Rooms.Count);
            Assert.IsNotNull(hotel.Staff);
            Assert.IsNotNull(hotel.Account);
            Assert.AreEqual(0, (Clock.Now-hotel.LastRentWithdrawal).TotalSeconds,0.01);
        }
        [TestMethod]
        public void ConstructorTest_incorrect_Name()
        {
            //Arrange
            string name1 = "1234567";
            string name2 = "Мій готель";
            string name3 = "A";
            string name4 = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            //Act + Assert
            Assert.ThrowsException<ArgumentException>(() => new Hotel(name1, address));
            Assert.ThrowsException<ArgumentException>(() => new Hotel(name2, address));
            Assert.ThrowsException<ArgumentException>(() => new Hotel(name3, address));
            Assert.ThrowsException<ArgumentException>(() => new Hotel(name4, address));

        }
        [TestMethod]
        [DataRow("3, вул. Шевченко, Львів, Україна")]
        [DataRow("AAAAAA, Shevchenko st., Lviv, Ukraine")]
        [DataRow("3, Ukraine")]
        [DataRow("3, 12345., Lviv, Ukraine")]
        [DataRow("3, Shevchenko st., 12345, Ukraine")]
        [DataRow("3, Shevchenko st., Lviv, 123456")]
        [DataRow("3, A, Lviv, Ukraine")]
        [DataRow("3, AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa, Lviv, Ukraine")]
        [DataRow("3, Shevchenko st., A, Ukraine")]
        [DataRow("3, Shevchenko st., AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA, Ukraine")]
        [DataRow("3, Shevchenko st., Lviv, A")]
        [DataRow("3, Shevchenko st., Lviv, AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA")]

        public void ConstructorTest_incorrect_Address(string address)
        {
            //Arrange
            string name = "My Hotel";
            //Act + Assert
            Assert.ThrowsException<ArgumentException>(() => new Hotel(name, address));

        }
        [TestMethod]
        public void LastFeeWithdrawalTest_Correct()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);
            DateTime lastfee = Clock.Now.AddDays(-10);
            //Act
            hotel.LastRentWithdrawal = lastfee;
            //Assert
            Assert.AreEqual(lastfee, hotel.LastRentWithdrawal); 
        }
        [TestMethod]
        public void LastFeeWithdrawalTest_Incorrect()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);
            DateTime lastfee = Clock.Now.AddDays(10);
            //Act + Assert
           Assert.ThrowsException<ArgumentException>(()=> hotel.LastRentWithdrawal = lastfee);
        }
        [TestMethod]
        public void RegisterNewTenantTest_Correct()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);
            string tenantFirstName1 = "John";
            string tenantLastName1 = "Smith";
            string tenantFirstName2 = "Lara";
            string tenantLastName2 = "Crowley";
            DateTime birthDate1 = Clock.Now.AddYears(-20);
            DateTime birthDate2 = Clock.Now.AddYears(-19);
            Tenant tenant2 = new Tenant(tenantFirstName2,tenantLastName2, birthDate2);
            //Act
            hotel.RegisterTenant(tenantFirstName1, tenantLastName1, birthDate1);
            hotel.RegisterTenant(tenant2);
            //Assert
            Assert.AreEqual(2, hotel.Tenants.Count);
            Assert.AreEqual(tenantFirstName1, hotel.Tenants[0].FirstName);
            Assert.AreEqual(tenantLastName1, hotel.Tenants[0].LastName);
            Assert.AreEqual(birthDate1, hotel.Tenants[0].BirthDate);
            Assert.AreEqual(tenantFirstName2, hotel.Tenants[1].FirstName);
            Assert.AreEqual(tenantLastName2, hotel.Tenants[1].LastName);
            Assert.AreEqual(birthDate2, hotel.Tenants[1].BirthDate);
        }
        
        [TestMethod]
        public void RegisterNewTenantTest_Incorrect_Tenant_Information()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);
            string incorrectName = "12345";
            DateTime birthDate = Clock.Now.AddYears(-5);
            //Act+Assert
            Assert.ThrowsException<ArgumentException>(()=>hotel.RegisterTenant(incorrectName, "Smith", birthDate));
            Assert.ThrowsException<ArgumentException>(() => hotel.RegisterTenant("John",incorrectName, birthDate));
            Assert.AreEqual(0, hotel.Tenants.Count);

        }
        [TestMethod]
        public void RegisterNewTenantTest_Incorrect_Tenant_already_registered()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);
            string tenantFirstName = "John";
            string tenantLastName = "Smith";
            DateTime birthDate = Clock.Now.AddYears(-20);
            Tenant tenant = new Tenant(tenantFirstName,tenantLastName, birthDate);
            //Act
            hotel.RegisterTenant(tenant);
            //Assert
           Assert.ThrowsException<ArgumentException>(() => hotel.RegisterTenant(tenant));
            Assert.ThrowsException<ArgumentException>(() => hotel.RegisterTenant(tenantFirstName, tenantLastName, birthDate));

            Assert.AreEqual(1,hotel.Tenants.Count);
        }

        [TestMethod]
        public void RegisterNewRoom_Correct()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);
            int cost = 5;
            RoomType type = RoomType.Business;
            Room room = new Room(cost, type);
            //Act
            hotel.RegisterNewRoom(room);
            //Assert
            Assert.AreEqual(1, hotel.Rooms.Count);
            Assert.IsTrue(hotel.Rooms.Contains(room));
        }
        [TestMethod]
        public void RegisterNewRoom_Correct_multiple()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);
            int cost = 5;
            RoomType type = RoomType.Business;
            Room room1 = new Room(cost, type);
            Room room2 = new Room(cost, type);
            //Act
            hotel.RegisterNewRoom(room1);
            hotel.RegisterNewRoom(room2);
            //Assert
            Assert.AreEqual(2, hotel.Rooms.Count);
            Assert.IsTrue(hotel.Rooms.Contains(room1));
            Assert.IsTrue(hotel.Rooms.Contains(room2));
        }
        [TestMethod]
        public void RegisterNewRoom_Incorrect_room_already_registered()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);
            int cost = 5;
            RoomType type = RoomType.Business;
            Room room1 = new Room(cost, type);
            //Act + Assert
            hotel.RegisterNewRoom(room1);
            Assert.ThrowsException<ArgumentException>(()=>hotel.RegisterNewRoom(room1));
            Assert.AreEqual(1, hotel.Rooms.Count);
        }
        [TestMethod]
        public void BookARoomTest_Correct()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);

            Room room = new Room(5, RoomType.Business);
            hotel.RegisterNewRoom(room);

            Tenant tenant = new Tenant("John", "Smith", Clock.Now.AddYears(-20));
            hotel.RegisterTenant(tenant);

            DateTime date1 = Clock.Now;
            DateTime date2 = Clock.Now.AddDays(5);
            //Act

            hotel.BookARoom(tenant.ID, room.ID, date1, date2);
            //Assert
            Assert.AreEqual(1, hotel.Reservations.Count);
            Assert.AreEqual(tenant.ID, hotel.Reservations[0].TenantID);
            Assert.AreEqual(date1, hotel.Reservations[0].StartDate);
            Assert.AreEqual(date2, hotel.Reservations[0].EndDate);
            Assert.IsFalse(hotel.Reservations[0].IsDeleted);
        }
        [TestMethod]
        public void BookARoomTest_Incorrect_RoomID()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);

            Room room = new Room(5, RoomType.Business);
            hotel.RegisterNewRoom(room);

            Tenant tenant = new Tenant("John", "Smith", Clock.Now.AddYears(-20));
            hotel.RegisterTenant(tenant);

            DateTime date1 = Clock.Now;
            DateTime date2 = Clock.Now.AddDays(5);
            //Act+Assert

            Assert.ThrowsException<ArgumentException>(()=>hotel.BookARoom(tenant.ID, room.ID+1, date1, date2));           
            
        }
        [TestMethod]
        public void BookARoomTest_Incorrect_TenantID()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);

            Room room = new Room(5, RoomType.Business);
            hotel.RegisterNewRoom(room);

            Tenant tenant = new Tenant("John", "Smith", Clock.Now.AddYears(-20));
            hotel.RegisterTenant(tenant);

            DateTime date1 = Clock.Now;
            DateTime date2 = Clock.Now.AddDays(5);
            //Act+Assert

            Assert.ThrowsException<ArgumentException>(() => hotel.BookARoom(tenant.ID+1, room.ID, date1, date2));

        }
        [TestMethod]
        public void BookARoomTest_Incorrect_Date()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);

            Room room = new Room(5, RoomType.Business);
            hotel.RegisterNewRoom(room);

            Tenant tenant = new Tenant("John", "Smith", Clock.Now.AddYears(-20));
            hotel.RegisterTenant(tenant);

            DateTime date1 = Clock.Now;
            DateTime date2 = Clock.Now.AddDays(-5);
            DateTime date3 = Clock.Now.AddDays(5);
            hotel.BookARoom(tenant.ID, room.ID, date1, date3);

            //Act+Assert

            Assert.ThrowsException<ArgumentException>(() => hotel.BookARoom(tenant.ID, room.ID, date1, date2));
            Assert.ThrowsException<ArgumentException>(() => hotel.BookARoom(tenant.ID, room.ID, date2, date1));
            Assert.ThrowsException<Exception>(() => hotel.BookARoom(tenant.ID, room.ID, date1, date3));
        }
        [TestMethod]
        public void CancelRoomReservationTest_Correct()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);

            Room room = new Room(5, RoomType.Business);
            hotel.RegisterNewRoom(room);

            Tenant tenant = new Tenant("John", "Smith", Clock.Now.AddYears(-20));
            hotel.RegisterTenant(tenant);

            DateTime date1 = Clock.Now;
            DateTime date2 = Clock.Now.AddDays(5);
            hotel.BookARoom(tenant.ID, room.ID, date1, date2);
            //Act
            hotel.CancelRoomReservation(tenant.ID, room.ID);

            //Assert
            Assert.AreEqual(1, hotel.Reservations.Count);
            Assert.IsTrue(hotel.Reservations[0].IsDeleted);

        }
        [TestMethod]
        public void CancelRoomReservationTest_Incorrect_Tenant_not_Found()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);

            Room room = new Room(5, RoomType.Business);
            hotel.RegisterNewRoom(room);

            Tenant tenant = new Tenant("John", "Smith", Clock.Now.AddYears(-20));
            
            //Act+Assert
            Assert.ThrowsException<KeyNotFoundException>(()=>hotel.CancelRoomReservation(tenant.ID, room.ID));


        }
        [TestMethod]
        public void CancelRoomReservationTest_Incorrect_Room_not_Found()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);

            Room room = new Room(5, RoomType.Business);

            Tenant tenant = new Tenant("John", "Smith", Clock.Now.AddYears(-20));
            hotel.RegisterTenant(tenant);

            DateTime date1 = Clock.Now;
            DateTime date2 = Clock.Now.AddDays(5);

            //Act+Assert
            Assert.ThrowsException<KeyNotFoundException>(() => hotel.CancelRoomReservation(tenant.ID, room.ID));
        }
        [TestMethod]
        public void GetAvailableRoomsForDatesTest_Correct()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);

            Room room1 = new Room(5, RoomType.Business);
            hotel.RegisterNewRoom(room1);

            Tenant tenant1 = new Tenant("John", "Smith", Clock.Now.AddYears(-20));
            hotel.RegisterTenant(tenant1);

            DateTime date1 = Clock.Now.AddSeconds(1);
            DateTime date2 = Clock.Now.AddDays(5);
            hotel.BookARoom(tenant1.ID, room1.ID, date1, date2);

            Room room2 = new Room(5, RoomType.Business);
            hotel.RegisterNewRoom(room2);

            Tenant tenant2 = new Tenant("John", "Smith", Clock.Now.AddYears(-20));
            hotel.RegisterTenant(tenant2);

            DateTime date3 = Clock.Now.AddDays(6);
            DateTime date4 = Clock.Now.AddDays(10);
            hotel.BookARoom(tenant2.ID, room2.ID, date3, date4);

            hotel.BookARoom(tenant1.ID, room2.ID, date1, date2);
            hotel.CancelRoomReservation(tenant1.ID, room2.ID);
            //Act
            List<Room> result1 = hotel.GetAvailableRoomsForDates(Clock.Now.AddSeconds(1), Clock.Now.AddDays(3));
            List<Room> result2 = hotel.GetAvailableRoomsForDates(Clock.Now.AddDays(6), Clock.Now.AddDays(8));
            List<Room> result3 = hotel.GetAvailableRoomsForDates(Clock.Now.AddDays(4), Clock.Now.AddDays(10));
            //Assert
            Assert.AreEqual(1, result1.Count);
            Assert.AreEqual(room2, result1[0]);
            Assert.AreEqual(1, result2.Count);
            Assert.AreEqual(room1, result2[0]);
            Assert.AreEqual(0, result3.Count);
        }
        [TestMethod]
        public void GetAvailableRoomsForDatesTest_Incorrect()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);

            Room room1 = new Room(5, RoomType.Business);
            hotel.RegisterNewRoom(room1);

            Tenant tenant1 = new Tenant("John", "Smith", Clock.Now.AddYears(-20));
            hotel.RegisterTenant(tenant1);

            DateTime date1 = Clock.Now;
            DateTime date2 = Clock.Now.AddDays(5);
            hotel.BookARoom(tenant1.ID, room1.ID, date1, date2);
            //Act + Assert
            Assert.ThrowsException<ArgumentException>(()=>hotel.GetAvailableRoomsForDates(Clock.Now, Clock.Now.AddDays(-3)));
            Assert.ThrowsException<ArgumentException>(() => hotel.GetAvailableRoomsForDates(Clock.Now.AddDays(-1), Clock.Now.AddDays(3)));
        }
        [TestMethod]
        public void GetAvailableRoomsTest()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);

            Room room1 = new Room(5, RoomType.Business);
            hotel.RegisterNewRoom(room1);

            Tenant tenant1 = new Tenant("John", "Smith", Clock.Now.AddYears(-20));
            hotel.RegisterTenant(tenant1);

            DateTime date1 = Clock.Now;
            DateTime date2 = Clock.Now.AddDays(5);
            hotel.BookARoom(tenant1.ID, room1.ID, date1, date2);

            Room room2 = new Room(5, RoomType.Business);
            hotel.RegisterNewRoom(room2);

            Tenant tenant2 = new Tenant("John", "Smith", Clock.Now.AddYears(-20));
            hotel.RegisterTenant(tenant2);

            DateTime date3 = Clock.Now.AddDays(6);
            DateTime date4 = Clock.Now.AddDays(10);
            hotel.BookARoom(tenant2.ID, room2.ID, date3, date4);

            hotel.BookARoom(tenant1.ID, room2.ID, date1, date2);
            hotel.CancelRoomReservation(tenant1.ID, room2.ID);
            //Act
            List<Room> result = hotel.GetAvailableRooms();
            //Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(room2, result[0]);
        }
        [TestMethod]
        public void WithdrawDailyFeeTest_Correct (){
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);
            hotel.LastRentWithdrawal = Clock.Now.AddDays(-1);

            Room room1 = new Room(15, RoomType.Budget);
            hotel.RegisterNewRoom(room1);

            Tenant tenant1 = new Tenant("John", "Smith", Clock.Now.AddYears(-20));
            tenant1.Account.Deposit(15);
            hotel.RegisterTenant(tenant1);
             
            DateTime date1 = Clock.Now;
            DateTime date2 = Clock.Now.AddDays(5);
            hotel.BookARoom(tenant1.ID, room1.ID, date1, date2);

            Room room2 = new Room(500, RoomType.Luxury);
            hotel.RegisterNewRoom(room2);

            Tenant tenant2 = new Tenant("Laura", "Crowley", Clock.Now.AddYears(-20));
            tenant2.Account.Deposit(15);
            hotel.RegisterTenant(tenant2);

            DateTime date3 = Clock.Now.AddDays(6);
            DateTime date4 = Clock.Now.AddDays(10);
            hotel.BookARoom(tenant2.ID, room2.ID, date3, date4);

            Tenant tenant3 = new Tenant("John", "Smith", Clock.Now.AddYears(-20));
            hotel.RegisterTenant(tenant3);

            hotel.BookARoom(tenant3.ID, room2.ID, date1, date2);

            Room room3 = new Room(500, RoomType.Luxury);
            hotel.RegisterNewRoom(room3);
            hotel.BookARoom(tenant1.ID, room3.ID, date1, date2);
            hotel.CancelRoomReservation(tenant1.ID, room3.ID);
            //Act
            List<Reservation> cancelledReservations = hotel.WithdrawRoomRent();
            //Assert
            Assert.AreEqual(0, tenant1.Account.Balance);
            Assert.AreEqual(15, tenant2.Account.Balance);
            Assert.AreEqual(0, tenant3.Account.Balance);
            Assert.AreEqual(15, hotel.Account.Balance);
            Assert.IsTrue(hotel.Reservations.Find(x => x.TenantID == tenant3.ID).IsDeleted);
            Assert.AreEqual(1,cancelledReservations.Count);
            Assert.AreEqual(tenant3.ID, cancelledReservations[0].TenantID);
            Assert.AreEqual(0, (Clock.Now - hotel.LastRentWithdrawal).TotalSeconds, 0.01);
        }
        [TestMethod]
        public void WithdrawDailyFeeTest_Correct_Multiple_Days()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);
            hotel.LastRentWithdrawal = Clock.Now.AddDays(-1);

            Room room1 = new Room(15, RoomType.Budget);
            hotel.RegisterNewRoom(room1);

            Tenant tenant1 = new Tenant("John", "Smith", Clock.Now.AddYears(-20));
            tenant1.Account.Deposit(45);
            hotel.RegisterTenant(tenant1);

            DateTime date1 = Clock.Now;
            DateTime date2 = Clock.Now.AddDays(5);
            hotel.BookARoom(tenant1.ID, room1.ID, date1, date2);

            Room room2 = new Room(500, RoomType.Luxury);
            hotel.RegisterNewRoom(room2);

            Tenant tenant2 = new Tenant("Laura", "Crowley", Clock.Now.AddYears(-20));
            tenant2.Account.Deposit(15);
            hotel.RegisterTenant(tenant2);

            DateTime date3 = Clock.Now.AddDays(6);
            DateTime date4 = Clock.Now.AddDays(10);
            hotel.BookARoom(tenant2.ID, room2.ID, date3, date4);

            Tenant tenant3 = new Tenant("John", "Smith", Clock.Now.AddYears(-20));
            hotel.RegisterTenant(tenant3);

            hotel.BookARoom(tenant3.ID, room2.ID, date1, date2);

            Room room3 = new Room(500, RoomType.Luxury);
            hotel.RegisterNewRoom(room3);
            hotel.BookARoom(tenant1.ID, room3.ID, date1, date2);
            hotel.CancelRoomReservation(tenant1.ID, room3.ID);

            Clock.Offset = Clock.Offset.Add(new TimeSpan(2, 0, 0, 0));
            //Act
            List<Reservation> cancelledReservations = hotel.WithdrawRoomRent();
            //Assert
            Assert.AreEqual(0, tenant1.Account.Balance);
            Assert.AreEqual(15, tenant2.Account.Balance);
            Assert.AreEqual(0, tenant3.Account.Balance);
            Assert.AreEqual(45, hotel.Account.Balance);
            Assert.IsTrue(hotel.Reservations.Find(x => x.TenantID == tenant3.ID).IsDeleted);
            Assert.AreEqual(1, cancelledReservations.Count);
            Assert.AreEqual(tenant3.ID, cancelledReservations[0].TenantID);
            Assert.AreEqual(0, (Clock.Now - hotel.LastRentWithdrawal).TotalSeconds, 0.01);
            Clock.Offset = TimeSpan.Zero;
        }
        [TestMethod]
        public void WithdrawDailyFeeTest_Incorrect()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);
            hotel.LastRentWithdrawal = Clock.Now.AddDays(-1);

            Room room1 = new Room(15, RoomType.Budget);
            hotel.RegisterNewRoom(room1);

            Tenant tenant1 = new Tenant("John", "Smith", Clock.Now.AddYears(-20));
            tenant1.Account.Deposit(15);
            hotel.RegisterTenant(tenant1);

            DateTime date1 = Clock.Now;
            DateTime date2 = Clock.Now.AddDays(5);
            hotel.BookARoom(tenant1.ID, room1.ID, date1, date2);

            Room room2 = new Room(500, RoomType.Luxury);
            hotel.RegisterNewRoom(room2);

            Tenant tenant2 = new Tenant("Laura", "Crowley", Clock.Now.AddYears(-20));
            tenant2.Account.Deposit(15);
            hotel.RegisterTenant(tenant2);

            DateTime date3 = Clock.Now.AddDays(6);
            DateTime date4 = Clock.Now.AddDays(10);
            hotel.BookARoom(tenant2.ID, room2.ID, date3, date4);

            Tenant tenant3 = new Tenant("John", "Smith", Clock.Now.AddYears(-20));
            hotel.RegisterTenant(tenant3);

            hotel.BookARoom(tenant3.ID, room2.ID, date1, date2);

            Room room3 = new Room(500, RoomType.Luxury);
            hotel.RegisterNewRoom(room3);
            hotel.BookARoom(tenant1.ID, room3.ID, date1, date2);
            hotel.CancelRoomReservation(tenant1.ID, room3.ID);
            //Act
            List<Reservation> cancelledReservations = hotel.WithdrawRoomRent();
            //Assert
            Assert.ThrowsException<Exception>(() => hotel.WithdrawRoomRent());
            Assert.AreEqual(0, tenant1.Account.Balance);
            Assert.AreEqual(15, tenant2.Account.Balance);
            Assert.AreEqual(0, tenant3.Account.Balance);
            Assert.AreEqual(15, hotel.Account.Balance);
            Assert.IsTrue(hotel.Reservations.Find(x => x.TenantID == tenant3.ID).IsDeleted);
            Assert.AreEqual(1, cancelledReservations.Count);
            Assert.AreEqual(tenant3.ID, cancelledReservations[0].TenantID);
            Assert.AreEqual(0, (Clock.Now - hotel.LastRentWithdrawal).TotalSeconds, 0.01);
        }
        [TestMethod]
        public void HireStaffTest_Correct()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);
            string staffMemberFirstName1 = "John";
            string staffMemberLastName1 = "Smith";
            string staffMemberFirstName2 = "Lara";
            string staffMemberLastName2 = "Crowley";
            DateTime birthDate1 = Clock.Now.AddYears(-20);
            DateTime birthDate2 = Clock.Now.AddYears(-19);
            Job job1 = Job.Attendant;
            Job job2 = Job.Concierge;
            double salary1 = 25;
            double salary2 = 50;
            StaffMember staffMember1 = new StaffMember(staffMemberFirstName1, staffMemberLastName1, birthDate1, job1, salary1);
            StaffMember staffMember2 = new StaffMember(staffMemberFirstName2, staffMemberLastName2, birthDate2,job2,salary2);
            //Act
            hotel.HireStaff(staffMember1);
            hotel.HireStaff(staffMember2);
            //Assert
            Assert.AreEqual(2, hotel.Staff.Count);
            Assert.AreEqual(staffMemberFirstName1, hotel.Staff[0].FirstName);
            Assert.AreEqual(staffMemberLastName1, hotel.Staff[0].LastName);
            Assert.AreEqual(birthDate1, hotel.Staff[0].BirthDate);
            Assert.AreEqual(staffMemberFirstName2, hotel.Staff[1].FirstName);
            Assert.AreEqual(staffMemberLastName2, hotel.Staff[1].LastName);
            Assert.AreEqual(birthDate2, hotel.Staff[1].BirthDate);
            Assert.AreEqual(salary2, hotel.Staff[1].DailyRate);
            Assert.AreEqual(job2, hotel.Staff[1].Job);
            Assert.AreEqual(salary1, hotel.Staff[0].DailyRate);
            Assert.AreEqual(job1, hotel.Staff[0].Job);
        }

        [TestMethod]
        public void HireStaffTest_Incorrect_StaffMember_already_registered()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);
            string staffMemberFirstName = "John";
            string staffMemberLastName = "Smith";
            DateTime birthDate = Clock.Now.AddYears(-20);
            Job job = Job.Attendant;
            double salary = 25;
            StaffMember staffMember = new StaffMember(staffMemberFirstName, staffMemberLastName, birthDate,job,salary);
            //Act
            hotel.HireStaff(staffMember);
            //Assert
            Assert.ThrowsException<Exception>(() => hotel.HireStaff(staffMember));
            Assert.AreEqual(1, hotel.Staff.Count);
        }
        [TestMethod]
        public void FireStaffTest_Correct()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);
            string staffMemberFirstName = "John";
            string staffMemberLastName = "Smith";
            DateTime birthDate = Clock.Now.AddYears(-20);
            Job job = Job.Attendant;
            double salary = 25;
            StaffMember staffMember = new StaffMember(staffMemberFirstName, staffMemberLastName, birthDate, job, salary);
            hotel.HireStaff(staffMember);
            //Act
            hotel.FireStaff(staffMember.ID);
            //Assert
            Assert.AreEqual(1, hotel.Staff.Count);
            Assert.IsTrue(hotel.Staff[0].IsFired);
            Assert.IsTrue(hotel.Staff.Contains(staffMember));
        }
        [TestMethod]
        public void FireStaffTest_Incorrect()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);
            string staffMemberFirstName = "John";
            string staffMemberLastName = "Smith";
            DateTime birthDate = Clock.Now.AddYears(-20);
            Job job = Job.Attendant;
            double salary = 25;
            StaffMember staffMember = new StaffMember(staffMemberFirstName, staffMemberLastName, birthDate, job, salary);
            hotel.HireStaff(staffMember);
            //Act+Assert
            Assert.ThrowsException<Exception>(()=>hotel.FireStaff(staffMember.ID+5));
            Assert.AreEqual(1, hotel.Staff.Count);
            Assert.IsFalse(hotel.Staff[0].IsFired);
            Assert.IsTrue(hotel.Staff.Contains(staffMember));
        }
        [TestMethod]
        public void PayStaffSalariesTest_Correct()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);

            string staffMemberFirstName1 = "John";
            string staffMemberLastName1 = "Smith";
            Job job1 = Job.Attendant;
            DateTime birthDate1 = Clock.Now.AddYears(-20);
            double salary1 = 25;
            DateTime date1 = Clock.Now.AddDays(-2);
            StaffMember staffMember1 = new StaffMember(staffMemberFirstName1, staffMemberLastName1, birthDate1, job1, salary1);
            staffMember1.LastSalaryPay = date1;

            string staffMemberFirstName2 = "Lara";
            string staffMemberLastName2 = "Crowley";
            DateTime birthDate2 = Clock.Now.AddYears(-19);
            Job job2 = Job.Concierge;
            double salary2 = 75;
            DateTime date2 = Clock.Now.AddDays(-1);
            StaffMember staffMember2 = new StaffMember(staffMemberFirstName2, staffMemberLastName2, birthDate2, job2, salary2);
            staffMember2.LastSalaryPay = date2;

            hotel.HireStaff(staffMember1);
            hotel.HireStaff(staffMember2);
            hotel.Account.Deposit(200);
            //Act
            hotel.PayStaffSalaries();
            //Assert
            Assert.AreEqual(75, hotel.Account.Balance);
            Assert.AreEqual(50,staffMember1.Account.Balance);
            Assert.AreEqual(75,staffMember2.Account.Balance);
            Assert.AreEqual(0, (Clock.Now-staffMember1.LastSalaryPay).TotalSeconds,0.01);
            Assert.AreEqual(0, (Clock.Now - staffMember2.LastSalaryPay).TotalSeconds, 0.01);
        }
        [TestMethod]
        public void PayStaffSalariesTest_not_enough_funds()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);

            string staffMemberFirstName1 = "John";
            string staffMemberLastName1 = "Smith";
            Job job1 = Job.Attendant;
            DateTime birthDate1 = Clock.Now.AddYears(-20);
            double salary1 = 25;
            DateTime date1 = Clock.Now.AddDays(-10);
            StaffMember staffMember1 = new StaffMember(staffMemberFirstName1, staffMemberLastName1, birthDate1, job1, salary1);
            staffMember1.LastSalaryPay = date1;

            string staffMemberFirstName2 = "Lara";
            string staffMemberLastName2 = "Crowley";
            DateTime birthDate2 = Clock.Now.AddYears(-19);
            Job job2 = Job.Concierge;
            double salary2 = 75;
            DateTime date2 = Clock.Now.AddDays(-10);
            StaffMember staffMember2 = new StaffMember(staffMemberFirstName2, staffMemberLastName2, birthDate2, job2, salary2);
            staffMember2.LastSalaryPay = date2;

            hotel.HireStaff(staffMember1);
            hotel.HireStaff(staffMember2);
            hotel.Account.Deposit(200);
            //Act
            hotel.PayStaffSalaries();
            //Assert
            Assert.AreEqual(0, hotel.Account.Balance);
            Assert.AreEqual(50, staffMember1.Account.Balance);
            Assert.AreEqual(150, staffMember2.Account.Balance);
            Assert.AreEqual(0, (Clock.Now.AddDays(-8) - staffMember1.LastSalaryPay).TotalSeconds, 0.01);
            Assert.AreEqual(0, (Clock.Now.AddDays(-8) - staffMember2.LastSalaryPay).TotalSeconds, 0.01);
        }
        [TestMethod]
        public void GetExpectedRentPayTest()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);
            hotel.LastRentWithdrawal = Clock.Now.AddDays(-1);

            Room room1 = new Room(15, RoomType.Budget);
            hotel.RegisterNewRoom(room1);

            Tenant tenant1 = new Tenant("John", "Smith", Clock.Now.AddYears(-20));
            tenant1.Account.Deposit(15);
            hotel.RegisterTenant(tenant1);

            DateTime date1 = Clock.Now;
            DateTime date2 = Clock.Now.AddDays(5);
            hotel.BookARoom(tenant1.ID, room1.ID, date1, date2);

            Room room2 = new Room(500, RoomType.Luxury);
            hotel.RegisterNewRoom(room2);

            Tenant tenant2 = new Tenant("Laura", "Crowley", Clock.Now.AddYears(-20));
            tenant2.Account.Deposit(15);
            hotel.RegisterTenant(tenant2);

            DateTime date3 = Clock.Now.AddDays(6);
            DateTime date4 = Clock.Now.AddDays(10);
            hotel.BookARoom(tenant2.ID, room2.ID, date3, date4);

            Tenant tenant3 = new Tenant("John", "Smith", Clock.Now.AddYears(-20));
            hotel.RegisterTenant(tenant3);

            hotel.BookARoom(tenant3.ID, room2.ID, date1, date2);

            Room room3 = new Room(500, RoomType.Luxury);
            hotel.RegisterNewRoom(room3);
            hotel.BookARoom(tenant1.ID, room3.ID, date1, date2);
            hotel.CancelRoomReservation(tenant1.ID, room3.ID);
            //Act
            double sum = hotel.GetExpectedRentPay();
            //Assert
            Assert.AreEqual(0, hotel.Account.Balance);
            Assert.AreEqual(515, sum, 0.01);
        }
        [TestMethod]
        public void GetExpectedSalaryTest() {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);

            string staffMemberFirstName1 = "John";
            string staffMemberLastName1 = "Smith";
            Job job1 = Job.Attendant;
            DateTime birthDate1 = Clock.Now.AddYears(-20);
            double salary1 = 25;
            DateTime date1 = Clock.Now.AddDays(-2);
            StaffMember staffMember1 = new StaffMember(staffMemberFirstName1, staffMemberLastName1, birthDate1, job1, salary1);
            staffMember1.LastSalaryPay = date1;

            string staffMemberFirstName2 = "Lara";
            string staffMemberLastName2 = "Crowley";
            DateTime birthDate2 = Clock.Now.AddYears(-19);
            Job job2 = Job.Concierge;
            double salary2 = 75;
            DateTime date2 = Clock.Now.AddDays(-1);
            StaffMember staffMember2 = new StaffMember(staffMemberFirstName2, staffMemberLastName2, birthDate2, job2, salary2);
            staffMember2.LastSalaryPay = date2;

            hotel.HireStaff(staffMember1);
            hotel.HireStaff(staffMember2);
            hotel.Account.Deposit(200);
            //Act
            double sum = hotel.GetExpectedRentPay();
            //Assert
            Assert.AreEqual(200, hotel.Account.Balance);
            Assert.AreEqual(0, sum);
        }
    }
}