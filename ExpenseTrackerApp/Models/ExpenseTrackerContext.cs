using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTrackerApp.Models
{
    public class ExpenseTrackerContext : DbContext
    {
        public ExpenseTrackerContext(DbContextOptions<ExpenseTrackerContext> options):base(options) { 
        
        }
        public DbSet<ExpenseCategory> ExpenseCategorys { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<User>()
                .HasData(
                    new User
                    {
                        ID = 1,
                        UserName = "admin",
                        UserPassword = "1234"
                    }
                );
           
            modelBuilder.Entity<ExpenseCategory>()
                .HasData(
                   new ExpenseCategory
                   {
                       ID = 1,
                       CategoryName = "House Rent"
                   },
                    new ExpenseCategory
                    {
                        ID = 2,
                        CategoryName = "Water Bill"
                    },
                    new ExpenseCategory
                    {
                        ID = 3,
                        CategoryName = "Electric Bill"
                    },
                    new ExpenseCategory
                    {
                        ID = 4,
                        CategoryName = "Groceries"
                    },
                    new ExpenseCategory
                    {
                        ID = 5,
                        CategoryName = "Uber"
                    },
                    new ExpenseCategory
                    {
                        ID = 6,
                        CategoryName = "Medications"
                    }
               );


            modelBuilder.Entity<Expense>()
                .HasData(
                    new Expense
                    {
                        ID = 1,
                        ExpenseCategory = 1,
                        ExpenseDate = DateTime.Today,
                        ExpenseAmount = 5000
                    },
                    new Expense
                    {
                        ID = 2,
                        ExpenseCategory = 2,
                        ExpenseDate = DateTime.Today,
                        ExpenseAmount = 500
                    },
                    new Expense
                    {
                        ID = 3,
                        ExpenseCategory = 3,
                        ExpenseDate = DateTime.Today,
                        ExpenseAmount = 901
                    },
                    new Expense
                    {
                        ID = 4,
                        ExpenseCategory = 4,
                        ExpenseDate = DateTime.Today,
                        ExpenseAmount = 620
                    },
                    new Expense
                    {
                        ID = 5,
                        ExpenseCategory = 5,
                        ExpenseDate = DateTime.Today,
                        ExpenseAmount = 870
                    },
                    new Expense
                    {
                        ID = 6,
                        ExpenseCategory = 6,
                        ExpenseDate = DateTime.Today,
                        ExpenseAmount = 2200
                    }
               );
        }
    }


}
