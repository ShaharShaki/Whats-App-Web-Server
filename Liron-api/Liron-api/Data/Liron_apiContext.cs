using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Liron_api.Models;

namespace Liron_api.Data
{
    public class Liron_apiContext : DbContext
    {
        public Liron_apiContext (DbContextOptions<Liron_apiContext> options)
            : base(options)
        {
        }


        public DbSet<Liron_api.Models.Message>? Message { get; set; }

        public DbSet<Liron_api.Models.User>? User { get; set; }

        public DbSet<Liron_api.Models.Conversation>? Conversation { get; set; }
    }
}
