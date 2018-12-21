using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capitec.Payroll.Models
{
    public class TimeKeeping
    {
        public int TimeKeepingId { get; }
        public DateTime Date { get; set; }
        public double HoursWorked { get; set; }
        public int EmployeeId { get; set; }
        public int JobGroupId { get; set; }
        public int ReportId { get; set; }
    }
}