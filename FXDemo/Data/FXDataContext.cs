using Microsoft.EntityFrameworkCore;
using FXDemo.Models;

namespace FXDemo.Data
{
    public class FXDataContext : DbContext
    {

        // Define Propoeryes
        public DbSet<Player> Player { get; set; }
        public DbSet<Manager> Manager { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<Match> Match { get; set; }

        public DbSet<MatchPlayersHouse> MatchPlayersHouse { get; set; }
        public DbSet<MatchPlayersAway> MatchPlayersAway { get; set; }


        public FXDataContext(DbContextOptions<FXDataContext> options): base(options)
        {
            Database.EnsureCreated();
        }



        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Team>()
                .HasMany(c => c.Players)
                .WithOne(e => e.Team);

            base.OnModelCreating(modelBuilder);
        }



        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        */

        public DbSet<FXDemo.Models.Referee> Referee { get; set; }



        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        */

        public DbSet<FXDemo.Models.GeneralStatisiticResponce> GeneralStatisiticResponce { get; set; }



        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        */

    }
}
