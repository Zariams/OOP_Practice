using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP_Practice;

namespace TestProject
{
    [TestClass]
    public class TestTenant
    {

        [TestMethod]
        public void ConstructorTest_correct()
        {
            //Arrange
            string firstName = "John";
            string lastName = "Smith";
            DateTime birthdate = new DateTime(1980, 1, 1);
            int currentCounter = Tenant.Counter;
            //Act
            Tenant tenant = new Tenant(firstName,lastName, birthdate);
            //Assert
            Assert.AreEqual(firstName, tenant.FirstName);
            Assert.AreEqual(lastName, tenant.LastName);
            Assert.AreEqual(birthdate, tenant.BirthDate);
            Assert.IsNotNull(tenant.Account);
            Assert.AreEqual(currentCounter + 1, tenant.ID);
            Assert.AreEqual(currentCounter + 1, Tenant.Counter);
        }
        [TestMethod]
        [DataRow(null)]
        [DataRow("12345")]
        [DataRow("A")]
        [DataRow("Джон")]
        [DataRow("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA")]
        public void ConstructorTest_incorrect_FirstName(string name)
        {
            //Arrange
            DateTime birthdate = new DateTime(1980, 1, 1);
            int currentCounter = Tenant.Counter;
            //Act + Assert
            Assert.ThrowsException<ArgumentException>(() => new Tenant(name,"Smith", birthdate));
            Assert.AreEqual(currentCounter, Tenant.Counter);
        }
        [TestMethod]
        [DataRow("12345")]
        [DataRow("A")]
        [DataRow("Сміт")]
        [DataRow("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA")]
        public void ConstructorTest_incorrect_LastName(string name)
        {
            //Arrange
            DateTime birthdate = new DateTime(1980, 1, 1);
            int currentCounter = Tenant.Counter;
            //Act + Assert
            Assert.ThrowsException<ArgumentException>(() => new Tenant("John",name, birthdate));
            Assert.AreEqual(currentCounter, Tenant.Counter);
        }
        [TestMethod]
        public void ConstructorTest_incorrect_Date()
        {
            //Arrange
            string firstName = "John";
            string lastName = "Smith";
            DateTime birthdate1 = new DateTime(2980, 1, 1);
            DateTime birthdate2 = Clock.Now.AddYears(-15);
            DateTime birthdate3 = Clock.Now.AddYears(-200);
            int currentCounter = Tenant.Counter;
            //Act + Assert
            Assert.ThrowsException<ArgumentException>(() => new Tenant(firstName, lastName, birthdate1));
            Assert.ThrowsException<ArgumentException>(() => new Tenant(firstName, lastName, birthdate2));
            Assert.ThrowsException<ArgumentException>(() => new Tenant(firstName, lastName, birthdate3));
            Assert.AreEqual(currentCounter, Tenant.Counter);
        }
    }
}
