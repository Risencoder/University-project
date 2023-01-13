using System.Threading.Tasks;
using UniversityService.DTOs;

namespace UniversityService.SyncDataServices.Http
{
    public interface ICommandDataClient
    {
        Task SendUniversityToCommand(UniversityReadDto University); 
    }
}