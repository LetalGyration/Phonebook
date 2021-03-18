using Phonebook.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Phonebook.Models
{
    public class AdoNetEmployeeRepository : IEmployeeRepository
    {
        string connectionString = "server=localhost;port=3306;database=phonebook;user=root;password=12345;";
        public IEnumerable<EmployeeDto> GetEmployees(int departmentID)
        {
            try
            {
                List<EmployeeDto> items = new List<EmployeeDto>();

                using (Db db = new Db(connectionString))
                {
                    var cmd = db.Connection.CreateCommand();
                    cmd.CommandText = @"SELECT EmployeeID, FirstName, LastName, PhoneNumber, Address, DepartmentID FROM employee WHERE DepartmentID=@id";
                    cmd.Parameters.AddWithValue("@id", departmentID);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new EmployeeDto()
                            {
                                EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                Address = reader["Address"].ToString(),
                                DepartmentID = Convert.ToInt32(reader["DepartmentID"])
                            });
                        }
                    }
                    db.Connection.Close();
                }
                return items;
            }
            catch
            {
                throw;
            }
        }

        public EmployeeDto GetEmployee(int id)
        {
            try
            {
                EmployeeDto item = new EmployeeDto();
                using (Db db = new Db(connectionString))
                {
                    var cmd = db.Connection.CreateCommand();
                    cmd.CommandText = @"SELECT EmployeeID, FirstName, LastName, PhoneNumber, Address, DepartmentID FROM employee WHERE EmployeeID = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            item.EmployeeID = Convert.ToInt32(reader["EmployeeID"]);
                            item.FirstName = reader["FirstName"].ToString();
                            item.LastName = reader["LastName"].ToString();
                            item.PhoneNumber = reader["PhoneNumber"].ToString();
                            item.Address = reader["Address"].ToString();
                            item.DepartmentID = Convert.ToInt32(reader["DepartmentID"]);
                        }
                    }
                    db.Connection.Close();
                }
                return item;
            }
            catch
            {
                throw;
            }
        }

        public void Create(Employee employee)
        {
            try
            {
                using (Db db = new Db(connectionString))
                {
                    var cmd = db.Connection.CreateCommand();
                    cmd.CommandText = @"INSERT INTO employee(FirstName, LastName, PhoneNumber, Address, DepartmentID) VALUES (@firstName, @lastName, @phoneNumber, @address, @departmentID)";
                    cmd.Parameters.AddWithValue("@firstName", employee.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", employee.LastName);
                    cmd.Parameters.AddWithValue("@phoneNumber", employee.PhoneNumber);
                    cmd.Parameters.AddWithValue("@address", employee.Address);
                    cmd.Parameters.AddWithValue("@departmentID", employee.DepartmentID);

                    cmd.ExecuteNonQuery();

                    db.Connection.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        public void Update(int id, Employee employee)
        {
            try
            {
                using (Db db = new Db(connectionString))
                {
                    var cmd = db.Connection.CreateCommand();
                    cmd.CommandText = @"UPDATE employee SET FirstName=@firstName, LastName=@lastName, PhoneNumber=@phoneNumber, Address=@address, DepartmentID=@departmentID WHERE EmployeeID=@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@firstName", employee.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", employee.LastName);
                    cmd.Parameters.AddWithValue("@phoneNumber", employee.PhoneNumber);
                    cmd.Parameters.AddWithValue("@address", employee.Address);
                    cmd.Parameters.AddWithValue("@departmentID", employee.DepartmentID);

                    cmd.ExecuteNonQuery();

                    db.Connection.Close();
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
                    cmd.CommandText = @"DELETE FROM employee WHERE EmployeeID=@id";
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

        public IEnumerable<EmployeeDto> FindByAttributes(string information)
        {
            try
            {
                List<EmployeeDto> items = new List<EmployeeDto>();
                using (Db db = new Db(connectionString))
                {
                    var cmd = db.Connection.CreateCommand();
                    cmd.CommandText = $"SELECT EmployeeID, FirstName, LastName, PhoneNumber, Address, DepartmentID FROM employee WHERE FirstName LIKE '%{information}%' OR LastName LIKE '%{information}%' OR PhoneNumber LIKE '%{information}%' OR Address LIKE '%{information}%'";
                    //cmd.Parameters.AddWithValue("@info", findInformation);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new EmployeeDto
                            {
                                EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                Address = reader["Address"].ToString(),
                                DepartmentID = Convert.ToInt32(reader["DepartmentID"])
                            });
                        }
                    }
                    db.Connection.Close();
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
