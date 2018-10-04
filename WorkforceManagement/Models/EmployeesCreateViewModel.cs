
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WorkforceManagement.Models;

namespace WorkforceManagement.Models.ViewModels
{
    public class EmployeesCreateViewModel
    {
        public Employees Employees { get; set; }

        public int DepartmentId { get; set; }

        [Display(Name = "Department Name")]
        public List<SelectListItem> Department { get; }

        private readonly IConfiguration _config;

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public EmployeesCreateViewModel() { }

        public EmployeesCreateViewModel(IConfiguration config)
        {
            _config = config;

            string sql = $@"SELECT Id, DeptName FROM Department";

            using (IDbConnection conn = Connection)
            {
                List<Department> departments = (conn.Query<Department>(sql)).ToList();

                this.Department = departments
                    .Select(li => new SelectListItem
                    {
                        Text = li.DeptName,
                        Value = li.Id.ToString()
                    }).ToList();
            }


            // Add a prompt so that the <select> element isn't blank
            this.Department.Insert(0, new SelectListItem
            {
                Text = "Choose Department...",
                Value = "0"
            });
        }
    }
}