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
            Tenant tenant = new Tenant("John Smith", DateTime.Now.AddYears(-20));
            //Act
            Reservation reservation = new Reservation(tenant,date1,date2);
            //Assert
            Assert.AreEqual(date1, reservation.StartDate);
            Assert.AreEqual(date2, reservation.EndDate);
            Assert.AreEqual(tenant, reservation.Tenant);
            Assert.AreEqual(true, reservation.IsActive);

        }
        [TestMethod]
        public void ConstructorTest_correct_Inactive()
        {
            //Arrange
            DateTime date1 = DateTime.Now.AddDays(1);
            DateTime date2 = DateTime.Now.AddDays(5);
            Tenant tenant = new Tenant("John Smith", DateTime.Now.AddYears(-20));
            //Act
            Reservation reservation = new Reservation(tenant, date1, date2);
            //Assert
            Assert.AreEqual(date1, reservation.StartDate);
            Assert.AreEqual(date2, reservation.EndDate);
            Assert.AreEqual(tenant, reservation.Tenant);
            Assert.AreEqual(false, reservation.IsActive);

        }
        [TestMethod]
        public void ConstructorTest_incorrect_StartDate()
        {
            //Arrange
            DateTime date1 = DateTime.Now.AddDays(-1);
            DateTime date2 = DateTime.Now.AddDays(5);
            Tenant tenant = new Tenant("John Smith", DateTime.Now.AddYears(-20));
           
            //Act + Assert
            Assert.ThrowsException<ArgumentException>(()=>new Reservation(tenant, date1, date2));
        }
        [TestMethod]
        public void ConstructorTest_incorrect_EndDate()
        {
            //Arrange
            DateTime date1 = DateTime.Now.AddDays(1);
            DateTime date2 = DateTime.Now.AddDays(-5);
            Tenant tenant = new Tenant("John Smith", DateTime.Now.AddYears(-20));
            DateTime date3 = DateTime.Now.AddDays(5);
            DateTime date4 = DateTime.Now.AddDays(1);
            //Act + Assert
            Assert.ThrowsException<ArgumentException>(() => new Reservation(tenant, date1, date2));
            Assert.ThrowsException<ArgumentException>(() => new Reservation(tenant, date3, date4));
        }
    }
}
