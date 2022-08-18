using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rento.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Display(Name ="First Name")]
        [StringLength(25)]
        public string  FirstName { get; set; }
        [Display(Name = "Last Name")]
        [StringLength(25)]
        public string LastName { get; set; }
    }
}
