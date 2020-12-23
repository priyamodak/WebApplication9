using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication9.Models
{
    public class EmployeeViewModel
    {
       
            public int ID { get; set; }
            public string EmployeeName { get; set; }
            public string Client { get; set; }
            public Nullable<int> Amount { get; set; }
            public Nullable<System.DateTime> Date { get; set; }
            public string PurchasedItem { get; set; }
            public string Bill { get; set; }
        
    }
}