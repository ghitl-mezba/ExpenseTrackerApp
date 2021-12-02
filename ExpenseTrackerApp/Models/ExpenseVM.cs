using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTrackerApp.Models
{
    public class ExpenseVM
    {
       
        public int ID { get; set; }
        public string CategoryName { get; set; }
        public DateTime ExpenseDate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal ExpenseAmount {get; set;}

        public ExpenseCategory expCatVM { get; set; }
        public Expense expVM { get; set; }
    }
}
