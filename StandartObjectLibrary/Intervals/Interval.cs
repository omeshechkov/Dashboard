using System;
using System.Collections.Generic;
using System.Text;

namespace StandartObjectLibrary
{
    public class Interval
    {
        private double value;
        public double Value
        {
            get { return value; }
            set { this.value = value; }
        }


        public Interval() : this(0) { }

        public Interval(double value)
        {
            this.value = value;
        }
    }
}
