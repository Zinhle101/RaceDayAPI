using Microsoft.EntityFrameworkCore;
using RaceDay.API.Models;
using RaceDay.Models;

namespace RaceDay.API.Data
{  // start of namespace
    public class RaceDayContext : DbContext 
    { // start of class
        public DbSet<User> Users { get; set; } // Models that wll be used in the database

        public DbSet<RaceEvent> RaceEvents { get; set; }

        public DbSet<RaceCategory> RaceCategories { get; set; }

        public DbSet<RaceEntry> RaceEntries { get; set; }

        public DbSet<RaceResult> RaceResults { get; set; }

        public DbSet<OrganiserProfile> OrganiserProfiles { get; set; }

        public DbSet<ParticipantProfile> ParticipantProfiles { get; set; }


        public RaceDayContext(DbContextOptions<RaceDayContext> options)  // class that communicates with the database
            : base(options)
        {

        }
    } // end of class
} // end of namespace
