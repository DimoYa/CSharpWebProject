namespace MyResourcePlanning.Tests.Common
{
    using Microsoft.AspNetCore.Identity;
    using MyResourcePlanning.Models;
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
                    ApproverId = "250"

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
    }
}
