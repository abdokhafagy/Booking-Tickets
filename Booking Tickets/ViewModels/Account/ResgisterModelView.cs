using System.ComponentModel.DataAnnotations;

namespace Booking_Tickets.ViewModels.Account
{
    public class ResgisterModelView
    {
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Username { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string FullName { get; set; }
        [Required]
        [MinLength(11)]
        [MaxLength(11)]
        public string Phone { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }


    }
}
