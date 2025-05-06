using System;
using System.Collections.Generic;

namespace RestaurantSystem
{
    public static class SimpleDataStore
    {
        public static List<Employee> Employees = new List<Employee>()
        {
            new Employee { Username = "admin", Password = "admin123", Role = "Admin" },
            new Employee { Username = "chef1", Password = "chef123", Role = "Chef" },
            new Employee { Username = "waiter1", Password = "waiter123", Role = "Waiter" },
        };

        public static List<Customer> Customers = new List<Customer>()
        {
            // Pre-registered customer
            new Customer { Username = "customer1", Password = "cust123", Name = "John Doe", Phone = "1234567890", Address = "123 Street", DateOfBirth = new DateTime(1990, 1, 1) }
        };

        public static List<Order> Orders = new List<Order>();
    }

    public class Employee
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class Customer
    {
        public string Username { get; set; }
        public string Password { get; set; }
        // Additional customer details
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string MenuItem { get; set; }
        public string Status { get; set; } // "Pending", "Completed", "Served"
        public int PrepTime { get; set; }  // in minutes
    }
}
