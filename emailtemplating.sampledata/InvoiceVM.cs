using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplating.SampleData
{
    public class InvoiceVM: Invoice
    {
        public Employee Employee { get; private set; }
        public Customer Customer { get; set; }

        public InvoiceVM()
            : base() { }

        public InvoiceVM(Employee employee, Customer customer)
            : base() 
        {
            this.Employee = employee;
            this.EmployeeID = employee == null ? 0 : employee.ID;
            this.Customer = customer;
            this.CustomerID = customer == null ? 0 : customer.ID;
        }
        
    }
}
