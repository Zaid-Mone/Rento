using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rento.Models
{
    public class Car
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Car Image")]
        public string ImageUrl { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string PlateNumber { get; set; }
        [Required]
        public double PricePerDay { get; set; }
        [Required]
        public Guid CarTypeId { get; set; }
        [ForeignKey("CarTypeId")]
        public CarType CarType { get; set; }

        public List<Reservation> Reservations { get; set; }


    }
}
