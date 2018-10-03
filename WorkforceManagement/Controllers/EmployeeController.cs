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
using WorkforceManagement.Models.ViewModels;
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

        public async Task<IActionResult> Details(int? id)
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
                   c.Manufacturer
              from Employee e
              join Department d on e.DepartmentId = d.Id
              left join EmployeeComputer ec on e.Id = ec.EmployeeId
              left join Computer c on c.Id = ec.ComputerId
              where e.Id = {id}";

            using (IDbConnection conn = Connection)
            {
                Employees emp = new Employees();
                Department dept = new Department();
                Computer comp = new Computer();
                var employeeQuerySet = await conn.QueryAsync<Employees, Department, Computer, Employees>(
    sql,
             (employee, department, computer) =>
             {

                 emp.Id = employee.Id;
                 emp.FirstName = employee.FirstName;
                 emp.LastName = employee.LastName;

                 dept.Id = department.Id;
                 dept.DeptName = department.DeptName;

                 if (computer != null)
                 {
                     comp.Id = computer.Id;
                     comp.Manufacturer = computer.Manufacturer;
                 }


                 return employee;
           });
                emp.Department = dept;
                emp.Computer = comp;
                return View(emp);
             }
        }

 
        public async Task<SelectList> DepartmentList(int? selected)
        {
            using (IDbConnection conn = Connection)
            {
                List<Department> departments = (await conn.QueryAsync<Department>("SELECT Id, DeptName FROM Department")).ToList();

                departments.Insert(0, new Department() { Id=0, DeptName = "Select Department..."});

                var selectList = new SelectList(departments, "Id", "Name", selected);
                return selectList;
            }
        }

        public async Task<IActionResult> Create()
        {
            using (IDbConnection conn = Connection)
            {
                EmployeesCreateViewModel model = new EmployeesCreateViewModel(_config);

                return View(model);
            }
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (EmployeesCreateViewModel employeescreateviewmodel)
        {
            if (ModelState.IsValid)
            {
                string sql = $@"
                    insert into Employee
                        (FirstName, LastName, StartDate, DepartmentId)
                    values
                        ('{employeescreateviewmodel.Employees.FirstName}',
                         '{employeescreateviewmodel.Employees.LastName}',
                         '{employeescreateviewmodel.Employees.StartDate}',   
                         '{employeescreateviewmodel.DepartmentId}')";

                using (IDbConnection conn = Connection)
                {
                    int rowsAffected = await conn.ExecuteAsync(sql);

                    if (rowsAffected > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

            }
            return View(employeescreateviewmodel);
        } 
    }
}
