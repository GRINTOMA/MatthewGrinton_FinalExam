using System;
using System.Collections.Generic;
using System.Text;

namespace MatthewGrinton_FinalExam
{
    class Customer
    {
        public Customer(string last)
        {
            this.cFirstName = "";
            this.cLastName = last;
            this.city = "";
            this.creditLimit = 0;
        }
        public Customer(string fname, string lname, string c, decimal climit)
        {
            this.cFirstName = fname;
            this.cLastName = lname;
            this.city = c;
            this.creditLimit = climit;
        }
        public Customer(int cNum, string fname, string lname, string c, decimal climit)
        {
            this.cNumber = cNum;
            this.cFirstName = fname;
            this.cLastName = lname;
            this.city = c;
            this.creditLimit = climit;
        }
        public int cNumber { get; set; }
        public string cFirstName { get; set; }
        public string cLastName { get; set; }
        public string city { get; set; }
        public decimal creditLimit { get; set; }
    }
}
