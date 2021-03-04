using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Hospital_web.Models;

namespace Hospital_web.Data
{
    public class Hospital_webDatabase : DbContext
    {
        public Hospital_webDatabase (DbContextOptions<Hospital_webDatabase> options)
            : base(options)
        {
        }

        public DbSet<Hospital_web.Models.Companies> Companies { get; set; }

        public DbSet<Hospital_web.Models.Recievers> Recievers { get; set; }

        public DbSet<Hospital_web.Models.Senders> Senders { get; set; }

        public DbSet<Hospital_web.Models.Trackings> Trackings { get; set; }

        public DbSet<Hospital_web.Models.Parcels> Parcels { get; set; }
    }
}
