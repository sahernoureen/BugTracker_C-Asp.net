using BugTracker.Models;
using BugTracker.Models.ProjectClasses;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;

namespace BugTracker.BL
{
    public class AdminLogic
    {
        public static ApplicationDbContext db = new ApplicationDbContext();
        public static UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        public static RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

        //DeleteUser
        public static bool DeleteUser(string userId)
        {
            if (userManager.FindById(userId) != null)
            {
                var user = db.Users.Find(userId);
                userManager.Delete(user);
                return true;
            }
            return false;
        }

        //GetAllRoles
        public static List<IdentityRole> GetAllRoles()
        {
            return db.Roles.Where(r => r.Name != "Admin").ToList();
        }
        public static ApplicationUser GetUserById(string userId)
        {
            return db.Users.FirstOrDefault(x => x.Id == userId);
        }
        public static List<ApplicationUser> GetAllUserExceptAdmin()
        {
            var adminRoleId = db.Roles.FirstOrDefault(r => r.Name == "Admin").Id;
            var adminRoleId = db.Roles.FirstOrDefault(r => r.Name == "Admin").Id;
            return db.Users.Where(u => !u.Roles.Any(r => r.RoleId == adminRoleId)).ToList();
        }
        public static List<ApplicationUser> GetAllUserExceptSubmitter()
        {
            var adminRoleId = db.Roles.FirstOrDefault(r => r.Name == "Submitter").Id;
            return db.Users.Where(u => !u.Roles.Any(r => r.RoleId == adminRoleId)).ToList();
        }
        //ADD ROLE TO USER
        public static bool AddUserToRole(string userId, string role)
        {
            if (CheckIfUserIsInRole(userId, role))
            {
                return false;
            }
            else
            {
                userManager.AddToRole(userId, role);
                return true;
            }
        }
        //REMOVE ROLE TO USER
        public static bool RemoveUserFromRole(string userId, string role)
        {
            if (!CheckIfUserIsInRole(userId, role))
            {
                return false;
            }
            else
            {
                userManager.RemoveFromRole(userId, role);
                return true;
            }
        }
        //ADD ROLE
        public static bool AddRole(string roleName)
        {
            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new IdentityRole { Name = roleName });
                return true;
            }
            return false;
        }

        public static List<UserInfoHolder> GetAllUsersInfo()
        {
            var users = db.Users.ToList();

            var userInfo = new List<UserInfoHolder>();

            foreach (var u in users)
            {
                var ui = new UserInfoHolder();
                ui.Id = u.Id;
                ui.Name = u.UserName;

                foreach (var r in u.Roles)
                {
                    var role = db.Roles.Find(r.RoleId);
                    ui.RolesInfo.Add(role);
                }
                userInfo.Add(ui);
            }
            return userInfo.Where(u => u.RolesInfo.All(r => r.Name != "Admin")).ToList();
        }

        //CHECKING
        public static bool CheckIfUserIsInRole(string userId, string role)
        {
            var result = userManager.IsInRole(userId, role);
            return result;
        }

        //GetRolesForUser
        public static List<IdentityRole> GetRolesForUser(string userId)
        {
            var user = userManager.FindById(userId);
            if (user != null)
            {
                if (user.Roles == null)
                {
                    return null;
                }
                else
                {
                    var roleIds = user.Roles.Select(r => r.RoleId).ToList();
                    var roles = new List<IdentityRole>();
                    foreach (var roleId in roleIds)
                    {
                        roles.Add(db.Roles.Find(roleId));
                    }
                    return roles;
                }

            }

            return null;
        }

        public static List<Ticket> GetRelatedTickets(string input)
        {
            var result = db.Tickets.Where(t => t.Title.Contains(input)).ToList();
            return result;
        }

        public static Ticket GetTicketByTitle(string title) 
        {
            var result = db.Tickets.FirstOrDefault(t=>t.Title == title);
            return result;
        }
    }
}