using System.ComponentModel.DataAnnotations;

namespace Booking_Tickets.Data.Entites
{
    public class UserReview
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int EventID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public Event Event { get; set; }
    }

}
