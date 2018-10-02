using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using WorkforceManagement.Models;
using System.Data.SqlClient;

=======
>>>>>>> master

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkforceManagement.Controllers
{
    public class EmployeeController : Controller
    {
<<<<<<< HEAD
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
                    d.DepartmentId
                from Employee e
                join Department d on e.DepartmentId = d.Id
                where e.Id = {id}";

            using (IDbConnection conn = Connection)
            {
                Dictionary<int, Employees> Employee = new Dictionary<int, Employees>();

                var employeeQuerySet = await conn.QueryAsync<Employees, Department, Computer, Employees>(
                    sql,
                    (employees, department, computer) =>
                    {
                        if (!Employee.ContainsKey(employees.Id))
                        {
                            Employee[employees.Id] = employees;
                        }
                        Employee[employees.Id].Department = department;
                        Employee[employees.Id].Computer = computer;
                        return employees;
                    });
                return View(Employee.Values);
            }
        }

        public async Task<IActionResult> EmployeeDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string sql = $@"
            select
               e.Id,
               e.FirstName,
               e.LastName,
               c.ComputerId,
               t.Training
            from Employees e
            join Computer c on e.ComputerId = c.Id
            join Training t on e.Training = t.Id
          ";

            using (IDbConnection conn = Connection)
            {
                Employees employees = (await conn.QueryAsync<Employees>(sql)).ToList().Single();

                if (employees == null)
                {
                    return NotFound();
                }

                return View(employees);
            }
        }

        public async Task<SelectList> EmployeeList(int? selected)
        {
            using (IDbConnection conn = Connection)
            {
                List<Employees> employees = (await conn.QueryAsync<Employees>("SELECT Id, FirstName FROM Employee")).ToList();

                employees.Insert(0, new Employees() { Id=0, FirstName="Insert New Employee..."});

                var selectList = new SelectList(employees, "Id", "FirstName", selected);
                return selectList;
            }
        }

        public async Task<IActionResult> Create()
        {
            using (IDbConnection conn = Connection)
            {
                ViewData["EmployeeId"] = await EmployeeList(null);
                return View();
            }
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (Employees employees)
        {
            if (ModelState.IsValid)
            {
                string sql = $@"
                    insert into Employees
                        (FirstName, LastName, StartDate, Department)
                    values
                        ('{employees.FirstName}',
                         '{employees.LastName}',
                         '{employees.StartDate}',   
                         '{employees.Department}')";

                using (IDbConnection conn = Connection)
                {
                    int rowsAffected = await conn.ExecuteAsync(sql);

                    if (rowsAffected > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }

            using (IDbConnection conn = Connection)
            {
                IEnumerable<Employees> employee = (await conn.QueryAsync<Employees>("SELECT Id, FirstName FROM Employees")).ToList();
                ViewData["EmployeeId"] = await EmployeeList(employee.Employees);
                return View(employee);
            }
        } 
=======

>>>>>>> master
    }
}
