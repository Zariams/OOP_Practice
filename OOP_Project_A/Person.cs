using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Practice
{
    public abstract class Person
    {
        public abstract string FirstName { get; set; }
        public abstract string LastName { get; set; }
        public abstract int ID { get; }    
        public abstract DateTime BirthDate { get; set; }

     
        public virtual int GetAge()
        {
            return Clock.Now.Month > BirthDate.Month? Clock.Now.Year-BirthDate.Year: Clock.Now.Year - BirthDate.Year-1;
        }
        public override string ToString()
        {
            return $"{FirstName} {LastName}, {BirthDate.ToShortDateString()}";
        }
    }
}
