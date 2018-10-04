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
using Microsoft.Extensions.Configuration;
using WorkforceManagement.Models;
using System.Data.SqlClient;

namespace WorkforceManagement.Controllers
{
    public class TrainingController : Controller
    {
        private readonly IConfiguration _config;

        public TrainingController(IConfiguration config)
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

        //GET Training Program List
        public async Task<IActionResult> Index()
        {
            string sql = @"
                select
                t.Id,
                t.ProgName,
                t.StartDate,
                t.EndDate
                from TrainingProgram t
				where StartDate > (select CAST(GETDATE() as DATE))
                ";


            using (IDbConnection conn = Connection)
            {
                IEnumerable<Training> trainingPrograms = await conn.QueryAsync<Training>(
                    sql);
                return View(trainingPrograms);
            }
        }

        //GET Training Program Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string sql = $@"
                select
                t.Id,
                t.ProgName,
                t.StartDate,
                t.EndDate,
                t.ProgDesc,
                t.MaxAttendees,
                et.Id,
                et.EmployeeId,
                et.TrainingProgramId,
                e.Id,
                e.FirstName,
                e.LastName
                from TrainingProgram t
                LEFT JOIN EmployeeTraining et ON t.Id = et.TrainingProgramId
				LEFT JOIN Employee e ON  e.Id = et.EmployeeId
                WHERE t.Id = {id}";

            using (IDbConnection conn = Connection)
            {
                Training program = new Training();
                var deptQuery = await conn.QueryAsync<Training, Employees, Training>(sql,
                    (training, employee) =>
                    {

                        program.Id = training.Id;
                        program.ProgName = training.ProgName;
                        program.StartDate = training.StartDate;
                        program.EndDate = training.EndDate;
                        program.MaxAttendees = training.MaxAttendees;
                        program.ProgDesc = training.ProgDesc;


                        program.EmployeeList.Add(employee);
                        return training;
                    });
                return View(program);
            }
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }


        //POST Create Training Program
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Training trainingProgram)
        {

            if (ModelState.IsValid)
            {
                string sql = $@"
                INSERT INTO TrainingProgram
                (ProgName, StartDate, EndDate, MaxAttendees, ProgDesc)
                VALUES
                ( '{trainingProgram.ProgName}'
                    ,'{trainingProgram.StartDate}'
                    ,'{trainingProgram.EndDate}'
                    ,'{trainingProgram.MaxAttendees}'
                    ,'{trainingProgram.ProgDesc}'
                )
                ";

                using (IDbConnection conn = Connection)
                {
                    int rowsAffected = await conn.ExecuteAsync(sql);

                    if (rowsAffected > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(trainingProgram);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string sql = $@"
                SELECT 
                t.Id,
                t.ProgName,
                t.StartDate,
                t.EndDate,
                t.ProgDesc,
                t.MaxAttendees
                from TrainingProgram t
                WHERE t.Id = {id}
";
            using (IDbConnection conn = Connection)
            {
                Training model = (await conn.QueryAsync<Training>(sql)).Single();

                return View(model);
            }


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Training model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string sql = $@"
                UPDATE TrainingProgram
                SET             ProgName = '{model.ProgName}',
                                StartDate = '{model.StartDate.Date}',
                                EndDate = '{model.EndDate.Date}',
                                ProgDesc = '{model.ProgDesc}',
                                MaxAttendees = '{model.MaxAttendees}'
                WHERE id = {id}";

                using (IDbConnection conn = Connection)
                {
                    int rowsAffected = await conn.ExecuteAsync(sql);
                    if (rowsAffected > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    throw new Exception("Training program not updated");
                }
            }
            else
            {
                return new StatusCodeResult(StatusCodes.Status406NotAcceptable);
            }
            }
        }
    }