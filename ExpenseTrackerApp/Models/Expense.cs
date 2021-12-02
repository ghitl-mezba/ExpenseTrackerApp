using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTrackerApp.Models
{
    public class Expense
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Expense category field is required")]
        public int ExpenseCategory { get; set; }

        [Required(ErrorMessage = "Expense date field is required")]
        public DateTime ExpenseDate { get; set; }

        [Required(ErrorMessage = "Expense amount field is required")]
        public decimal ExpenseAmount {get; set;}
    }
}
