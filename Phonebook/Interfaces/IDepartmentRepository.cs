using Phonebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Interfaces
{
    interface IDepartmentRepository
    {
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentDto GetDepartment(int id);
        void Create(Department item);
        void Update(int id, Department item);
        void Delete(int id);
        IEnumerable<DepartmentDto> GetChildDepartments(int id);
        
    }
}
