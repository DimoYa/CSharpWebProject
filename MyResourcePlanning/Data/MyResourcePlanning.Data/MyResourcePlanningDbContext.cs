namespace MyResourcePlanning.Data
{
    using System.Linq;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using MyResourcePlanning.Models;

    public class MyResourcePlanningDbContext : IdentityDbContext<User, UserRole, string>
    {
        public MyResourcePlanningDbContext(DbContextOptions<MyResourcePlanningDbContext> options)
            : base(options)
        {
        }

        public DbSet<Calendar> Calendars { get; set; }

        public DbSet<Request> Requests { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<Training> Trainings { get; set; }

        public DbSet<UserCalendar> UserCalendars { get; set; }

        public DbSet<UserSkill> UserSkills { get; set; }

        public DbSet<UserTraining> UserTrainings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ConfigureUserIdentityRelations(builder);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys()
                .Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));

            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<UserCalendar>()
                   .HasKey(uc => new
                   {
                       uc.UserId,
                       uc.CalendarId,
                   });

            builder.Entity<UserSkill>()
                   .HasKey(us => new
                   {
                       us.UserId,
                       us.SkillId,
                   });

            builder.Entity<UserTraining>()
                  .HasKey(ut => new
                  {
                      ut.UserId,
                      ut.TrainingId,
                  });
        }

        private static void ConfigureUserIdentityRelations(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
