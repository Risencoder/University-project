using System;
using System.Collections.Generic;
using System.Linq;
using CommandsService.Models;

namespace CommandsService.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext _context;

        public CommandRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateCommand(int UniversityId, Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            command.UniversityId = UniversityId;
            _context.Commands.Add(command);
        }

        public void CreateUniversity(University Univ)
        {
            if(Univ == null)
            {
                throw new ArgumentNullException(nameof(Univ));
            }
            _context.Universitys.Add(Univ);
        }

        public bool ExternalUniversityExists(int externalUniversityId)
        {
            return _context.Universitys.Any(p => p.ExternalID == externalUniversityId);
        }

        public IEnumerable<University> GetAllUniversitys()
        {
            return _context.Universitys.ToList();
        }

        public Command GetCommand(int UniversityId, int commandId)
        {
            return _context.Commands
                .Where(c => c.UniversityId == UniversityId && c.Id == commandId).FirstOrDefault()!;
        }

        public IEnumerable<Command> GetCommandsForUniversity(int UniversityId)
        {
            return _context.Commands
                .Where(c => c.UniversityId == UniversityId)
                .OrderBy(c => c.University.Name);
        }

        public bool PlaformExits(int UniversityId)
        {
            return _context.Universitys.Any(p => p.Id == UniversityId);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}