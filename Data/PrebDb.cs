namespace InspectionService.Data
{
    public static class PrebDb
    {
        public static void PrebPopulation(WebApplication app)
        {
            using (var DbScope = app.Services.CreateScope())
            {
                SeedData(DbScope.ServiceProvider.GetRequiredService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            Console.WriteLine("---> Populate Random Data");
            if (!context.InspectionTypes.Any())
            {
                Console.WriteLine("---- Seeding Data ----");
                context.InspectionTypes.AddRange(
                    new Models.InspectionType() { Name = "Basement" },
                    new Models.InspectionType() { Name = "Mezzanine" },
                    new Models.InspectionType() { Name = "First Floor" },
                    new Models.InspectionType() { Name = "Second Floor" }
                );
                int res = context.SaveChanges();

                if (res > 0)
                {

                    context.Inspections.AddRange(
                        new Models.Inspection() { Status = "UnSat", Comments = "Ahmed sit on Basement", InspectionTypeId = 1 },
                        new Models.Inspection() { Status = "Sat", Comments = "Mahmoud sit on First Floor", InspectionTypeId = 3 }
                    );
                    context.SaveChanges();
                }
                
                Console.WriteLine("---- Seeding Users ----");
                context.Users.AddRange(
                    new Models.User() { Username = "mahmoud", PasswordHash = BCrypt.Net.BCrypt.HashPassword("1234"), DisplayName = "Mahmoud Afify" }
                );
                context.SaveChanges();

                Console.WriteLine("---> Finish Seeding Data");
            }
            else
                Console.WriteLine("---- We already have data ----");
        }
    }
}