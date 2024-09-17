using static Booking_Tickets.Helper.Type;

namespace Booking_Tickets.Data.Entites
{
    public class Event
    {
        public int ID { get; set; }
        public string? Name { get; set; } // if party or match
        public string? EventImage { get; set; } // if party

        public string? FirstTeamName { get; set; } // if match
        public string? FirstTeamImage { get; set; } // if match
        public string? SecondTeamName { get; set; } // if match
        public string? SecondTeamImage { get; set; } // if match

        public string OrganizedBy { get; set; }

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
