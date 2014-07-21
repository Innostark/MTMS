using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplating.SampleData
{
    public class EmployeeSalesSummaryVM: Employee
    {
        public enum StatusEnum
        {
            above,
            average,
            below
        }

        public string Period { get; set; }
        public List<Invoice> Sales { get; set; }
        public double PeriodAverage { get; set; }
        public double PeriodVariance { get; set; }


        public double Total 
        {
            get
            {
                return this.Sales.Sum(m => m.Total);
            }
        }

        public StatusEnum Status 
        {
            get
            {
                var total = this.Total; //
                if (total > this.PeriodAverage + this.PeriodVariance)
                {
                    return StatusEnum.above;
                }
                else if (total < this.PeriodAverage - this.PeriodVariance)
                {
                    return StatusEnum.below;
                }
                else
                {
                    return StatusEnum.average;
                }
            }
        }
    }
}
