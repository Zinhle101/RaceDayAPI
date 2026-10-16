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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // A user can organise multiple race events.
            modelBuilder.Entity<RaceEvent>()
                .HasOne(r => r.Organiser)
                .WithMany()
                .HasForeignKey(r => r.OrganiserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Each race category belongs to one race event.
            modelBuilder.Entity<RaceCategory>()
                .HasOne(c => c.RaceEvent)
                .WithMany()
                .HasForeignKey(c => c.RaceEventId)
                .OnDelete(DeleteBehavior.Cascade);

            // Each race entry belongs to one participant.
            modelBuilder.Entity<RaceEntry>()
                .HasOne(e => e.Participant)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Each race entry belongs to one category.
            modelBuilder.Entity<RaceEntry>()
                .HasOne(e => e.RaceCategory)
                .WithMany()
                .HasForeignKey(e => e.RaceCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Each result belongs to one race entry.
            modelBuilder.Entity<RaceResult>()
                .HasOne(r => r.RaceEntry)
                .WithMany()
                .HasForeignKey(r => r.RaceEntryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Each organiser profile belongs to one user.
            modelBuilder.Entity<OrganiserProfile>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Each participant profile belongs to one user.
            modelBuilder.Entity<ParticipantProfile>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }


        public RaceDayContext(DbContextOptions<RaceDayContext> options)  // class that communicates with the database
            : base(options)
        {

        }
    } // end of class
} // end of namespace
