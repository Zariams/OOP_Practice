using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Practice
{
    public static class Clock
    {
        public static TimeSpan Offset { get; set; } = TimeSpan.Zero;
        public static DateTime Now
        {
            get
            {
                return DateTime.Now.Add(Offset);
            }
        }

    }
}
