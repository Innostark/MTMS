using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmailTemplating.Models;

namespace EmailTemplating.SampleData
{
    public class DataSet
    {
        #region >> static datasets <<

        private static List<Customer> _customers;
        private static List<Employee> _employees;
        private static List<Invoice> _invoices;

        private static List<Template> _templates;

        #endregion

        public DataSet()
        {
            if (DataSet._customers == null || DataSet._employees == null || DataSet._invoices == null) { DataSet.InitializeDatasets(); }
        }


        public List<Customer> Customers 
        {
            get
            {
                return DataSet._customers;
            }
        }

        public List<Employee> Employees
        {
            get
            {
                return DataSet._employees;
            }
        }

        public List<Invoice> Invoices
        {
            get
            {
                return DataSet._invoices;
            }
        }

        public List<InvoiceVM> InvoicesViewModel
        {
            get
            {
                return DataSet._invoices
                    .Select(m => new InvoiceVM(
                            Employees.SingleOrDefault(e => e.ID == m.EmployeeID),
                            Customers.SingleOrDefault(c => c.ID == m.CustomerID)
                        ) { InvoiceID = m.InvoiceID, Date = m.Date, Items = m.Items })
                    .ToList();
            }
        }

        public List<EmployeeSalesSummaryVM> BuildEmployeeSalesSummaryViewModel(DateTime startDate, DateTime endDate, string period)
        {
            var sales = this.Invoices
                            .Where(m => m.Date >= startDate && m.Date <= endDate)
                            .GroupBy(m => m.EmployeeID)
                            .Select(m => new
                            {
                                ID = m.Key,
                                Total = m.Sum(s => s.Total)
                            }).ToList();
            var average = sales.Average(m => m.Total);
            var stddev = sales.Select(m => m.Total).StdDev();
            
                return this.Employees
                    .Select(e => new EmployeeSalesSummaryVM()
                    {
                        ID = e.ID,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        DOB = e.DOB,
                        Email = e.Email,
                        Period = period,
                        Sales = this.Invoices.Where(m => m.EmployeeID == e.ID && m.Date >= startDate && m.Date <= endDate).OrderBy(m => m.Date).ToList(),
                        PeriodAverage = average,
                        PeriodVariance = stddev
                    }).ToList();
        }

        public List<Template> Templates
        {
            get
            {
                return DataSet._templates;
            }
        }

        public List<MergeVarMap> MergeVarMaps
        {
            get
            {
                return this.Templates.Select(m => m.TagMap).Distinct().ToList();
            }
        }

        #region >> initialize dataset <<


        private const string COMPANY_DOMAIN = "abc_sales.com";
        private const int MAX_CUSTOMERS = 100;
        private const int MAX_EMPLOYEES = 20;
        private const int MAX_ORDERS_PER_CUSTOMER = 10;
        private const int MAX_ITEMS_PER_ORDER = 10;
        private const int MAX_QTY_PER_ITEM = 10;
        private const int MIN_ORDERS_PER_CUSTOMER = 1;
        private const int MIN_ITEMS_PER_ORDER = 2;
        private const int MIN_QTY_PER_ITEM = 1;



        private static Random _rnd = new Random(DateTime.Now.Millisecond);

        public static void InitializeDatasets()
        {
            _InitializeEmployees();
            _InitializeCustomers();
            _InitializeInvoices();
            _InitializeTemplates();
        }

        private static void _InitializeCustomers()
        {
            _customers = new List<Customer>(MAX_CUSTOMERS);
            var firstnames = _firstNames;
            var lastnames = _lastNames;
            var cities = _cities;
            var domains = _domains;
            for (int i = 0; i < MAX_CUSTOMERS; i++)
            {
                var customer = new Customer()
                {
                    ID = i * 1000 + 1,
                    FirstName = _NextRandom(firstnames),
                    LastName = _NextRandom(lastnames),
                    City = _NextRandom(cities)
                };
                customer.Email = _NameToEmail(customer.FirstName, customer.LastName, _NextRandom(domains));
                _customers.Add(customer);
            }
        }

        private static void _InitializeEmployees()
        {
            _employees = new List<Employee>(MAX_EMPLOYEES);
            var firstnames = _firstNames;
            var lastnames = _lastNames;
            var cities = _cities;
            for (int i = 0; i < MAX_EMPLOYEES; i++)
            {
                var employee = new Employee()
                {
                    ID = i + 1,
                    FirstName = _NextRandom(firstnames),
                    LastName = _NextRandom(lastnames),
                    DOB = new DateTime(_rnd.Next(1980,2000), _rnd.Next(1,13), _rnd.Next(1, 29))
                };
                employee.Email = _NameToEmail(employee.FirstName, employee.LastName, COMPANY_DOMAIN);
                _employees.Add(employee);
            }
        }

