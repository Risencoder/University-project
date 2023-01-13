using System.Collections.Generic;
using CommandsService.Models;

namespace CommandsService.Data
{
    public interface ICommandRepo
    {
        bool SaveChanges();

        // Universitys
        IEnumerable<University> GetAllUniversitys();
        void CreateUniversity(University Univ);
        bool PlaformExits(int UniversityId);
        bool ExternalUniversityExists(int externalUniversityId);

        // Commands
        IEnumerable<Command> GetCommandsForUniversity(int UniversityId);
        Command GetCommand(int UniversityId, int commandId);
        void CreateCommand(int UniversityId, Command command);
    }
}