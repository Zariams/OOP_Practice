using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP_Practice;
namespace TestProject
{
    [TestClass]
    public class ClockTest
    {
        [TestMethod]
        public void Clock_Test()
        {
            //Arrange
            TimeSpan timeSpan = new TimeSpan(1, 0, 0, 0);
            //Act
            Clock.Offset = timeSpan;
            //Assert
            Assert.AreEqual(0, (Clock.Now-(DateTime.Now + timeSpan)).TotalSeconds,0.01);
            Clock.Offset = TimeSpan.Zero;
            Assert.AreEqual(0, (Clock.Now - DateTime.Now).TotalSeconds, 0.01);
        }
    }
}
