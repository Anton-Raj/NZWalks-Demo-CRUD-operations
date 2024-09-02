using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions):base(dbContextOptions) 
        {

        }
        //This will create the table according to the model in the database
        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }  

        public DbSet<Walk> Walks { get; set; }

        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed the Data for Difficulties
            //Easy , medium, hard

            var difficulties = new List<Difficulty>()
            {
                new Difficulty
                {
                    Id = Guid.Parse("0949c0b4-e8b4-414b-a301-2a1c8130fd51"),
                    Name = "Easy"
                },
                new Difficulty
                {
                    Id = Guid.Parse("b0ec6810-13e3-49de-83ca-aae3e9606549"),
                    Name = "Medium"
                },
                new Difficulty
                {
                    Id = Guid.Parse("5aa3a4af-7e09-482f-b8b5-d180b7a50826"),
                    Name = "Hard"
                }

            };

            //Seed difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);
        }
    }
}
