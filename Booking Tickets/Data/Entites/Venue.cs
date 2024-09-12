using System.ComponentModel.DataAnnotations;

namespace Booking_Tickets.Data.Entites
{
    public class Venue
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation Property
        public ICollection<Event> Events { get; set; }
    }

}
