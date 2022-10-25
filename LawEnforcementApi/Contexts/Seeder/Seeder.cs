using LawEnforcementApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace LawEnforcementApi.Contexts.Seeder
{
    public static class Seeder
    {
        public static void SeedDatabase(this ModelBuilder builder)
        {
            SeedOfficers(builder);
            SeedRanks(builder);
        }

        private static void SeedOfficers(ModelBuilder builder)
        {
            List<Officer> officers = new()
            {
                new Officer()
                {
                    Id = new Guid("01fde555-3de6-4ad2-ab32-ac725a6391e9"),
                    OfficerRankId = new Guid("8960db50-77bd-4ea1-9038-952eb09a4eb5"),
                    CallSign = "Tango-4",
                },
                new Officer()
                {
                    Id = new Guid("098603a4-895a-4b1e-85ee-bb83ed801c0a"),
                    OfficerRankId = new Guid("1685cae0-71e4-4e76-83a9-5c9488b3942f"),
                    CallSign = "Echo-1",
                },
                new Officer()
                {
                    Id = new Guid("b37e6518-d42b-4809-b848-c1501cf5af80"),
                    OfficerRankId = new Guid("ebb26b52-b7b9-42a2-8687-b06d1b2011ee"),
                    CallSign = "Charlie-10",
                }
            };
            builder.Entity<Officer>().HasData(officers);
        }

        private static void SeedRanks(ModelBuilder builder)
        {
            List<Rank> ranks = new()
            {
                new Rank()
                {
                    Id = new Guid("1685cae0-71e4-4e76-83a9-5c9488b3942f"),
                    Name = "Lieutenant"
                },
                new Rank()
                {
                    Id = new Guid("8960db50-77bd-4ea1-9038-952eb09a4eb5"),
                    Name = "Sergeant"
                },
                new Rank()
                {
                    Id = new Guid("ebb26b52-b7b9-42a2-8687-b06d1b2011ee"),
                    Name = "Deputy"
                },
            };
            builder.Entity<Rank>().HasData(ranks);
        }
    }
}
