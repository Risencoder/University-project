using System.Collections.Generic;
using CommandsService.Models;

namespace CommandsService.SyncDataServices.Grpc
{
    public interface IUniversityDataClient
    {
        IEnumerable<University> ReturnAllUniversitys();
    }
}