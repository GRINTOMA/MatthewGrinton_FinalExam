using System;
using System.Collections.Generic;
using System.Text;

namespace MatthewGrinton_FinalExam
{
    class Product
    {
        public Product(int n)
        {
            this.productNumber = n;
            this.description = "";
            this.productType = "";
            this.msrp = 0;
            this.onHand = 0;
        }
        public Product(int n, string d, string t, decimal m, int o)
        {
            this.productNumber = n;
            this.description = d;
            this.productType = t;
            this.msrp = m;
            this.onHand = o;
        }
        public int productNumber { get; set; }
        public string description { get; set; }
        public string productType { get; set; }
        public decimal msrp { get; set; }
        public int onHand { get; set; }
    }
}
