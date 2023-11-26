using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeldingJobTrackerWebApp.Data.Enum;
using WeldingJobTrackerWebApp.Models;

namespace WeldingJobTrackerWebApp.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.ProjectStatuses.Any())
                {
                    context.ProjectStatuses.AddRange(new List<ProjectStatus>()
                    {
                        new ProjectStatus()
                        {
                            Code = "Draft",
                            Name = "Draft",
                        },
                        new ProjectStatus()
                        {
                            Code = "Pending",
                            Name = "Pending",
                        },
                        new ProjectStatus()
                        {
                            Code = "Active",
                            Name = "Active",
                        },
                        new ProjectStatus()
                        {
                            Code = "OnHold",
                            Name = "OnHold",
                        },
                        new ProjectStatus()
                        {
                            Code = "InActive",
                            Name = "InActive",
                        },
                        new ProjectStatus()
                        {
                            Code = "Completed",
                            Name = "Completed",
                        },
                    });
                    context.SaveChanges();
                }

                if (!context.Clients.Any())
                {
                    context.Clients.AddRange(new List<Client>()
                    {
                        new Client()
                        {
                            Name = "Test Client1",
                            Address = new Address()
                            {
                                Street1 = "321 FM Rd 2222 ",
                                Street2 = "Apt 11306",
                                City = "Austin",
                                State = State.TX,
                                PostalCode = "78730"
                            },

                        },
                        new Client()
                        {
                            Name = "Test Client2",
                            Address = new Address()
                            {
                                Street1 = "321 Pike Place",
                                City = "Seattle",
                                State = State.WA,
                                PostalCode = "54321"
                            }
                        }
                    });
                    context.SaveChanges();
                }

            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                if (!context.Companys.Any())
                {
                    context.Companys.AddRange(new List<Company>()
                    {
                        new Company()
                        {
                            Name = "Test LLC"
                        }
                    });

                    await context.SaveChangesAsync();
                }

                var testCompany = await context.Companys.FirstOrDefaultAsync(c => c.Name == "Test LLC");

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.Welder))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Welder));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var adminUserEmail = "cobra40@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new User()
                    {
                        UserName = adminUserEmail,
                        FirstName = "kevin",
                        LastName = "Hernandez",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street1 = "123 Main St",
                            City = "Austin",
                            State = State.TX,
                            PostalCode = "33333"
                        },
                        Companies = new List<Company>()
                        {
                            new Company()
                            {
                                Id = 1,
                                Name = "Test LLC"
                            }
                        }
                    };

                    //newAdminUser.Companies.Add(testCompany);

                    await userManager.CreateAsync(newAdminUser, "12QWas==");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                var appUserEmail = "user@aws11.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new User()
                    {
                        UserName = appUserEmail,
                        FirstName = "Alex",
                        MiddleName = "Omar",
                        LastName = "Hernandez-Diaz",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street1 = "123 Main St",
                            City = "Charlotte",
                            State = State.AZ,
                            PostalCode = "33333"
                        },
                        Companies = new List<Company>()
                        {
                            new Company()
                            {
                                Id = 1,
                                Name = "Test LLC"
                            }
                        }
                    };
                    //newAppUser.Companies.Add(testCompany);

                    await userManager.CreateAsync(newAppUser, "12QWas==");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Welder);
                }
            }
        }
    }
}