        private static void _InitializeInvoices()
        {
            //assumes customers has been initialized
            //assumes employees has been initialized

            var inventory = _Inventory;

            _invoices = new List<Invoice>();
            foreach (var customer in _customers)
            {
                for (int i = 0; i < _rnd.Next(MIN_ORDERS_PER_CUSTOMER, MAX_ORDERS_PER_CUSTOMER+1); i++)
                {
                    var order = new Invoice()
                    {
                        InvoiceID = _invoices.Count() + 1,
                        CustomerID = customer.ID,
                        EmployeeID = _employees[_rnd.Next(0,_employees.Count)].ID,
                        Date = DateTime.Now.AddDays(-1 * _rnd.Next(1,100))
                    };
                    _invoices.Add(order);
                    for (int x = 0; x < _rnd.Next(MIN_ITEMS_PER_ORDER, MAX_ITEMS_PER_ORDER+1); x++)
                    {
                        string key = inventory.Keys.ToList()[_rnd.Next(0, inventory.Count)];

                        order.Items.Add(new SalesItem()
                        {
                            ID = _invoices.Sum(m => m.Items.Count) + 1,
                            InvoiceID = order.InvoiceID,
                            ItemCode = key.Substring(0,3).ToUpper(),
                            Description = key,
                            Quantity = _rnd.Next(MIN_QTY_PER_ITEM, MAX_QTY_PER_ITEM),
                            UnitPrice = inventory[key]
                        });
                    }
                }
            }
        }


        // TEMPLATES .....
        private static void _InitializeTemplates()
        {
            _templates = new List<Template>();

            //some merge maps we will use 
            var mergemaps = new Dictionary<string, MergeVarMap>();
            mergemaps.Add("customers", new MergeVarMap()
            {
                ID = 101,
                Name = "Customers",
                MapItems = new List<MergeVarMapItem>()
                {
                    new MergeVarMapItem() { VariableName = "id", PropertyName = "ID" },
                    new MergeVarMapItem() { VariableName = "first_name", PropertyName = "FirstName" },
                    new MergeVarMapItem() { VariableName = "last_name", PropertyName = "LastName" },
                    new MergeVarMapItem() { VariableName = "email", PropertyName = "Email" },
                    new MergeVarMapItem() { VariableName = "city", PropertyName = "City" }
                }
            });
            mergemaps.Add("employees", new MergeVarMap()
            {
                ID = 102,
                Name = "Employees",
                MapItems = new List<MergeVarMapItem>()
                {
                    new MergeVarMapItem() { VariableName = "id", PropertyName = "ID" },
                    new MergeVarMapItem() { VariableName = "first_name", PropertyName = "FirstName" },
                    new MergeVarMapItem() { VariableName = "last_name", PropertyName = "LastName" },
                    new MergeVarMapItem() { VariableName = "email", PropertyName = "Email" },
                    new MergeVarMapItem() { VariableName = "birthday", PropertyName = "DOB" }
                }
            });
            mergemaps.Add("employees_sales_sum", new MergeVarMap()
            {
                ID = 102,
                Name = "Employees Sales Summary",
                MapItems = new List<MergeVarMapItem>()
                {
                    new MergeVarMapItem() { VariableName = "id", PropertyName = "ID" },
                    new MergeVarMapItem() { VariableName = "first_name", PropertyName = "FirstName" },
                    new MergeVarMapItem() { VariableName = "last_name", PropertyName = "LastName" },
                    new MergeVarMapItem() { VariableName = "email", PropertyName = "Email" },
                    new MergeVarMapItem() { VariableName = "birthday", PropertyName = "DOB" },
                    new MergeVarMapItem() { VariableName = "period", PropertyName = "Period" },
                    new MergeVarMapItem() { VariableName = "total", PropertyName = "Total" }
                }
            });

            //Password Reset
            _templates.Add(new Template()
            {
                ID = 1,
                Name = "Password Reset",
                Description = "Used for sending customers instructions on how to reset their password",
                Body = string.Format("Dear *|first_name|*, {0}{0}It looks like you need to reset your password.  Good news - just go to http://not_a_real_link.com and enter the following information...{0}email address: *|email|*{0}city: *|city|*{0}{0}Thank You!", Environment.NewLine),
                TagMapID = mergemaps["customers"].ID,
                TagMap = mergemaps["customers"]
            });

            //Promotion
            _templates.Add(new Template()
            {
                ID = 1,
                Name = "Promotion",
                Description = "Used for sending customers notice that we will be in their city for a special sale.",
                Body = string.Format("Dear *|first_name|*, {0}{0}Great News.  We will be in *|city|* this weekend and will be selling our most popular items for 50% off.  The action starts at 10am and the things go fast... So don't be late!{0}{0}See you there!", Environment.NewLine),
                TagMapID = mergemaps["customers"].ID,
                TagMap = mergemaps["customers"]
            });

            //Parking Violation
            _templates.Add(new Template()
            {
                ID = 1,
                Name = "Parking Violation",
                Description = "Used for sending employee(s) notice that they parked in the wrong spot.",
                Body = string.Format("Dear *|first_name|* *|last_name|*, {0}{0}Looks like you had trouble parking today.  We noticed that your car was in the wrong spot!{0}{0}Please be more careful in the future!", Environment.NewLine),
                TagMapID = mergemaps["employees"].ID,
                TagMap = mergemaps["employees"]
            });

            //High Sales
            _templates.Add(new Template()
            {
                ID = 1,
                Name = "High Sales",
                Description = "Used for sending employees notice that they had above average sales.",
                Body = string.Format("Hey *|first_name|*, {0}{0}Great News.  Your sale for *|period|* were $*|total|* which puts you amoung the best in the company.  As a token of our thanks, we are sending you on an all expenses paid trip to Paris (Texas).{0}{0}Hope you have fun!", Environment.NewLine),
                TagMapID = mergemaps["employees_sales_sum"].ID,
                TagMap = mergemaps["employees_sales_sum"]
            });



        }


