using System;
using System.Collections.Generic;
using CommandsService.Models;
using CommandsService.SyncDataServices.Grpc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CommandsService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var grpcClient = serviceScope.ServiceProvider.GetService<IUniversityDataClient>()!;

                var university = grpcClient.ReturnAllUniversitys();

                SeedData(serviceScope.ServiceProvider.GetService<ICommandRepo>()!, university);
            }
        }
        
        private static void SeedData(ICommandRepo repo, IEnumerable<University> university)
        {
            Console.WriteLine("Seeding new university...");

            foreach (var plat in university)
            {
                if(!repo.ExternalUniversityExists(plat.ExternalID))
                {
                    repo.CreateUniversity(plat);
                }
                repo.SaveChanges();
            }
        }
    }
}