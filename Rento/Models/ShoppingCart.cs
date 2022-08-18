using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rento.Models
{
    public class ShoppingCart
    {
        [Key]
        public Guid Id { get; set; }

        public Guid CarId { get; set; }
        [ForeignKey("CarId")]
        [ValidateNever]
        public Car Car { get; set; }

       
        public string  ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
