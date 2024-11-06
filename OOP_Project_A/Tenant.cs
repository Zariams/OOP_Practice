using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_Practice
{
    public class Tenant
    {
        public string Name { get; set; }
        public DateTime BirthDate {  get; set; } 
        public Account Account { get; set; }

        public Tenant(string name, DateTime birthDate) {
            throw new NotImplementedException();
        }

    }
}