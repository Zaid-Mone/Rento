using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rento.Models
{
    public class Reservation
    {
        //public Reservation()
        //{
        //    NumberOfDays = Convert.ToInt32(ReturnDate - PickupDate);
        //}
        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime PickupDate { get; set; }
        [Required]
        public DateTime ReturnDate { get; set; }

        public int NumberOfDays { get; set; }
        [Required]
        public double Price { get; set; }

        //public string ApplicationUserId { get; set; }
        //[NotMapped]
        //[ForeignKey("ApplicationUserId")]
        //public ApplicationUser ApplicationUser { get; set; }

        public Guid CarId { get; set; }
        [ForeignKey("CarId")]
        public Car Car { get; set; }


    }
}
