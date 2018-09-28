
﻿//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading.Tasks;
//using Dapper;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.Extensions.Configuration;
//using System.Data.SqlClient;


//// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace WorkforceManagement.Controllers
//{
//    public class EmployeeController : Controller
//    {
//        private readonly IConfiguration _config;

//        public EmployeeController (IConfiguration config)
//        {
//            _config = config;
//        }

//        public IDbConnection Connection
//        {
//            get
//            {
//                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
//            }
//        }

//        public async Task<IActionResult> Index ()
//        {
//            string sql = @"
//            select
//               e.Id,
//               e.FirstName,
//               e.LastName,
//               e."
//        }
//        public IActionResult Index()
//        {
//            return View();
//        }
//    }
//}
