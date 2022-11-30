using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ThirstBurst.Models;

namespace ThirstBurst.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ThirstBurst.Models.Drink> Drink { get; set; }
        public DbSet<ThirstBurst.Models.Company> Company { get; set; }
    }
}
