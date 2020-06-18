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
            var adminRoleId = db.Roles.FirstOrDefault(r => r.Name == "Administrator").Id;
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
            return userInfo.Where(u => u.RolesInfo.All(r => r.Name != "Administrator")).ToList();
        }

        //CHECKING
        public static bool CheckIfUserIsInRole(string userId, string role)
        {
            var result = userManager.IsInRole(userId, role);
            return result;
        }
    }
}