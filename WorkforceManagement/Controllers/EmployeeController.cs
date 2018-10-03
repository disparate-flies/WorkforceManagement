using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using WorkforceManagement.Models;
using System.Data.SqlClient;
using Dapper;

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
                    e.DepartmentId,
                    e.FirstName,
                    e.LastName,
                    d.Id,
                    d.DeptName
                from Employee e
                join Department d on e.DepartmentId = d.Id";

            using (IDbConnection conn = Connection)
            {
                Dictionary<int, Employees> employee = new Dictionary<int, Employees>();

                var employeeQuerySet = await conn.QueryAsync<Employees, Department, Employees>(
                    sql,
                    (employees, department) =>
                    {
                        if (!employee.ContainsKey(employees.Id))
                        {
                            employee[employees.Id] = employees;
                        }
                        employee[employees.Id].Department = department;
                        return employees;
                    });
                return View(employee.Values);
            }
        }

        public async Task<IActionResult>Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string sql = $@"
             select
                  e.Id,
                   e.DepartmentId,
                  e.FirstName,
                  e.LastName,
                  d.Id,
                   d.DeptName,
                   ec.EmployeeId,
                   ec.ComputerId,
                   c.Id,
                   c.Make,
                   c.Manufacturer,
                   et.EmployeeId,
                   et.TrainingProgramId,
                   tp.Id,
                   tp.ProgName
              from Employee e
              join Department d on e.DepartmentId = d.Id
              join EmployeeComputer ec on e.Id = ec.EmployeeId
              join Computer c on c.Id = ec.ComputerId
              join EmployeeTraining et ON et.EmployeeId = e.Id
              join TrainingProgram tp ON tp.ID = et.TrainingProgramId
              where e.Id = {id}";

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
                    insert into Employee
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
                ViewData["EmployeeId"] = await EmployeeList(employees.Id);
                return View(employee);
            }
        } 
    }
}
