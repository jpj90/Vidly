using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }  

        [Required(ErrorMessage = "Please enter customer's name.")]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime? Birthdate { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        public MembershipType MembershipType { get; set; }

        // note that this is implictly required, because a regular byte
        // cannot be null
        [Display(Name = "Membership Type")]
        // remember to add a placeholder on the form to contain val. error messages
        [Min18YearsIfAMember]
        public byte MembershipTypeId { get; set; }


    }
}