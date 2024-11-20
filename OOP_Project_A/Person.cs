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
        public abstract DateTime BirthDate { get; set; }

     
        public virtual int GetAge()
        {
            return DateTime.Now.Month > BirthDate.Month? DateTime.Now.Year-BirthDate.Year: DateTime.Now.Year - BirthDate.Year-1;
        }

    }
}
