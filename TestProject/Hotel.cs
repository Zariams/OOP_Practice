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
        [DataRow("AAAAAAAAAAAAAAa, Shevchenko st., Lviv, Ukraine")]
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
        public void RegisterNewTenantTest_Correct()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);
            string tenantName1 = "John Smith";
            string tenantName2 = "Lara Smith";
            DateTime birthDate1 = DateTime.Now.AddYears(-20);
            DateTime birthDate2 = DateTime.Now.AddYears(-19);
            Tenant tenant2 = new Tenant(tenantName2, birthDate2);
            //Act
            hotel.RegisterTenant(tenantName1, birthDate1);
            hotel.RegisterTenant(tenant2);
            //Assert
            Assert.AreEqual(1, hotel.Tenants.Count);
            Assert.AreEqual(tenantName1, hotel.Tenants[0].Name);
            Assert.AreEqual(birthDate1, hotel.Tenants[0].BirthDate);
            Assert.AreEqual(tenantName2, hotel.Tenants[1].Name);
            Assert.AreEqual(birthDate2, hotel.Tenants[1].BirthDate);
        }
        
        [TestMethod]
        public void RegisterNewTenantTest_Incorrect_Tenant_Information()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);
            string tenantName = "12345";
            DateTime birthDate = DateTime.Now.AddYears(-5);
            //Act+Assert
            Assert.ThrowsException<ArgumentException>(()=>hotel.RegisterTenant(tenantName, birthDate));
            Assert.AreEqual(0, hotel.Tenants.Count);

        }
        [TestMethod]
        public void RegisterNewTenantTest_Incorrect_Tenant_already_registered()
        {
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);
            string tenantName = "John Smith";
            DateTime birthDate = DateTime.Now.AddYears(-20);
            Tenant tenant = new Tenant(tenantName, birthDate);
            //Act
            hotel.RegisterTenant(tenantName, birthDate);
            //Assert
            Assert.ThrowsException<ArgumentException>(() => hotel.RegisterTenant(tenantName, birthDate));
            Assert.ThrowsException<ArgumentException>(() => hotel.RegisterTenant(tenant));
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

            Tenant tenant = new Tenant("John Smith", DateTime.Now.AddYears(-20));
            hotel.RegisterTenant(tenant);

            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Now.AddDays(5);
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

            Tenant tenant = new Tenant("John Smith", DateTime.Now.AddYears(-20));
            hotel.RegisterTenant(tenant);

            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Now.AddDays(5);
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

            Tenant tenant = new Tenant("John Smith", DateTime.Now.AddYears(-20));
            hotel.RegisterTenant(tenant);

            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Now.AddDays(5);
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

            Tenant tenant = new Tenant("John Smith", DateTime.Now.AddYears(-20));
            hotel.RegisterTenant(tenant);

            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Now.AddDays(-5);
            //Act+Assert

            Assert.ThrowsException<ArgumentException>(() => hotel.BookARoom(tenant.ID, room.ID, date1, date2));
            Assert.ThrowsException<ArgumentException>(() => hotel.BookARoom(tenant.ID, room.ID, date2, date1));

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

            Tenant tenant = new Tenant("John Smith", DateTime.Now.AddYears(-20));
            hotel.RegisterTenant(tenant);

            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Now.AddDays(5);
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

            Tenant tenant = new Tenant("John Smith", DateTime.Now.AddYears(-20));
            
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

            Tenant tenant = new Tenant("John Smith", DateTime.Now.AddYears(-20));
            hotel.RegisterTenant(tenant);

            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Now.AddDays(5);

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

            Tenant tenant1 = new Tenant("John Smith", DateTime.Now.AddYears(-20));
            hotel.RegisterTenant(tenant1);

            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Now.AddDays(5);
            hotel.BookARoom(tenant1.ID, room1.ID, date1, date2);

            Room room2 = new Room(5, RoomType.Business);
            hotel.RegisterNewRoom(room2);

            Tenant tenant2 = new Tenant("John Smith", DateTime.Now.AddYears(-20));
            hotel.RegisterTenant(tenant2);

            DateTime date3 = DateTime.Now.AddDays(6);
            DateTime date4 = DateTime.Now.AddDays(10);
            hotel.BookARoom(tenant2.ID, room2.ID, date3, date4);

            hotel.BookARoom(tenant1.ID, room2.ID, date1, date2);
            hotel.CancelRoomReservation(tenant1.ID, room2.ID);
            //Act
            List<Room> result1 = hotel.GetAvailableRoomsForDates(DateTime.Now, DateTime.Now.AddDays(3));
            List<Room> result2 = hotel.GetAvailableRoomsForDates(DateTime.Now.AddDays(6), DateTime.Now.AddDays(8));
            List<Room> result3 = hotel.GetAvailableRoomsForDates(DateTime.Now.AddDays(4), DateTime.Now.AddDays(10));
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

            Tenant tenant1 = new Tenant("John Smith", DateTime.Now.AddYears(-20));
            hotel.RegisterTenant(tenant1);

            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Now.AddDays(5);
            hotel.BookARoom(tenant1.ID, room1.ID, date1, date2);
            //Act + Assert
            Assert.ThrowsException<ArgumentException>(()=>hotel.GetAvailableRoomsForDates(DateTime.Now, DateTime.Now.AddDays(-3)));
            Assert.ThrowsException<ArgumentException>(() => hotel.GetAvailableRoomsForDates(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(3)));
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

            Tenant tenant1 = new Tenant("John Smith", DateTime.Now.AddYears(-20));
            hotel.RegisterTenant(tenant1);

            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Now.AddDays(5);
            hotel.BookARoom(tenant1.ID, room1.ID, date1, date2);

            Room room2 = new Room(5, RoomType.Business);
            hotel.RegisterNewRoom(room2);

            Tenant tenant2 = new Tenant("John Smith", DateTime.Now.AddYears(-20));
            hotel.RegisterTenant(tenant2);

            DateTime date3 = DateTime.Now.AddDays(6);
            DateTime date4 = DateTime.Now.AddDays(10);
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
        public void WithdrawDailyFeeTest (){
            //Arrange
            string name = "My Hotel";
            string address = "3, Shevchenko st., Lviv, Ukraine";
            Hotel hotel = new Hotel(name, address);

            Room room1 = new Room(15, RoomType.Budget);
            hotel.RegisterNewRoom(room1);

            Tenant tenant1 = new Tenant("John Smith", DateTime.Now.AddYears(-20));
            tenant1.Account.Deposit(15);
            hotel.RegisterTenant(tenant1);

            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Now.AddDays(5);
            hotel.BookARoom(tenant1.ID, room1.ID, date1, date2);

            Room room2 = new Room(500, RoomType.Luxury);
            hotel.RegisterNewRoom(room2);

            Tenant tenant2 = new Tenant("Laura Smith", DateTime.Now.AddYears(-20));
            tenant2.Account.Deposit(15);
            hotel.RegisterTenant(tenant2);

            DateTime date3 = DateTime.Now.AddDays(6);
            DateTime date4 = DateTime.Now.AddDays(10);
            hotel.BookARoom(tenant2.ID, room2.ID, date3, date4);

            Tenant tenant3 = new Tenant("John Broke", DateTime.Now.AddYears(-20));
            hotel.RegisterTenant(tenant3);

            hotel.BookARoom(tenant3.ID, room2.ID, date1, date2);

            Room room3 = new Room(500, RoomType.Luxury);
            hotel.RegisterNewRoom(room3);
            hotel.BookARoom(tenant1.ID, room3.ID, date1, date2);
            hotel.CancelRoomReservation(tenant1.ID, room3.ID);
            //Act
            hotel.WithdrawDailyFee();
            //Assert
            Assert.AreEqual(0, tenant1.Account.Balance);
            Assert.AreEqual(15, tenant2.Account.Balance);
            Assert.AreEqual(0, tenant3.Account.Balance);
            Assert.IsTrue(hotel.Reservations.Find(x => x.TenantID == tenant3.ID)?.IsDeleted);
        }
    }
}