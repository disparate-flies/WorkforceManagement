using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Workforce.Models;
using Workforce.Models.ViewModels;
using System.Data.SqlClient;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkforceManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _config;

        public EmployeeController(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public async Task<IActionResult> Index()
        {
            string sql = @"
            select
               e.Id,
               e.FirstName,
               e.LastName,
               e.IsSupervisor,
               d.DepartmentId,
               e.IsActive,
               c.ComputerId
            from Employees e
            join Department d on e.DepartmentId = d.Id
            join Computer c on e.ComputerId = c.Id
        ";
            using (IDbConnection conn = Connection)
            {
                Dictionary<int, Employee> Employee = new Dictionary<int, Employee>();

                var employeeQuerySet = await conn.QueryAsync<Employee, Department, Computer>(
                    sql,
                    (emloyee, department) =>
                    {
                        if (!employees.ContainsKey(employee.Id))
                        {
                            employees[employee.Id] = employee;
                        }
                        employees[employee.Id].Department = department;
                        return employee;
                    });
                return View(employees.Values);
            }
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
