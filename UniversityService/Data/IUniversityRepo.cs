using System.Collections.Generic;
using UniversityService.Models;

namespace UniversityService.Data
{
    public interface IUniversityRepo
    {
        bool SaveChanges();

        IEnumerable<University> GetAllUniversitys();
        University GetUniversityById(int id);
        void CreateUniversity(University University);
    }
}