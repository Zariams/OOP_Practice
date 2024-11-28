using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP_Practice;

namespace TestProject
{
    [TestClass]
    public class StaffMemberTest
    {

        [TestMethod]
        public void ConstructorTest_correct()
        {
            //Arrange
            string firstName = "John";
            string lastName = "Smith";
            DateTime birthdate = new DateTime(1980, 1, 1);
            int currentCounter = StaffMember.Counter;
            Job job = Job.Concierge;
            double salary = 75;
            //Act
            StaffMember staffMember = new StaffMember(firstName, lastName, birthdate,job,salary);
            //Assert
            Assert.AreEqual(firstName, staffMember.FirstName);
            Assert.AreEqual(lastName, staffMember.LastName);
            Assert.AreEqual(birthdate, staffMember.BirthDate);
            Assert.IsNotNull(staffMember.Account);
            Assert.AreEqual(currentCounter + 1, staffMember.ID);
            Assert.AreEqual(currentCounter + 1, StaffMember.Counter);
            Assert.IsFalse(staffMember.IsFired);
            Assert.AreEqual(0,(Clock.Now-staffMember.LastSalaryPay).TotalSeconds,0.01);
            Assert.AreEqual(job, staffMember.Job);
        }
        [TestMethod]
        [DataRow("12345")]
        [DataRow("A")]
        [DataRow("Джон")]
        [DataRow("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA")]
        public void ConstructorTest_incorrect_FirstName(string name)
        {
            //Arrange
            DateTime birthdate = new DateTime(1980, 1, 1);
            int currentCounter = StaffMember.Counter;
            Job job = Job.Concierge;
            double salary = 75;
            //Act + Assert
            Assert.ThrowsException<ArgumentException>(() => new StaffMember(name, "Smith", birthdate,job,salary));
            Assert.AreEqual(currentCounter, StaffMember.Counter);
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
            int currentCounter = StaffMember.Counter;
            Job job = Job.Concierge;
            double salary = 75;
            //Act + Assert
            Assert.ThrowsException<ArgumentException>(() => new StaffMember("John", name, birthdate, job, salary));
            Assert.AreEqual(currentCounter, StaffMember.Counter);
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
            int currentCounter = StaffMember.Counter;
            Job job = Job.Concierge;
            double salary = 75;
            //Act + Assert
            Assert.ThrowsException<ArgumentException>(() => new StaffMember(firstName, lastName, birthdate1, job, salary));
            Assert.ThrowsException<ArgumentException>(() => new StaffMember(firstName, lastName, birthdate2, job, salary));
            Assert.ThrowsException<ArgumentException>(() => new StaffMember(firstName, lastName, birthdate3, job, salary));
            Assert.AreEqual(currentCounter, StaffMember.Counter);
        }
        [TestMethod]
        public void ConstructorTest_Incorrect_Salary()
        {
            //Arrange
            string firstName = "John";
            string lastName = "Smith";
            DateTime birthdate = new DateTime(1980, 1, 1);
            int currentCounter = StaffMember.Counter;
            Job job = Job.Concierge;
            double salary1 = -75;
            double salary2 = 0;
            //Act + Assert
            Assert.ThrowsException<ArgumentException>(() => new StaffMember(firstName, lastName, birthdate, job, salary1));
            Assert.ThrowsException<ArgumentException>(() => new StaffMember(firstName, lastName, birthdate, job, salary2));
            Assert.AreEqual(currentCounter, StaffMember.Counter);
        }
        [TestMethod]
        public void ConstructorTest_Incorrect_Job()
        {
            //Arrange
            string firstName = "John";
            string lastName = "Smith";
            DateTime birthdate = new DateTime(1980, 1, 1);
            int currentCounter = StaffMember.Counter;
            Job job1 = (Job)25;
            Job job2 = (Job)(-25);
            double salary = 100;
            //Act + Assert
            Assert.ThrowsException<ArgumentException>(() => new StaffMember(firstName, lastName, birthdate, job1, salary));
            Assert.ThrowsException<ArgumentException>(() => new StaffMember(firstName, lastName, birthdate, job2, salary));
            Assert.AreEqual(currentCounter, StaffMember.Counter);
        }
        [TestMethod]
        public void JobTest_Correct()
        {
            //Arrange
            string firstName = "John";
            string lastName = "Smith";
            DateTime birthdate = new DateTime(1980, 1, 1);
            int currentCounter = StaffMember.Counter;
            Job job = Job.Concierge;
            double salary = 75;
            StaffMember staffMember = new StaffMember(firstName, lastName, birthdate, Job.Other, salary);
            //Act
            staffMember.Job = job;
            //Assert
            Assert.AreEqual(job, staffMember.Job);
        }
        [TestMethod]
        public void JobTest_Incorrect()
        {
            //Arrange
            string firstName = "John";
            string lastName = "Smith";
            DateTime birthdate = new DateTime(1980, 1, 1);
            int currentCounter = StaffMember.Counter;
            Job job = (Job)(-25);
            double salary = 75;
            StaffMember staffMember = new StaffMember(firstName, lastName, birthdate, Job.Other, salary);
            //Act + Assert
            Assert.ThrowsException<ArgumentException>(() => staffMember.Job = job);
        }
        [TestMethod]
        public void DailyRateTest_Correct()
        {
            //Arrange
            string firstName = "John";
            string lastName = "Smith";
            DateTime birthdate = new DateTime(1980, 1, 1);
            int currentCounter = StaffMember.Counter;
            Job job = Job.Concierge;
            double salary = 75;
            StaffMember staffMember = new StaffMember(firstName, lastName, birthdate, job, 25);
            //Act
            staffMember.DailyRate = salary;
            //Assert
            Assert.AreEqual(salary, staffMember.DailyRate);
        } 
        [TestMethod]
        public void DailyRateTest_Incorrect()
        {
            //Arrange
            string firstName = "John";
            string lastName = "Smith";
            DateTime birthdate = new DateTime(1980, 1, 1);
            int currentCounter = StaffMember.Counter;
            Job job = Job.Concierge;
            double salary = -75;
            StaffMember staffMember = new StaffMember(firstName, lastName, birthdate, job, 25);
            //Act + Assert
            Assert.ThrowsException<ArgumentException>(() => staffMember.DailyRate = salary);
        }
        [TestMethod]
        public void LastSalaryPay_Correct()
        {
            //Arrange
            string firstName = "John";
            string lastName = "Smith";
            DateTime birthdate = new DateTime(1980, 1, 1);
            int currentCounter = StaffMember.Counter;
            Job job = Job.Concierge;
            double salary = 75;
            StaffMember staffMember = new StaffMember(firstName, lastName, birthdate, job, salary);
            DateTime date = Clock.Now.AddDays(-10);
            //Act
            staffMember.LastSalaryPay = date;
            //Assert
            Assert.AreEqual(date, staffMember.LastSalaryPay);
        }
        [TestMethod]
        public void LastSalaryPay_Incorrect()
        {
            //Arrange
            string firstName = "John";
            string lastName = "Smith";
            DateTime birthdate = new DateTime(1980, 1, 1);
            int currentCounter = StaffMember.Counter;
            Job job = Job.Concierge;
            double salary = 75;
            StaffMember staffMember = new StaffMember(firstName, lastName, birthdate, job, salary);
            DateTime date = Clock.Now.AddDays(10);
            //Act + Assert
            Assert.ThrowsException<ArgumentException>(() => staffMember.LastSalaryPay = date);
        }
    }
}
