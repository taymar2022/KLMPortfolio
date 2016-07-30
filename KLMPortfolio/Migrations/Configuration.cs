namespace KLMPortfolio.Migrations
{ using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Models;
using System.Data.Entity.Migrations;
using System.Linq;

internal sealed class Configuration : DbMigrationsConfiguration<KLMPortfolio.Models.ApplicationDbContext>
{
    public Configuration()
    {
        AutomaticMigrationsEnabled = true;
    }

    protected override void Seed(KLMPortfolio.Models.ApplicationDbContext context)
    {
        var roleManager = new RoleManager<IdentityRole>(
        new RoleStore<IdentityRole>(context));
        if (!context.Roles.Any(r => r.Name == "Admin"))
        {
            roleManager.Create(new IdentityRole { Name = "Admin" });
        }

        if (!context.Roles.Any(r => r.Name == "Moderator"))
        {
            roleManager.Create(new IdentityRole { Name = "Moderator" });
        }
        var userManager = new UserManager<ApplicationUser>(
       new UserStore<ApplicationUser>(context));
        if (!context.Users.Any(u => u.Email == "karlyn194@aol.com"))
        {
            userManager.Create(new ApplicationUser
            {
                UserName = "karlyn194@aol.com",
                Email = "karlyn194@aol.com",
                FirstName = "Karen",
                LastName = "McKinney",
                DisplayName = "taymar2022"
            }, "Lktm@1998");
        }

        if (!context.Users.Any(u => u.Email == "jtwitchell@coderfoundry.com"))
        {
            userManager.Create(new ApplicationUser
            {
                UserName = "jtwichell@coderfoundry.com",
                Email = "jtwichell@coderfoundry.com",
                FirstName = "Jason",
                LastName = "Twichell",
                DisplayName = "J-Twich"
            }, "Abc&123!");
        }

        var userId = userManager.FindByEmail("karlyn194@aol.com").Id;
        userManager.AddToRole(userId, "Admin");

        var juserId = userManager.FindByEmail("jtwichell@coderfoundry.com").Id;
        userManager.AddToRole(juserId, "Moderator");

        //  This method will be called after migrating to the latest version.

        //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
        //  to avoid creating duplicate seed data. E.g.
        //
        //    context.People.AddOrUpdate(
        //      p => p.FullName,
        //      new Person { FullName = "Andrew Peters" },
        //      new Person { FullName = "Brice Lambson" },
        //      new Person { FullName = "Rowan Miller" }
        //    );
        //
    }
}
}
