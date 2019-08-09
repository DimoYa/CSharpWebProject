namespace MyResourcePlanning.Tests.Common
{
    using Microsoft.AspNetCore.Identity;
    using MyResourcePlanning.Models;
    using MyResourcePlanning.Models.Enums;
    using System;
    using System.Collections.Generic;

    public static class DummyData
    {
        public static List<UserRole> GetDummyUserRoles()
        {
            return new List<UserRole>()
            {
                new UserRole("Resource")
                {
                    Id = "111"
                },
                new UserRole("Approver")
                {
                    Id = "222"
                }
            };
        }
        public static List<User> GetDummyUsers()
        {
            return new List<User>()
            {
                new User
                {
                    Id = "123",
                    FirstName = "FirstName",
                    LastName = "LastName",
                    IsDeleted = false,
                    LockoutEnabled = true,
                    Roles = new List<IdentityUserRole<string>>()
                    {
                        new  IdentityUserRole<string> { RoleId = "111", UserId = "123" },
                        new  IdentityUserRole<string> { RoleId = "222", UserId = "123" }
                    },
                    ApproverId = "250",

                },
                new User
                {
                    Id = "124",
                    FirstName = "Test",
                    LastName = "Test",
                    IsDeleted = false,
                    Roles = new List<IdentityUserRole<string>>()
                    {
                        new  IdentityUserRole<string> { RoleId = "111", UserId = "124" }
                    },
                },
                new User
                {
                    Id = "125",
                    IsDeleted = true,
                    Roles = new List<IdentityUserRole<string>>()
                    {
                        new  IdentityUserRole<string> { RoleId = "222", UserId = "125" }
                    },

                },
                new User
                {
                    Id = "126",
                    IsDeleted = false,
                    Roles = new List<IdentityUserRole<string>>()
                    {
                        new  IdentityUserRole<string> { RoleId = "222", UserId = "126" }
                    },
                },
                new User
                {
                    Id = "127",
                    IsDeleted = false,
                    Roles = new List<IdentityUserRole<string>>()
                    {
                        new  IdentityUserRole<string> { RoleId = "222", UserId = "127" }
                    },
                },
                new User
                {
                    Id = "128",
                    IsDeleted = true,
                    Roles = new List<IdentityUserRole<string>>()
                    {
                        new  IdentityUserRole<string> { RoleId = "111", UserId = "128" }
                    },
                },
                new User
                {
                    Id = "129",
                    FirstName = "Ivan",
                    LastName = "Ivanov",
                    IsDeleted = false,
                    Skills = new List<UserSkill>()
                    {
                        new  UserSkill { UserId = "129", SkillId = "1" },
                        new  UserSkill { UserId = "129", SkillId = "2" },
                        new  UserSkill { UserId = "129", SkillId = "3" },
                    },
                    Trainings = new List<UserTraining>()
                    {
                        new UserTraining { UserId = "129", TrainingId = "1"},
                        new UserTraining { UserId = "129", TrainingId = "2"},
                        new UserTraining { UserId = "129", TrainingId = "3"},
                    }
                },
                new User
                {
                    Id = "250",
                    FirstName = "Approver",
                    LastName = "Approver",
                    IsDeleted = false,
                    Roles = new List<IdentityUserRole<string>>()
                    {
                        new  IdentityUserRole<string> { RoleId = "222", UserId = "250" }
                    },
                },
            };
        }

        public static List<Skill> GetDummySkills()
        {
            return new List<Skill>()
            {
                new Skill()
                {
                    Id = "1",
                    Name = "Skill1",
                    IsDeleted = false,
                    SkillCategoryId = "10",
                },
                new Skill()
                {
                    Id = "2",
                    Name = "Skill2",
                    IsDeleted = false,
                    SkillCategoryId = "10",
                },
                new Skill()
                {
                    Id = "3",
                    Name = "Skill3",
                    IsDeleted = false,
                    SkillCategoryId = "11",
                },
                new Skill()
                {
                    Id = "4",
                    Name = "Skill4",
                    IsDeleted = true,
                    SkillCategoryId = "11",
                },
                new Skill()
                {
                    Id = "5",
                    Name = "Skill5",
                    IsDeleted = true,
                    SkillCategoryId = "13",
                }
            };
        }

        public static List<SkillCategory> GetDummySkillCategories()
        {
            return new List<SkillCategory>()
            {
                new SkillCategory()
                {
                    Id = "10",
                    Name = "Category1",
                    IsDeleted = false,
                },
                new SkillCategory()
                {
                    Id = "11",
                    Name = "Category2",
                    IsDeleted = false,
                },
                new SkillCategory()
                {
                    Id = "12",
                    Name = "Category3",
                    IsDeleted = true,
                },
                new SkillCategory()
                {
                    Id = "13",
                    Name = "Category4",
                    IsDeleted = true,
                }
            };
        }

        public static List<UserSkill> GetDummyUserSkills()
        {
            return new List<UserSkill>()
            {
                new UserSkill()
                {
                   UserId = "123",
                   SkillId = "1",
                },
               new UserSkill()
               {
                   UserId = "123",
                   SkillId = "2",
                   Level = SkillLevel.Beginning,
               },
               new UserSkill()
              {
                   UserId = "123",
                   SkillId = "4",
               },

                new UserSkill()
                {
                    UserId = "124",
                    SkillId = "1",
                },
                new UserSkill()
                {
                    UserId = "124",
                    SkillId = "2",
                },
                new UserSkill()
                {
                    UserId = "125",
                    SkillId = "1",
                },
                new UserSkill()
                {
                    UserId = "125",
                    SkillId = "3",
                },
            };
        }

        public static List<Training> GetDummyTrainings()
        {
            return new List<Training>()
            {
                new Training()
                {
                    Id = "1",
                    Name = "Training1",
                    IsDeleted = false,
                    DueDate = DateTime.Now.AddDays(30),
                },
                new Training()
                {
                    Id = "2",
                    Name = "Training2",
                    IsDeleted = false,
                    DueDate = DateTime.Now.AddDays(-1),
                },
                new Training()
                {
                    Id = "3",
                    Name = "Training3",
                    IsDeleted = false,
                    DueDate = DateTime.Now.AddDays(10),
                },
                new Training()
                {
                    Id = "4",
                    Name = "Training4",
                    IsDeleted = true,
                    DueDate = DateTime.Now.AddDays(5),
                },

            };
        }

        public static List<UserTraining> GetDummyUserTrainings()
        {
            return new List<UserTraining>()
            {
                new UserTraining()
                {
                   UserId = "123",
                   TrainingId = "1",
                },
                new UserTraining()
                {
                   UserId = "123",
                   TrainingId = "4",
                },
                new UserTraining()
                {
                   UserId = "123",
                   TrainingId = "3",
                },
                new UserTraining()
                {
                   UserId = "124",
                   TrainingId = "4",
                },
                new UserTraining()
                {
                   UserId = "128",
                   TrainingId = "1",
                   Status = UserTrainingStatus.Assigned,
                },
            };
        }

        public static List<Project> GetDummyProjects()
        {
            return new List<Project>()
            {
                new Project()
                {
                   Id = "1",
                   Name = "Project1",
                   RequestedHours = 100,
                   StartDate = DateTime.Now,
                   EndDate = DateTime.Now.AddDays(60),
                   IsDeleted = false,
                },
                new Project()
                {
                   Id = "2",
                   Name = "Project2",
                   RequestedHours = 60,
                   StartDate = DateTime.Now,
                   EndDate = DateTime.Now.AddDays(30),
                   IsDeleted = false,
                },
                new Project()
                {
                   Id = "3",
                   Name = "Project3",
                   RequestedHours = 60,
                   StartDate = DateTime.Now.AddDays(-30),
                   EndDate = DateTime.Now.AddDays(-1),
                   IsDeleted = true,
                },
                new Project()
                {
                   Id = "4",
                   Name = "Project4",
                   RequestedHours = 80,
                   StartDate = DateTime.Now.AddDays(2),
                   EndDate = DateTime.Now.AddDays(30),
                   IsDeleted = false,
                },
            };
        }
        public static List<Request> GetDummyRequests()
        {
            return new List<Request>()
            {
                new Request()
                {
                   Id = "1",
                   UserId = "123",
                   ProjectId = "1",
                   WorkingHours = 10,
                   StartDate = DateTime.Now.AddDays(5),
                   EndDate = DateTime.Now.AddDays(15),
                   IsDeleted = false,
                   Status = RequestStatus.InProgress,
                },
                new Request()
                {
                   Id = "2",
                   UserId = "124",
                   ProjectId = "1",
                   WorkingHours = 5,
                   StartDate = DateTime.Now.AddDays(-10),
                   EndDate = DateTime.Now.AddDays(15),
                   IsDeleted = false,
                },
                new Request()
                {
                   Id = "4",
                   UserId = "123",
                   ProjectId = "1",
                   WorkingHours = 10,
                   StartDate = DateTime.Now.AddDays(5),
                   EndDate = DateTime.Now.AddDays(15),
                },
                new Request()
                {
                   Id = "3",
                   UserId = "125",
                   ProjectId = "1",
                   WorkingHours = 10,
                   StartDate = DateTime.Now.AddDays(5),
                   EndDate = DateTime.Now.AddDays(15),
                   IsDeleted = false
                },
                new Request()
                {
                   Id = "5",
                   UserId = "128",
                   ProjectId = "1",
                   WorkingHours = 10,
                   StartDate = DateTime.Now.AddDays(-15),
                   EndDate = DateTime.Now,
                   IsDeleted = false,
                },
                new Request()
                {
                   Id = "6",
                   UserId = "126",
                   ProjectId = "1",
                   WorkingHours = 1,
                   StartDate = DateTime.Now.AddDays(5),
                   EndDate = DateTime.Now.AddDays(15),
                },
            };
        }

        public static List<Calendar> GetDummyCalendarDays()
        {
            return new List<Calendar>()
            {
                new Calendar()
                {
                   Day = DateTime.Now,
                   IsPublicHoliday = false,
                },
                 new Calendar()
                {
                   Day = DateTime.Now.AddDays(1),
                   IsPublicHoliday = false,
                },
                  new Calendar()
                {
                   Day = DateTime.Now.AddDays(2),
                   IsPublicHoliday = true,
                },
                   new Calendar()
                {
                   Day = DateTime.Now.AddDays(3),
                   IsPublicHoliday = false,
                },
            };
        }
    }
}
