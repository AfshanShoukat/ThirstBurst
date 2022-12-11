﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThirstBurst.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Logo { get; set; }
        [Required]
        public DateTime DateOfOrigin { get; set; }
        [Required]
        public string Address { get; set; }

        public List<Drink> Drinks { get; set; }
    }
}
