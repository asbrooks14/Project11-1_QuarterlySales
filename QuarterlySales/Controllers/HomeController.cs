// Alexandra Brooks 
// Quarterly Sales (Project 11-1)
// CIS 411-76
// Spring 2022
// Due: 5/3/2022

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Diagnostics;
using QuarterlySales.Models;

namespace QuarterlySales.Controllers
{
    public class HomeController : Controller
    {
        private SalesContext context { get; set; }
        public HomeController(SalesContext ctx) => context = ctx;

        [HttpGet]
        public ViewResult Index(int id)
        {
            IQueryable<Sales> query = context.Sales
                .Include(s => s.Employee)
                .OrderBy(s => s.Year);
            
            if (id > 0)
            {

                query = query.Where(s => s.EmployeeId == id);
            }

            var vm = new SalesListViewModel
            {
                Sales = query.ToList(), 
                Employees = context.Employees.OrderBy(e => e.Firstname).ToList(),
                EmployeeId = id
            };
            return View(vm);
        }

        [HttpPost]
        public RedirectToActionResult Index(Employee employee)
        {
            if (employee.EmployeeId > 0)
                return RedirectToAction("Index", new { id = employee.EmployeeId });
            else
                return RedirectToAction("Index", new { id = "" });
        }


    }
}