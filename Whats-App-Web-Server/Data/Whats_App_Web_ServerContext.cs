#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Whats_App_Web_Server.Models;

namespace Whats_App_Web_Server.Data
{
    public class Whats_App_Web_ServerContext : DbContext
    {
        public Whats_App_Web_ServerContext (DbContextOptions<Whats_App_Web_ServerContext> options)
            : base(options)
        {
        }

        public DbSet<Whats_App_Web_Server.Models.User> User { get; set; }
    }
}
