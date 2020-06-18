using BugTracker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace BugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BugTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BugTracker.Models.ApplicationDbContext context)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            List<string> names = new List<string>() { "Mike", "Anna", "John", "Jill", "Bob", "Amy" };
            List<string> email = new List<string>() { "mike@bg.com", "anna@bg.com", "john@bg.com", "jill@bg.com", "bob@bg.com", "amy@bg.com" };

            for (int i = 0; i < names.Count; i++)
            {
                var passwordHash = new PasswordHasher();
                string password = passwordHash.HashPassword("111111");

                db.Users.AddOrUpdate(x => x.Id,
                    new ApplicationUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = email[i],
                        EmailConfirmed = true,
                        PasswordHash = password,
                        SecurityStamp = Guid.NewGuid().ToString("D"),
                        PhoneNumber = null,
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEndDateUtc = null,
                        LockoutEnabled = false,
                        AccessFailedCount = 0,
                        UserName = names[i]
                    });
            }

            roleManager.Create(new IdentityRole { Name = "Admin" });
            roleManager.Create(new IdentityRole { Name = "Manager" });
            roleManager.Create(new IdentityRole { Name = "Developer" });
            roleManager.Create(new IdentityRole { Name = "Submitter" });

            userManager.AddToRole(db.Users.FirstOrDefault(n => n.UserName == "Mike")?.Id, "Admin");
            userManager.AddToRole(db.Users.FirstOrDefault(n => n.UserName == "Anna")?.Id, "Manager");
            userManager.AddToRole(db.Users.FirstOrDefault(n => n.UserName == "John")?.Id, "Developer");
            userManager.AddToRole(db.Users.FirstOrDefault(n => n.UserName == "Jill")?.Id, "Developer");
            userManager.AddToRole(db.Users.FirstOrDefault(n => n.UserName == "Bob")?.Id, "Submitter");
            userManager.AddToRole(db.Users.FirstOrDefault(n => n.UserName == "Amy")?.Id, "Submitter");

            db.SaveChanges();
        }
    }
}
