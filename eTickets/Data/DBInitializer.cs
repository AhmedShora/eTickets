using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Identity;

namespace eTickets.Data
{
    public class DBInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
            context.Database.EnsureCreated();

            if (!context.Cinemas.Any())
            {
                context.Cinemas.AddRange(new List<Cinema>()
                        {
                            new Cinema(){Name="c1",Description="C1 Des",Logo="Logo1"},
                            new Cinema(){Name="c2",Description="C2 Des",Logo="Logo2"}

                        });
                context.SaveChanges();

            }
            if (!context.Producers.Any())
            {
                context.Producers.AddRange(new List<Producer>()
                        {
                            new Producer(){FullName="P1",Bio="B1",ProfilePictureUrl="URL1",},
                            new Producer(){FullName="P2",Bio="B2",ProfilePictureUrl="URL2",},

                        });
                context.SaveChanges();

            }
            if (!context.Actors.Any())
            {
                context.Actors.AddRange(new List<Actor>()
                        {
                            new Actor(){FullName="A1",Bio="B1",ProfilePictureUrl="URL1",},
                            new Actor(){FullName="A2",Bio="B2",ProfilePictureUrl="URL2",},

                        });
                context.SaveChanges();

            }
            if (!context.Movies.Any())
            {
                context.Movies.AddRange(new List<Movie>()
                        {
                            new Movie(){Description="MD1",ImageUrl="img1",MovieCategory=MovieCategory.Action,Title="M1",CinemaId=1,EndDate=DateTime.Now.AddDays(7),Price=300,StartDate=DateTime.Now,ProducerId=1},
                            new Movie(){Description="MD2",ImageUrl="img2",MovieCategory=MovieCategory.Comedy,Title="M2",CinemaId=2,EndDate=DateTime.Now.AddDays(7),Price=350,StartDate=DateTime.Now,ProducerId=2},

                        });
                context.SaveChanges();

            }
            if (!context.ActorMovies.Any())
            {
                context.ActorMovies.AddRange(new List<ActorMovie>()
                        {
                           new ActorMovie(){ActorId=1,MovieId=1 },
                           new ActorMovie(){ActorId=1,MovieId=2 },
                           new ActorMovie(){ActorId=2,MovieId=1 },
                           new ActorMovie(){ActorId=2,MovieId=2 },

                        });
                context.SaveChanges();

            }

        }

        public static async Task SeedUsersAndRoles(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();

            //Roles
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            //Users
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            var adminUserEmail = "admin_1@admin.com";
            var adminUser =await userManager.FindByEmailAsync(adminUserEmail);
            if (adminUser == null)
            {
                var newAdminUser = new AppUser()
                {
                    FullName = "Admin User",
                    UserName = "Admin",
                    Email = adminUserEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(newAdminUser, "Admin.12345");
                await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
            }

            var appUserEmail = "user_1@user.com";
            var appUser = await userManager.FindByEmailAsync(appUserEmail);
            if (appUser == null)
            {
                var newAppUser = new AppUser()
                {
                    FullName = "App User",
                    UserName = "User",
                    Email = appUserEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(newAppUser, "User.12345");
                await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
            }

        }
    }
}
