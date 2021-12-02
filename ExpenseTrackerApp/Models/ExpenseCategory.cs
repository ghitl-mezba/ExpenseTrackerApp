using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTrackerApp.Models
{
    public class ExpenseCategory
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage ="Category name field is required")]
        [StringLength(maximumLength:20, ErrorMessage = "Category length must be between 3 and 100", MinimumLength = 3)]
        public string CategoryName { get; set; }

    }
}
