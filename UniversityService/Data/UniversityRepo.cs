using System;
using System.Collections.Generic;
using System.Linq;
using UniversityService.Models;

namespace UniversityService.Data
{
    public class UniversityRepo : IUniversityRepo
    {
        private readonly AppDbContext _context;

        public UniversityRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateUniversity(University University)
        {
            if(University == null)
            {
                throw new ArgumentNullException(nameof(University));
            }

            _context.Universitys.Add(University);
        }

        public IEnumerable<University> GetAllUniversitys()
        {
            return _context.Universitys.ToList();
        }

        public University GetUniversityById(int id)
        {
            return _context.Universitys.FirstOrDefault(p => p.Id == id)!;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}