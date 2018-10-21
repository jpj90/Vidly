    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class CustomerFormViewModel
    {
        // when using IEnumberable, you get a nice generic way of being able to
        // iterate through a collection, which is cool if you don't need any
        // of the methods offered on the List object
        public IEnumerable<MembershipType> MembershipType { get; set; }
        public Customer Customer { get; set; }
    }
}