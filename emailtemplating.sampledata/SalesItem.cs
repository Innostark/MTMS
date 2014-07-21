using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplating.SampleData
{
    public class SalesItem
    {
        public int ID { get; set; }
        public int InvoiceID { get; set; }
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }

        public double Cost 
        {
            get
            {
                return (double)Quantity * UnitPrice;
            }
        }

    }
}
