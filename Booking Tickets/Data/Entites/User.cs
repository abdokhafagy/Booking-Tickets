
using static Booking_Tickets.Helper.Role;

namespace Booking_Tickets.Data.Entites
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation Property
        public ICollection<Carts> Orders { get; set; }
        public ICollection<UserReview> Reviews { get; set; }
    }

}
