using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace Anna_Bondarenko_FinalTask.DAL.Identity
{
    public class ApplicationUserManager : UserManager<IdentityUser>
    {
        public ApplicationUserManager(IUserStore<IdentityUser> store) : base(store)
        {
            var provider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("Providers");

            UserTokenProvider = new DataProtectorTokenProvider<IdentityUser>(provider.Create("Reset Password"));
        }
    }
}