        // HELPERS .....

        

        private static List<string> _firstNames
        {
            get
            {
                return new List<string>() { "Amy", "Ann", "Abe", "Bonne", "Billy", "Bob", "Chester", "Charlie", "Christy", "Dawn", "Dilbert", "Dodo", "Ed", "Elaine", "Ellen", "Fred", "Fanny", "Florence", "Greg", "Grant", "Gill", "Henry", "Horese", "Hanna", "Ignacio", "Julia", "Janis", "Jana", "Kelly", "Kim", "Kent", "Lance", "Larry", "Mo", "Manny", "Mia" };
            }
        }

        private static List<string> _lastNames
        {
            get
            {
                return new List<string>() { "Anderson", "Brown", "Chambers", "Donning", "Ellington", "Franks", "Granny", "Hamilton", "Illas", "Jackson", "Kennedy", "Longbottom", "Mona", "Nutbag", "Obtuse", "Pink", "Queen", "Roberts", "Simpson", "Taylor", "Ulum", "Ventura", "Wilson" };
            }
        }

        private static List<string> _domains
        {
            get
            {
                return new List<string>() { "gmail.com", "yahoo.com", "live.net", "espn.com", "epl.uk.co" };
            }
        }

        private static List<string> _cities
        {
            get
            {
                return new List<string>() { "Los Angeles", "San Diego", "Dana Point", "Seattle", "Santa Fe", "Jasper", "Tahoe City", "Reno", "Napa", "Park City" };
            }
        }

        private static string _NextRandom(List<string> items)
        {
            return items[_rnd.Next(0, items.Count)];
        }

        private static Dictionary<string, double> _Inventory
        {
            get
            {
                var ret = new Dictionary<string, double>();
                ret.Add("Allen Wrench", 2.93);
                ret.Add("Box", 33.24);
                ret.Add("Candle", 9.22);
                ret.Add("Door", 134.30);
                ret.Add("Eclair", 0.99);
                ret.Add("Fan", 34.88);
                ret.Add("Grabber", 3.55);
                ret.Add("Hinges", 1.99);
                ret.Add("Knobs", 3.13);
                ret.Add("Screens", 10.33);

                return ret;
            }
        }

        private static string _NameToEmail(string fname, string lname, string domain)
        {
            return string.Format("{0}-{1}@{2}", fname.Substring(0, 1), lname, domain).ToLower();
        }
        private static string _NameToEmail(string fname, string lname, List<string> possible_domains)
        {
            return _NameToEmail(fname, lname, possible_domains[_rnd.Next(0, possible_domains.Count)]);
        }


        #endregion
    }
}
