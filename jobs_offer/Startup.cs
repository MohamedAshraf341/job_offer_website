using jobs_offer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(jobs_offer.Startup))]
namespace jobs_offer
{
    public partial class Startup
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefaultRolesAndUser();
        }
        public void CreateDefaultRolesAndUser()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role = new IdentityRole();
            if(!roleManager.RoleExists("Admin"))
            {
                role.Name = "Admin";
                roleManager.Create(role);
                ApplicationUser user = new ApplicationUser();
                user.UserName = "muhamed";
                user.Email = "moashraf20117@gmail.com";
                var check = userManager.Create(user, "Adm@123456789");
                if(check.Succeeded)
                {
                    userManager.AddToRole(user.Id,"Admin");
                }
            }

        }
    }
}
