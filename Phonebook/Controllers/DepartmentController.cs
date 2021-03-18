using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Phonebook.Interfaces;
using Phonebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        IDepartmentRepository CreateRepository()
        {
            return new AdoNetDepartmentRepository();
        }
        
        [HttpGet(Name = "GetAllDepartments")]
        public IEnumerable<DepartmentDto> GetDepartments()
        {
            var repo = CreateRepository();
            return repo.GetAllDepartments();
        }

        [HttpGet("{id}", Name = "GetDepartment")]
        public IActionResult GetDepartment(int id)
        {
            var repo = CreateRepository();
            DepartmentDto item = repo.GetDepartment(id);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult CreateDepartment([FromBody]Department item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            var repo = CreateRepository();
            repo.Create(item);
            return StatusCode(200);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]Department updatedItem)
        {
            /*if (updatedItem == null || updatedItem.DepartmentID != id)
            {
                return BadRequest();
            }*/
            var repo = CreateRepository();
            var item = repo.GetDepartment(id);

            if (item == null)
            {
                return NotFound();
            }
            
            repo.Update(id, updatedItem);
            return StatusCode(200);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var repo = CreateRepository();
            repo.Delete(id);

            return StatusCode(200);
        }

        [HttpGet("[action]/{id}")]
        public IEnumerable<DepartmentDto> GetChildDepartments(int id)
        {
            var repo = CreateRepository();
            return repo.GetChildDepartments(id);
        }
    }
}
