using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace OOP_Practice
{
    public class Tenant
    {
        public static int Counter {  get; private set; }   
        public string Name { get; private set; }
        public DateTime BirthDate {  get; private set; } 
        public Account Account { get; private set; }
        public int ID { get; private set; }
        public Tenant(string name, DateTime birthDate) {
            throw new NotImplementedException();
        }

    }
}