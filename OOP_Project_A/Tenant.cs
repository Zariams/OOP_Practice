
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace OOP_Practice
{
    public class Tenant : Person
    {
        private string firstName;

        private string lastName;
        private DateTime birthDate;
        public static int Counter { get; private set; } = 0; 
        public override string FirstName {
            get
            {
                return firstName;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("Ім'я не може бути порожнім!");
                if (value.Length < 3 || value.Length > 12)
                    throw new ArgumentException($"Ім'я не може бути коротшим за 3 символи та довшим за 12 символів (Введено: {value.Length})!");

                foreach (char c in value)
                {
                    if ((c < 'a' || c > 'z') && (c < 'A' || c > 'Z'))
                        throw new ArgumentException("Ім'я містить непідтримувані символи!");
                }
                firstName = value;
            }
        }
        public override string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("Прізвище не може бути порожнім!");
                if (value.Length < 3 || value.Length > 12)
                    throw new ArgumentException($"Прізвище не може бути коротшим за 3 символи та довшим за 12 символів (Введено: {value.Length})!");

                foreach (char c in value)
                {
                    if ((c < 'a' || c > 'z') && (c < 'A' || c > 'Z'))
                        throw new ArgumentException("Прізвище містить непідтримувані символи!");
                }
                lastName = value;
            }
        }
        public override DateTime BirthDate
        {
            get
            {
                return birthDate;
            }
            set
            {
                if (value > Clock.Now.AddYears(-16) || value < Clock.Now.AddYears(-120))
                    throw new ArgumentException($"Для реєстрації жильця готелю, йому має бути більше 16 та менше 120 років!(Введено:{Clock.Now.Year - value.Year}");
                birthDate = value;
            }
        } 
        public Account Account { get; private set; }
        public override int ID { get; }
        public Tenant(string firstName, string lastName, DateTime birthDate) {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Account  = new Account();
            Counter++;
            ID = Counter;
           
        }
        public override string ToString()
        {
            return base.ToString() + ", Tenant";
        }
    }
}