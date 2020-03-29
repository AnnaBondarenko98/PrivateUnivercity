using System.Data.Entity;
using Anna_Bondarenko_FinalTask.DAL.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Anna_Bondarenko_FinalTask.DAL.EF
{
    /// <summary>
    /// Database initializer
    /// </summary>
    internal class DbInitializer : CreateDatabaseIfNotExists<CommitteeContext>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db">Database Context</param>
        protected override void Seed(CommitteeContext db)
        {
            var userManager = new ApplicationUserManager(new UserStore<IdentityUser>(db));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var role1 = new IdentityRole
            {
                Name = "User"
            };

            var role2 = new IdentityRole
            {
                Name = "Admin"
            };

            var role3 = new IdentityRole
            {
                Name = "Operator"
            };

            roleManager.Create(role1);

            roleManager.Create(role2);

            roleManager.Create(role3);

            var user = new IdentityUser { Email = "admin@admin.ua", UserName = "admin@admin.ua" };
            var result = userManager.Create(user, "Qwerty12");

            if (result.Succeeded)
            {
                userManager.AddToRole(user.Id, "Admin");
            }

            base.Seed(db);
        }
    }
}
