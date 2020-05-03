using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MastiTimes.Models;

namespace MastiTimes.Data
{
    public class MastiTimesContext : DbContext
    {
        public MastiTimesContext (DbContextOptions<MastiTimesContext> options)
            : base(options)
        {
        }

        public DbSet<MastiTimes.Models.Movie> Movie { get; set; }

        public DbSet<MastiTimes.Models.Theater> Theater { get; set; }
    }
}
