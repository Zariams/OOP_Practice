﻿using OOP_Practice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Practice
{
    public class StaffMember : Person//, IComparable<StaffMember>
    {
        private string firstName;

        private string lastName;
        private DateTime birthDate;
        private double salary;
        private Job job;
        private DateTime lastSalary;
        public static int Counter { get; private set; } = 0;
        public override string FirstName
        {
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
                if (value > Clock.Now.AddYears(-18) || value < Clock.Now.AddYears(-120))
                    throw new ArgumentException($"Для реєстрації робітника готелю, йому має бути більше 18 та менше 120 років!(Введено:{Clock.Now.Year-value.Year})");
                birthDate = value;
            }
        }
        public Account Account { get; private set; }
        public override int ID { get; }
        public double DailyRate
        {
            get
            {
                return salary;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Заробітна плата не може бути від'ємною!");
                salary = value;
            }
        }    
        public Job Job
        {
            get
            {
                return job;
            }
            set
            {
                if (Enum.IsDefined(typeof(Job), value))
                {
                    job = value;
                }
                else
                {
                    throw new ArgumentException("Значення не відповідає жодній відомій професії!");
                }
            }
        }
        public bool IsFired { get; set; }
        public DateTime LastSalaryPay {
            get
            {
                return lastSalary;
            }
            set
            {
                if (value > Clock.Now) throw new ArgumentException("Дата останнього отримання заробітної плати не може бути у майбутньому!");
                lastSalary = value; 
            }
        }

        public StaffMember(string firstName,string lastName, DateTime birthDate, Job job, double salary)
        {
            FirstName = firstName;
            LastName = lastName;
            Job = job;
            BirthDate = birthDate;
            DailyRate = salary;
            Account = new Account();
            LastSalaryPay = Clock.Now;
            Counter++;
            ID = Counter;
        }
        public override string ToString()
        {
            return base.ToString() + $", {Job}";
        }
        /*public int CompareTo(StaffMember other)
        {
            throw new NotImplementedException();
        }*/
    }
}
