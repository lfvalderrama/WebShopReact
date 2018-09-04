﻿using System;
using System.Collections.Generic;

namespace WebShop.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Order = new HashSet<Order>();
            ShoppingCartProducts = new HashSet<ShoppingCartProducts>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }

        public ICollection<Order> Order { get; set; }
        public ICollection<ShoppingCartProducts> ShoppingCartProducts { get; set; }
    }
}
