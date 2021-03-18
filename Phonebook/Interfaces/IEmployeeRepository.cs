using Phonebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Interfaces
{
    interface IEmployeeRepository
    {
        IEnumerable<EmployeeDto> GetEmployees(int departmentID);
        EmployeeDto GetEmployee(int id);
        void Create(Employee employee);
        void Update(int id, Employee employee);
        void Delete(int id);
        IEnumerable<EmployeeDto> FindByAttributes(string information);
    }
}
