using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WorkforceManagement.Models;
using Dapper;

namespace WorkforceManagement.Controllers
{
    public class ComputerController : Controller
    {
        private readonly IConfiguration _config;

        public ComputerController(IConfiguration config)
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


        // GET: Computer
        public async Task<IActionResult> Index()
        {
        using (IDbConnection conn = Connection) {
                IEnumerable<Computer> computer = await conn.QueryAsync<Computer>(
                    "select Id, PurchaseDate, Manufacturer, Make, DecommissionDate, Condition from Computer;"
                );
                return View(computer);
}
        }

        // GET: Computer/Details/5
        public async Task<IActionResult> Details([FromRoute]int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string sql = $@"
            SELECT
                c.Id,
                c.PurchaseDate,
                c.Manufacturer,
                c.Make,
                c.DecommissionDate,
                c.Condition
            FROM Computer c
            WHERE c.Id = {id}";

            using (IDbConnection conn = Connection)
            {
                Computer computer = await conn.QuerySingleAsync<Computer>(sql);

                if (computer == null) {
                    return NotFound();
                }

                return View(computer);
            }
        }

        // GET: Computer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Computer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Computer computer)
        {
            if (ModelState.IsValid) {
                string sql = $@"
                    INSERT INTO Computer
                        (PurchaseDate, Manufacturer, Make )
                        VALUES
                        ('{computer.PurchaseDate}',
                         '{computer.Manufacturer}', 
                         '{computer.Make}')";

                using (IDbConnection conn = Connection)
                {
                    int rowsAffected = await conn.ExecuteAsync(sql);

                    if (rowsAffected > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
                return View(computer);
        }

        // GET: Computer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Computer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Computer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Computer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}