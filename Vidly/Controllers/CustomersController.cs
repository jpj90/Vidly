using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Customers
        public ActionResult Index()
        {
            //var customers = GetCustomers();
            // mind that EF will not instantly query the database, this is 'deferred execution'
            //var customers = _context.Customers;
            // this way, by calling ToList(), you immediately 'materialze' the querys
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);
        }

        public ActionResult Details(int id)
        {
            //var customer = GetCustomers().SingleOrDefault(c => c.Id == id);
            // the line below is also immediately executed, because of the SingleOrDefault extension method
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

    }
}