using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Phonebook.Interfaces;
using Phonebook.Models;
using Phonebook.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
//using web = System.Web.Http;

namespace Phonebook.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        IEmployeeRepository CreateRepository()
        {
            return new AdoNetEmployeeRepository();
        }

        [HttpGet("{DepartmentID}", Name = "GetEmployees")]
        public IEnumerable<EmployeeDto> GetEmployees(int DepartmentID)
        {
            var repo = CreateRepository();
            return repo.GetEmployees(DepartmentID);
        }

        /*[HttpGet("[action]/{id}", Name = "GetEmployee")]
        public IActionResult GetEmployee(int id)
        {
            var repo = CreateRepository();
            EmployeeDto item = repo.GetEmployee(id);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }*/

        [HttpPost]
        public IActionResult CreateEmployee([FromBody]Employee employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }
            var repo = CreateRepository();
            repo.Create(employee);
            return StatusCode(200);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] Employee employee)
        {
            /*if (employee == null || employee.EmployeeID != id)
            {
                return BadRequest();
            }*/
            var repo = CreateRepository();
            var item = repo.GetEmployee(id);
            if (item == null)
            {
                return NotFound();
            }
            
            repo.Update(id, employee);
            return RedirectToRoute("GetEmployees", new { DepartmentID = employee.DepartmentID });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var repo = CreateRepository();
            int depID = repo.GetEmployee(id).DepartmentID;
            repo.Delete(id);

            return StatusCode(200);
        }

        [Route("[action]/{findInformation}")]
        [HttpGet(Name = "Find")]
        public IEnumerable<EmployeeDto> FindEmployee(string findInformation)
        {
            /*var serializeOptions = new JsonSerializerOptions();
            serializeOptions.Converters.Add(new StringConverter());
            string convertedObject = JsonSerializer.Deserialize<string>(findInformation,
            serializeOptions);*/
            var repo = CreateRepository();
            return repo.FindByAttributes(findInformation);
        }
    }
}
