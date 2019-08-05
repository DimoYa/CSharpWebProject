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
                    Roles = new List<IdentityUserRole<string>>()
                    {
                        new  IdentityUserRole<string> { RoleId = "111", UserId = "123" },
                        new  IdentityUserRole<string> { RoleId = "222", UserId = "123" }
                    },

                },
                new User
                {
                    Id = "124",
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
                }
            };
        }
    }
}
