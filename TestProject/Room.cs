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
            //Arrange
            RoomType roomType = RoomType.Budget;
            int cost = 4;
            int currentCounter = Room.Counter;
            //Act
            Room room = new Room(cost,roomType);
            //Assert
            Assert.AreEqual(currentCounter+1,room.ID);
            Assert.AreEqual(roomType,room.Type);
            Assert.AreEqual(cost,room.DailyCost);
        }
        [TestMethod]
        [DataRow(10)]
        [DataRow(-10)]
        public void ConstructorTest_Inorrect_Type(int type)
        {
            //Arrange

            RoomType roomType = (RoomType)type;
            int cost = 4;
            int currentCounter = Room.Counter;
            //Act + Assert
            Assert.ThrowsException<ArgumentException>(()=>new Room(cost,roomType));
            Assert.AreEqual(currentCounter,Room.Counter);
        }
        [TestMethod]
        [DataRow(0)]
        [DataRow(-10)]
        public void ConstructorTest_Inorrect_Cost(int cost)
        {
            //Arrange
            RoomType roomType = RoomType.Standard;
            int currentCounter = Room.Counter;
            //Act + Assert
            Assert.ThrowsException<ArgumentException>(() => new Room(cost, roomType));
            Assert.AreEqual(currentCounter, Room.Counter);
        }
        [TestMethod]
        public void CloneTest()
        {
            //Arrange
            RoomType roomType = RoomType.Budget;
            int cost = 4;
            int currentCounter = Room.Counter;
            Room room = new Room(cost, roomType);
            //Act
            Room? room2 = room.Clone() as Room;
            //Assert
            Assert.IsNotNull(room2);
            Assert.AreNotSame(room, room2);
            Assert.AreEqual(room.DailyCost, room2.DailyCost);
            Assert.AreEqual(room.Type, room2.Type);
            Assert.AreEqual(currentCounter + 2, Room.Counter);
        }
        [TestMethod]
        public void CompareToTest_Cost()
        {
            //Arrange
            Room.ChooseComparisonFunction(0);
            RoomType roomType = RoomType.Budget;
            int cost = 4;
            int currentCounter = Room.Counter;
            Room room1 = new Room(cost, roomType);
            Room room2 = room1.Clone() as Room;
            Room room3 = room1.Clone() as Room;
            room2.DailyCost = 20;
            //Act
            int result12 = room1.CompareTo(room2);
            int result21 = room2.CompareTo(room1);
            int result13 = room1.CompareTo(room3);
            int result31 = room3.CompareTo(room1);
            //Assert
            Assert.AreEqual(-1, result12);
            Assert.AreEqual(1, result21);
            Assert.AreEqual(0, result31);
            Assert.AreEqual(0, result13);
        }
        [TestMethod]
        public void CompareToTest_ID()
        {
            //Arrange
            Room.ChooseComparisonFunction(2);
            RoomType roomType = RoomType.Budget;
            int cost = 4;
            int currentCounter = Room.Counter;
            Room room1 = new Room(cost, roomType);
            Room room2 = room1.Clone() as Room;
            Room room3 = room1.Clone() as Room;
            //Act
            int result12 = room1.CompareTo(room2);
            int result21 = room2.CompareTo(room1);
            int result13 = room1.CompareTo(room3);
            int result31 = room3.CompareTo(room1);
            int result11 = room1.CompareTo(room1);
            //Assert
            Assert.AreEqual(-1, result12);
            Assert.AreEqual(1, result21);
            Assert.AreEqual(1, result31);
            Assert.AreEqual(-1, result13);
            Assert.AreEqual(0, result11);
        }
        [TestMethod]
        public void CompareToTest_Type()
        {
            //Arrange
            Room.ChooseComparisonFunction(1);
            RoomType roomType = RoomType.Budget;
            int cost = 4;
            int currentCounter = Room.Counter;
            Room room1 = new Room(cost, roomType);
            Room room2 = room1.Clone() as Room;
            Room room3 = room1.Clone() as Room;
            room2.Type = RoomType.Luxury;
            //Act
            int result12 = room1.CompareTo(room2);
            int result21 = room2.CompareTo(room1);
            int result13 = room1.CompareTo(room3);
            int result31 = room3.CompareTo(room1);
            //Assert
            Assert.AreEqual(-1, result12);
            Assert.AreEqual(1, result21);
            Assert.AreEqual(0, result31);
            Assert.AreEqual(0, result13);
        }
    }
}
