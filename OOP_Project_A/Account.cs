using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_Practice
{
    public class Account : IBankAccount
    {
        static double overdraft = 0;
        private AccountState state;
      
        public double Balance { get; private set; }
        public AccountState State
        {
            get
            {
                return state;
            }
            set
            {
                if (Enum.IsDefined(typeof(AccountState), value))
                {
                    state = value;
                }
                else
                {
                    throw new ArgumentException("Значення не відповідає жодному відомому стану рахунку!");
                }
            }
        }


        public Account() { 
            Balance = 0;
            State = AccountState.Active;
        }
        public bool Withdraw(double number)
        {
            if (number <= 0)
                throw new ArgumentException("Не можна зняти з рахунку від'ємне число!");
            if (State == AccountState.Inactive)
                throw new ArgumentException("Не можна зняти гроші з неактивного рахунку!");
            if (Balance >= number)
            {
                Balance -= number;
                return true;
            }
            else
                return false;
                
        }
        public void Deposit(double number) 
        {
            if (number <= 0)
                throw new ArgumentException("Не можна покласти на рахунок від'ємне число!");
            if (State == AccountState.Inactive)
                throw new ArgumentException("Не можна покласти гроші на неактивний рахунок!");
            Balance += number;
        }
    }
}