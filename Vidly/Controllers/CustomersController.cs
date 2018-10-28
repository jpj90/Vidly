using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

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
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var ViewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipType = membershipTypes
            };
            return View("CustomerForm",ViewModel);
        }

        // applying the HttpPost attribute, you ensure that only this type can reach the action
        // the passed in model will be mapped to the request data, this is 'Model Binding'
        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            //this will check if all validations on the Customer action parameter that was
            // passed in check out. if not, then return the object to the user for revision.
            // be sure to also create a ValidationMessage placeholder in your form!
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipType = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
                // the method below will add the 'customer' ONLY in memory and will mark it as 'added'
                _context.Customers.Add(customer);
            else {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
            }
            // the command below actually persists all work to the database
            // NOTA BENE: this means that ALL current changes will be executed in a single db transaction
            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipType = _context.MembershipTypes.ToList()
            };

            // by entering a view name, you override the convention that otherwise, ASP.NET
            // will look for a View named 'Edit'
            return View("CustomerForm", viewModel);
        }
    }
}