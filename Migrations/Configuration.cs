using BugTracker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace BugTracker.Migrations
{
    using BugTracker.Models.ProjectClasses;
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

            Project project1 = new Project(1,"Matrix",Priority.High);
            project1.Tickets = new List<Ticket>();
            project1.ProjectUsers = new List<ProjectUser>();

            var pjuser1 = new ProjectUser();

            pjuser1.Id = 1;
            pjuser1.ProjectId = 1;
            pjuser1.UserId = userManager.FindByName("Bob").Id;
            var pjuser2 = new ProjectUser();
            pjuser2.Id = 2;
            pjuser2.ProjectId = 1;
            pjuser2.UserId = userManager.FindByName("Amy").Id;
            project1.ProjectUsers.Add(pjuser1);
            project1.ProjectUsers.Add(pjuser2);

            Ticket ticket1 = new Ticket("find a bug","this page lost 404",DateTime.Now,1,1,1,1, userManager.FindByName("Bob").Id);
            Ticket ticket2 = new Ticket("this is a bug", "this page lost 404", DateTime.Now, 1, 2, 1, 1, userManager.FindByName("Amy").Id);

            project1.Tickets.Add(ticket1);
            project1.Tickets.Add(ticket2);
            db.Projects.Add(project1);
            db.Tickets.Add(ticket1);
            db.Tickets.Add(ticket2);

            db.SaveChanges();
        }
    }
}
