using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_Practice
{
    public class Room : IComparable<Room>, ICloneable
    {
        private int dailyCost;
        private RoomType type;

        public static int Counter { get; private set; } = 0;
       // public Hotel Hotel { get; set; }
        public int ID { get; private set; }
        public RoomType Type {

            get
            {
                return type;
            }
            set
            {
                if (Enum.IsDefined(typeof(RoomType),value))
                {
                    type = value;
                }
                else
                {
                    throw new ArgumentException("Значення не відповідає жодному відомому типу кімнати!");
                }
            }

        }
        public int DailyCost {

            get
            {
                return dailyCost;
            }
            set
            {
                if (value > 0)
                {
                    dailyCost= value;
                }
                else
                {
                    throw new ArgumentException("Ціна на оренду кімнати не може бути від'ємною!");
                }
            } 
        
        }

        public static Func<Room,Room,int> Comparison;
        public static void ChangeComparisonFunction(Func<Room,Room,int> comparison)
        {
            Comparison = comparison;
        }
        public static void ChooseComparisonFunction(int choiceNumber)
        {
            switch (choiceNumber)
            {
                case 0:
                    ChangeComparisonFunction((room1, room2) => room1.DailyCost.CompareTo(room2.DailyCost));
                    break;
                case 1:
                    ChangeComparisonFunction((room1, room2) => room1.Type.CompareTo(room2.Type));
                    break;
                case 2:
                    ChangeComparisonFunction((room1, room2) => room1.ID.CompareTo(room2.ID));
                    break;
                default: throw new ArgumentException("Невірно заданий номер!");

            }
        }
        static Room()
        {
            Room.ChooseComparisonFunction(2);
        }
        public Room(int cost, RoomType roomType)
        {
            this.DailyCost = cost;
            this.Type = roomType;
            Counter++;
            this.ID = Counter;
            
        }
        public object Clone()
        {
           return new Room(this.DailyCost, this.Type);
        }
        public int CompareTo(Room other)
        {
            //return (this.DailyCost.CompareTo(other.DailyCost));
            if (Comparison is null)
                return 0;
            return (Comparison.Invoke(this,other));
        }

    }
}