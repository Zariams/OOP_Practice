using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_Practice
{
    public class Account : IBankAccount
    {
        static double overdraftPercentage = 0;
        public double Balance { get; set; }
        public AccountState State { get; set; }


        public Account() { 
            throw new NotImplementedException(); 
        }
        public bool Withdraw(double number)
        {
            throw new NotImplementedException();    
        }
        public void Deposit(double number) 
        { 
            throw new NotImplementedException(); 
        }
    }
}