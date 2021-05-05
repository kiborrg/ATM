using System;
using System.Collections.Generic;
using System.Text;

namespace ATM
{
    public class Money
    {
        public int Nominal { get; set; }
        public int Count { get; set; }

        public Money(int nominal, int count)
        {
            Nominal = nominal;
            Count = count;
        }

        public long getSumm()
        {
            return Nominal * Count;
        }
    }
}
