using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capitec.Payroll.Models
{
    public class JobGroup
    {
        public int JobGroupId { get; }
        public string Code { get; set; }
        public decimal Rate { get; set; }
    }
}