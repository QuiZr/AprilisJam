using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AprilisJam.Models;

namespace AprilisJam.Data
{
    public class AprilisJamRegistrationContext : DbContext
    {
        public AprilisJamRegistrationContext(DbContextOptions<AprilisJamRegistrationContext> options) : base(options) { }

        public DbSet<RegistrationForm> RegistrationForms { get; set; }
    }
}

