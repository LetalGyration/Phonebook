using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Phonebook.Interfaces;

namespace Phonebook.Models
{
    public class AdoNetDepartmentRepository : IDepartmentRepository
    {
        string connectionString = "server=localhost;port=3306;database=phonebook;user=root;password=12345;";
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            try
            {
                List<DepartmentDto> departments = new List<DepartmentDto>();

                using (Db db = new Db(connectionString))
                {
                    var cmd = db.Connection.CreateCommand();
                    cmd.CommandText = "SELECT DepartmentID, Name, ParentID FROM department";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            departments.Add(new DepartmentDto()
                            {
                                DepartmentID = reader.GetFieldValue<int>(0),
                                Name = reader.GetFieldValue<string>(1),
                                ParentID = reader.IsDBNull(2) ? 0 : reader.GetFieldValue<int>(2)
                            });
                        }
                    }
                    db.Connection.Close();
                }
                return departments;
            }
            catch 
            {
                throw;
            }
        }

        public DepartmentDto GetDepartment(int id)
        {
            try
            {
                DepartmentDto department = new DepartmentDto();

                using (Db db = new Db(connectionString))
                {
                    var cmd = db.Connection.CreateCommand();
                    cmd.CommandText = @"SELECT DepartmentID, Name, ParentID FROM department WHERE DepartmentID = @id;";
                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        department.DepartmentID = reader.GetFieldValue<int>(0);
                        department.Name = reader.GetFieldValue<string>(1);
                        department.ParentID = reader.IsDBNull(2) ? 0 : reader.GetFieldValue<int>(2);
                    }
                    db.Connection.Close();
                }
                return department;
            }
            catch
            {
                throw;
            }
        }

        public void Create(Department item)
        {
            try
            {
                using (Db db = new Db(connectionString))
                {
                    var cmd = db.Connection.CreateCommand();
                    cmd.CommandText = @"INSERT INTO department(Name, ParentID) VALUES (@name, @parentid);";
                    cmd.Parameters.AddWithValue("@name", item.Name);
                    cmd.Parameters.AddWithValue("@parentid", item.ParentID);
                    
                    cmd.ExecuteNonQuery();
                    db.Connection.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        public void Update(int id, Department item)
        {
            try
            {
                using (Db db = new Db(connectionString))
                {
                    var cmd = db.Connection.CreateCommand();
                    cmd.CommandText = @"UPDATE department SET Name=@name, ParentID=@parentid WHERE DepartmentID=@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", item.Name);
                    cmd.Parameters.AddWithValue("@parentid", item.ParentID);

                    cmd.ExecuteNonQuery();

                    cmd.Connection.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (Db db = new Db(connectionString))
                {
                    var cmd = db.Connection.CreateCommand();
                    cmd.CommandText = @"DELETE FROM department WHERE DepartmentID=@id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();

                    db.Connection.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<DepartmentDto> GetChildDepartments(int id)
        {
            try
            {
                List<DepartmentDto> items = new List<DepartmentDto>();
                using (Db db = new Db(connectionString))
                {
                    var cmd = db.Connection.CreateCommand();
                    cmd.CommandText = @"SELECT DepartmentID, Name, ParentID FROM department WHERE ParentID=@id";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new DepartmentDto()
                            {
                                DepartmentID = reader.GetFieldValue<int>(0),
                                Name = reader.GetFieldValue<string>(1),
                                ParentID = reader.IsDBNull(2) ? 0 : reader.GetFieldValue<int>(2),
                            });
                        }
                    }
                }
                return items;
            }
            catch
            {
                throw;
            }
        }
    }
}
