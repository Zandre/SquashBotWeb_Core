using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SquashBotWebCore.Models;
using SquashBotWebCore.Models.SquashBot.Classes.TournamentClasses;

namespace SquashBotWebCore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Tournament> Tournament { get; set; }

        public DbSet<Section> Section { get; set; }

        public DbSet<Team> Team { get; set; }

        public DbSet<TournamentAdministrator> TournamentAdministrators { get; set; }

        public DbSet<Fixture> Fixtures { get; set; }

        public DbSet<SquashVenue> SquashVenues { get; set; }

        public DbSet<TournamentSquashVenue> TournamentSquashVenues { get; set; }     
    }
}
