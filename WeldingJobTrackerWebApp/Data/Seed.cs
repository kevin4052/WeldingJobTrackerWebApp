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

                if (!context.Clients.Any())
                {
                    context.Clients.AddRange(new List<Client>()
                    {
                        new Client()
                        {
                            Name = "Client1",
                            Address = new Address()
                            {
                                Street1 = "321 FM Rd 2222 ",
                                Street2 = "Apt 11306",
                                City = "Austin",
                                State = State.TX,
                                PostalCode = "78730"
                            }
                        },
                        new Client()
                        {
                            Name = "Client2",
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

            }
        }
    }
}
