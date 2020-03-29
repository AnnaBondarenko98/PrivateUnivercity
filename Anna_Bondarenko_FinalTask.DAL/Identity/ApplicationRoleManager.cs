using Microsoft.AspNet.Identity;

namespace Anna_Bondarenko_FinalTask.DAL.Identity
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> store) : base(store)
        {

        }
    }
}
