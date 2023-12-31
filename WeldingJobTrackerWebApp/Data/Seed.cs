﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeldingJobTrackerWebApp.Data.Enum;
using WeldingJobTrackerWebApp.Models;

namespace WeldingJobTrackerWebApp.Data
{
    public class Seed
    {
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

                var seedCompany = context.Companys.FirstOrDefault(c => c.Name == "Test LLC");

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                }
                if (!await roleManager.RoleExistsAsync(UserRoles.Welder))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Welder));
                }

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
                        Company = seedCompany
                    };

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
                        Company = seedCompany
                    };

                    await userManager.CreateAsync(newAppUser, "12QWas==");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Welder);
                }
            }
        }
    }
}
