using IdentityProjectDB.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IdentityProjectDB.Startup))]
namespace IdentityProjectDB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateUserAndRoles();
        }

        public void CreateUserAndRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            //Revisar las dos siguientes lineas, no se ven completas en el video
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));

            if (!roleManager.RoleExists("SuperAdmin"))
            {
                //Crea el rol de superadmin
                var role = new IdentityRole("SuperAdmin");
                roleManager.Create(role);

                //Creacion de default user

                var user = new ApplicationUser();
                user.UserName = "info@equipomorillo.com";
                user.Email = "info@equipomorillo.com";
                string pwd = "P!ssw0rd";

                var newuser = userManager.Create(user,pwd);
                if (newuser.Succeeded)
                {
                    userManager.AddToRole(user.Id,"SuperAdmin");

                }

            }
        }
    }
}
