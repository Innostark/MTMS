using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using EmailTemplating.Models;
using EmailTemplating.Process;

namespace EmailTemplating.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        #region >> Builders (returns sample data) <<

        public Message BuildMessage()
        {
            //sample message
            return new Message()
            {
                From = new MessageAddress() { Address = "me@domain.com", DisplayName = "Mary Doe" },
                Recipients = new List<Recipient>() {
                    new Recipient() { Address = "one@domain.org", DisplayName = "One" },
                    new Recipient() { Address = "two@domain.org", DisplayName = "Two" },
                    new Recipient() { Address = "three@domain.org", DisplayName = "Three" },
                    new Recipient() { Address = "four@domain.org", DisplayName = "Four" }
                },
                Subject = "Testing",
                TemplateID = 1,
                Template = new Template()
                {
                    ID = 1,
                    Name = "Testing Template",
                    Description = "This is just a test",
                    Body = "Dear *|first_name|* *|last_name|*, Your birthday is on *|birthday|* - bye, MARY",
                    TagMapID = 10,
                    TagMap = new MergeVarMap()
                    {
                        ID = 10,
                        Name = "Test Map",
                        MapItems = new List<MergeVarMapItem>()
                        {
                            new MergeVarMapItem() { VariableName = "first_name", PropertyName = "FirstName" },
                            new MergeVarMapItem() { VariableName = "last_name", PropertyName = "LastName" },
                            new MergeVarMapItem() { VariableName = "birthday", PropertyName = "DOB" },
                        }
                    }
                }
            };
        }

        public List<Employee> BuildEmployees()
        {
            return new List<Employee>()
            {
                new Employee() { ID = 0, FirstName = "Zero", LastName = "Bonus", DOB = new DateTime(2000, 10, 1), Email = "zero@domin.org" },
                new Employee() { ID = 1, FirstName = "One", LastName = "Jones", DOB = new DateTime(2001, 10, 1), Email = "one@domain.org" },
                new Employee() { ID = 2, FirstName = "Two", LastName = "Roberts", Email = "two@domain.org" },
                new Employee() { ID = 3, FirstName = "Three", LastName = "Williamson", DOB = new DateTime(2000, 10, 3), Email = "three@domain.org" },
                new Employee() { ID = 4, FirstName = "Four", LastName = "Smith", Email = "four@domain.org" }
            };
        }


        public Employee MatchEmployee(Recipient recipient, List<Employee> employees)
        {
            return employees.SingleOrDefault(m => string.Equals(m.Email, recipient.Address, StringComparison.CurrentCultureIgnoreCase));
        }

        #endregion



        #region >> Test Builders <<

        [TestMethod]
        public void ValidateSampleEmployees()
        {
            var employees = BuildEmployees();
            Assert.IsNotNull(employees);
            Assert.IsTrue(employees.Count > 0);
            foreach (var item in employees)
            {
                Assert.IsTrue(item.Email.ToLower().StartsWith(item.FirstName.ToLower()));
            }

        }


        [TestMethod]
        public void ValidateSampleMessage()
        {
            var message = BuildMessage();
            Assert.IsNotNull(message);
            Assert.IsNotNull(message.From);
            Assert.IsNotNull(message.Recipients);
            Assert.IsNotNull(message.Template);
            Assert.IsNotNull(message.Template.TagMap);

            Assert.IsTrue(message.Recipients.Count > 0);
            Assert.IsTrue(message.Template.TagMap.MapItems.Count > 0);
        }


        [TestMethod]
        public void ValidateSampleRecipientMatcher()
        {
            var message = BuildMessage();
            Assert.IsNotNull(message);
            var employees = BuildEmployees();
            Assert.IsNotNull(employees);

            foreach (var recipient in message.Recipients)
            {
                var employee = MatchEmployee(recipient, employees);
                Assert.IsNotNull(employee);
                Assert.AreEqual(recipient.Address.ToLower(), employee.Email.ToLower());
                Assert.AreEqual(recipient.DisplayName, employee.FirstName);
            }
        }

        #endregion


        #region >> Test Process Extensions <<

        [TestMethod]
        public void TestMessageProcessExtension()
        {
            var message = BuildMessage();
            Assert.IsNotNull(message);
            var employees = BuildEmployees();
            Assert.IsNotNull(employees);

            message.ProcessMessageRecipients<Employee>(employees, MatchEmployee);
            foreach (var recipient in message.Recipients)
            {
                var employee = MatchEmployee(recipient, employees);
                Assert.IsNotNull(employee);
                Assert.AreEqual(recipient.Address.ToLower(), employee.Email.ToLower());
                Assert.AreEqual(employee.FirstName, recipient.MergeTags.Single(m => m.Name == "first_name").Value);
                Assert.AreEqual(employee.LastName, recipient.MergeTags.Single(m => m.Name == "last_name").Value);
                if (employee.DOB == null)
                {
                    //no DOB
                    Assert.IsNull(recipient.MergeTags.Single(m => m.Name == "birthday").Value);
                }
                else
                {
                    Assert.AreEqual(employee.DOB.ToString(), recipient.MergeTags.Single(m => m.Name == "birthday").Value);
                }
            }

        }

        #endregion


        #region >> Test Smtp Debugger <<

        [TestMethod]
        public void TestSmtpDebugger()
        {
            var message = BuildMessage();
            Assert.IsNotNull(message);
            var employees = BuildEmployees();
            Assert.IsNotNull(employees);

            message.ProcessMessageRecipients<Employee>(employees, MatchEmployee);

            var stmp = new SmtpMailDebuggingClient(Environment.CurrentDirectory);      //output in the bin/Debug folder
            var path = stmp.Send(message);

            Assert.IsTrue(System.IO.Directory.Exists(path));

            var files = System.IO.Directory.GetFiles(path, "*.txt");
            Assert.IsTrue(files.Length >= message.Recipients.Count);    //allows for multiple runs of the same test


        }

        #endregion


        public class Employee
        {
            public int ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Nullable<DateTime> DOB { get; set; }
            public string Email { get; set; }
        }
    }
}
