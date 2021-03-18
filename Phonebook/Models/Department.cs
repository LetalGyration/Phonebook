using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        /*public string Address { get; set; }
        public string Postcode { get; set; }*/
        public int ParentID { get; set; }
    }
}
