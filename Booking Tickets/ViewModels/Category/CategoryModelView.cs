using System.ComponentModel.DataAnnotations;

namespace Booking_Tickets.ViewModels.Category
{
    public class CategoryModelView
    {
        [Required]
       
        public string Name { get; set; }
    }
}
