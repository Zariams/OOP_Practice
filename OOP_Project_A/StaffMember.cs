using OOP_Practice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Practice
{
    public class StaffMember : Person//, IComparable<StaffMember>
    {
        public static int Counter { get; private set; }
        public override string FirstName { get; set; }
        public override string LastName { get; set; }
        public override DateTime BirthDate { get; set; }
        public Account Account { get; private set; }
        public int ID { get; private set; }
        public double DailyRate {  get ; set; }    
        public Job Job { get; set; }
        public bool IsFired { get; set; }
        public DateTime LastSalaryPay { get; set; }

        public StaffMember(string firstName,string lastName, DateTime birthDate, Job job, double salary)
        {
            throw new NotImplementedException();
        }
        /*public int CompareTo(StaffMember other)
        {
            throw new NotImplementedException();
        }*/
    }
}
