using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP_Practice;

namespace TestProject
{
    [TestClass]
    public class TestReservation
    {

        [TestMethod]
        public void ConstructorTest_correct_Active()
        {
            //Arrange
            DateTime date1 = DateTime.Now.AddSeconds(-1);
            DateTime date2 = DateTime.Now.AddDays(5);
            Tenant tenant = new Tenant("John", "Smith", DateTime.Now.AddYears(-20));
            int tenantID = tenant.ID;
            Room room = new Room(5, RoomType.Budget);
            int roomID = room.ID;
            //Act
            Reservation reservation = new Reservation(tenantID, roomID, date1, date2);
            //Assert
            Assert.AreEqual(date1, reservation.StartDate);
            Assert.AreEqual(date2, reservation.EndDate);
            Assert.AreEqual(tenantID, reservation.TenantID);
            Assert.AreEqual(roomID, reservation.RoomID);
            Assert.IsTrue(reservation.IsActiveToday);
            Assert.IsFalse(reservation.IsDeleted);
        }
        [TestMethod]
        public void ConstructorTest_correct_Inactive()
        {
            //Arrange
            DateTime date1 = DateTime.Now.AddDays(1);
            DateTime date2 = DateTime.Now.AddDays(5);
            Tenant tenant = new Tenant("John", "Smith", DateTime.Now.AddYears(-20));
            int tenantID = tenant.ID;
            Room room = new Room(5, RoomType.Budget);
            int roomID= room.ID;
            //Act
            Reservation reservation = new Reservation(tenantID,roomID, date1, date2);
            //Assert
            Assert.AreEqual(date1, reservation.StartDate);
            Assert.AreEqual(date2, reservation.EndDate);
            Assert.AreEqual(tenantID, reservation.TenantID);
            Assert.AreEqual(roomID, reservation.RoomID);
            Assert.IsFalse(reservation.IsActiveToday);
            Assert.IsFalse(reservation.IsDeleted);
        }
        [TestMethod]
        public void ConstructorTest_incorrect_StartDate()
        {
            //Arrange
            DateTime date1 = DateTime.Now.AddDays(-1);
            DateTime date2 = DateTime.Now.AddDays(5);
            Tenant tenant = new Tenant("John", "Smith", DateTime.Now.AddYears(-20));
            int tenantID = tenant.ID;
            Room room = new Room(5, RoomType.Budget);
            int roomID = room.ID;
            //Act + Assert
            Assert.ThrowsException<ArgumentException>(()=>new Reservation(tenantID,roomID, date1, date2));
        }
        [TestMethod]
        public void ConstructorTest_incorrect_EndDate()
        {
            //Arrange
            Tenant tenant = new Tenant("John", "Smith", DateTime.Now.AddYears(-20));
            int tenantID = tenant.ID;
            Room room = new Room(5, RoomType.Budget);
            int roomID = room.ID;

            DateTime date1 = DateTime.Now.AddDays(1);
            DateTime date2 = DateTime.Now.AddDays(-5);
            DateTime date3 = DateTime.Now.AddDays(5);
            DateTime date4 = DateTime.Now.AddDays(1);
            DateTime date5 = DateTime.Now.AddDays(1);
            DateTime date6 = DateTime.Now.AddDays(1).AddHours(1);
            //Act + Assert
            Assert.ThrowsException<ArgumentException>(() => new Reservation(tenantID, roomID, date1, date2));
            Assert.ThrowsException<ArgumentException>(() => new Reservation(tenantID,roomID, date3, date4));
            Assert.ThrowsException<ArgumentException>(() => new Reservation(tenantID, roomID, date5, date6));
        }
    }
}
