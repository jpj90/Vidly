using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        public List<Customer> customers { get; set; }

        public CustomersController()
        {
            customers = new List<Customer>
            {
                new Customer { Name = "John Smith", Id = 1 },
                new Customer { Name = "Mary Williams", Id = 2 }
            };
        }
        // GET: Customers
        public ActionResult Index()
        {
            
            return View(customers);
        }

        public ActionResult Details(int id)
        {
            if(customers.Any(x => x.Id == id))
                return View(customers[id - 1]);
            else
                return HttpNotFound();
        }
    }
}