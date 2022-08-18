using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rento.Models
{
    public class CarType
    {
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "Car Type ")]
        public string Name { get; set; }
        public string  ImageUrl { get; set; }
    }
}
