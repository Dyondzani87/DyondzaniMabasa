using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capitec.Payroll.Models
{
    public class EmployeeReport
    {
        public int ReportId { get; set; }
        public string PayPeriod { get; set; }
        public double AmountPaid { get; set; }
    }
}