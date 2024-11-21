using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP_Practice;
namespace TestProject
{
    [TestClass]
    public class TestAccount
    {

        [TestMethod]
        public void ConstructorTest_default()
        {
            //Arrange + Act
            Account account = new Account();
            //Assert
            Assert.AreEqual(0, account.Balance);
            Assert.AreEqual(account.State, AccountState.Active);
        }
        [TestMethod]
        public void DepositTest_correct()
        {
            //Arrange
            Account account = new Account();
            //Act
            account.Deposit(10);
            //Assert
            Assert.AreEqual(10, account.Balance);
        }
        [TestMethod]
        public void DepositTest_incorrect()
        {
            //Arrange
            Account account = new Account();
            //Act + Assert
            Assert.ThrowsException<ArgumentException>(() => account.Deposit(-10));
            Assert.ThrowsException<ArgumentException>(() => account.Deposit(0));
        }
        [TestMethod]
        public void WithdrawTest_correct()
        {
            //Arrange
            Account account = new Account();
            account.Deposit(10);
            //Act
            bool result = account.Withdraw(5);
            //Assert
            Assert.AreEqual(5, account.Balance);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void WithdrawTest_overdraft_correct()
        {
            //Arrange
            Account.OverdraftMax = 1;
            Account account = new Account();
            account.Deposit(10);
            //Act
            bool result =account.Withdraw(11);
            //Assert
            Assert.AreEqual(-1, account.Balance);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void WithdrawTest_argument_not_positive()
        {
            //Arrange
            Account account = new Account();
            account.Deposit(10);
            //Act + Assert
            Assert.ThrowsException<ArgumentException>(() => account.Withdraw(-10));
            Assert.ThrowsException<ArgumentException>(() => account.Withdraw(0));
        }
        [TestMethod]
        public void WithdrawTest_argument_too_big()
        {
            //Arrange
            Account account = new Account();
            account.Deposit(1);
            //Act
            bool result = account.Withdraw(100);
            //Assert
            Assert.IsFalse(result);
        }
    }
}
