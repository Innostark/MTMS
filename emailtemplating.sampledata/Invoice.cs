using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplating.SampleData
{
    public class Invoice
    {
        public int InvoiceID { get; set; }

        public int EmployeeID { get; set; }
        public int CustomerID { get; set; }
        public DateTime Date { get; set; }
        public List<SalesItem> Items { get; set; }

        public double Total 
        {
            get
            {
                return Items.Sum(m => m.Cost);
            }
        }

        public Invoice()
        {
            this.Items = new List<SalesItem>();
        }
    }
}
