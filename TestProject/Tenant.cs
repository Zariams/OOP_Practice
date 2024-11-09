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
            string name = "John Smith";
            DateTime birthdate = new DateTime(1980, 1, 1);
            int currentCounter = Tenant.Counter;
            //Act
            Tenant tenant = new Tenant(name, birthdate);
            //Assert
            Assert.AreEqual(name, tenant.Name);
            Assert.AreEqual(birthdate, tenant.BirthDate);
            Assert.IsNotNull(tenant.Account);
            Assert.AreEqual(currentCounter + 1, tenant.ID);
            Assert.AreEqual(currentCounter + 1, Tenant.Counter);
        }
        [TestMethod]
        public void ConstructorTest_incorrect_Name()
        {
            //Arrange
            string name1 = "12345";
            string name2 = "A";
            string name3 = "Джон";
            string name4 = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa";
            DateTime birthdate = new DateTime(1980, 1, 1);
            int currentCounter = Tenant.Counter;
            //Act + Assert
            Assert.ThrowsException<ArgumentException>(() => new Tenant(name1, birthdate));
            Assert.ThrowsException<ArgumentException>(() => new Tenant(name2, birthdate));
            Assert.ThrowsException<ArgumentException>(() => new Tenant(name3, birthdate));
            Assert.ThrowsException<ArgumentException>(() => new Tenant(name4, birthdate));
            Assert.AreEqual(currentCounter, Tenant.Counter);
        }
        [TestMethod]
        public void ConstructorTest_incorrect_Date()
        {
            //Arrange
            string name = "John Smith";
            DateTime birthdate1 = new DateTime(2980, 1, 1);
            DateTime birthdate2 = DateTime.Now;
            DateTime birthdate3 = DateTime.Now.AddYears(-200);
            int currentCounter = Tenant.Counter;
            //Act + Assert
            Assert.ThrowsException<ArgumentException>(() => new Tenant(name, birthdate1));
            Assert.ThrowsException<ArgumentException>(() => new Tenant(name, birthdate2));
            Assert.ThrowsException<ArgumentException>(() => new Tenant(name, birthdate3));
            Assert.AreEqual(currentCounter, Tenant.Counter);
        }
    }
}
