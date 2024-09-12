using static Booking_Tickets.Helper.Type;

namespace Booking_Tickets.Data.Entites
{
    public class Event
    {
        public int ID { get; set; }
        public string? Name { get; set; } // if party or match
        public string? EventImage { get; set; } // if party

        public string? Team1 { get; set; } // if match
        public string? TeamImage1 { get; set; } // if match
        public string? Team2 { get; set; } // if match
        public string? TeamImage2 { get; set; } // if match

        public EventType Type { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Date;
        public int VenueID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation Property
     
        public Venue Venue { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<UserReview> Reviews { get; set; }
    }
    
}
