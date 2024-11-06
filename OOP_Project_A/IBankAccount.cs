using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_Practice
{
    public interface IBankAccount
    { 
        bool Withdraw(double number);
        void Deposit(double number);
        
    }
}