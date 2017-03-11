using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AprilisJam.Data
{
    public class GameJamContext : DbContext
    {
        public GameJamContext(DbContextOptions<GameJamContext> options) : base(options) { }

        public DbSet<UserApplication> UserApplications { get; set; }
    }
}

