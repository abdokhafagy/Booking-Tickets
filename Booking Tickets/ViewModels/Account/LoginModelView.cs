using System.ComponentModel.DataAnnotations;

namespace Booking_Tickets.ViewModels.Account
{
    public class LoginModelView
    {
        [Required]
        [MinLength(11)]
        [MaxLength(11)]
        public string Phone { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

    }
}
