
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace OOP_Practice
{
    public class Tenant : Person
    {
        public static int Counter {  get; private set; }   
        public override string FirstName { get; set; }
        public override string LastName { get; set; }
        public override DateTime BirthDate {  get; set; } 
        public Account Account { get; private set; }
        public int ID { get; private set; }
        public Tenant(string firstName, string lastName, DateTime birthDate) {
            throw new NotImplementedException();
        }
    }
}