using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP_Practice;
namespace TestProject
{
    [TestClass]
    public class TestHotelRoom
    {

        [TestMethod]
        public void ConstructorTest_Correct()
        {
            //Assert
            RoomType roomType = RoomType.Budget;
            int cost = 4;
            int currentCounter = HotelRoom.Counter;
            //Act
            HotelRoom room = new HotelRoom(cost,roomType);
            //Assert
            Assert.AreEqual(currentCounter+1,room.RoomID);
            Assert.AreEqual(roomType,room.Type);
            Assert.AreEqual(cost,room.DailyCost);
        }
        [TestMethod]
        [DataRow(10)]
        [DataRow(-10)]
        public void ConstructorTest_Inorrect_Type(int type)
        {
            //Assert
            
            RoomType roomType = (RoomType)type;
            int cost = 4;
            int currentCounter = HotelRoom.Counter;
            //Act + Assert
            Assert.ThrowsException<ArgumentException>(()=>new HotelRoom(cost,roomType));
            Assert.AreEqual(currentCounter,currentCounter);
        }
        [TestMethod]
        [DataRow(0)]
        [DataRow(-10)]
        public void ConstructorTest_Inorrect_Cost(int cost)
        {
            //Assert
            RoomType roomType = RoomType.Standard;
            int currentCounter = HotelRoom.Counter;
            //Act + Assert
            Assert.ThrowsException<ArgumentException>(() => new HotelRoom(cost, roomType));
            Assert.AreEqual(currentCounter, currentCounter);
        }
    }
}
