using static Booking_Tickets.Helper.Type;

namespace Booking_Tickets.ViewModels.Event
{
    public class EventModelView
    {
        public string? Name { get; set; } // if party
        public string? EventImage { get; set; } // if party

        public string? Team1 { get; set; } // if match
        public string? TeamImage1 { get; set; } // if match
        public string? Team2 { get; set; } // if match
        public string? TeamImage2 { get; set; } // if match
        public EventType Type { get; set; } 
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public int VenueID { get; set; }
       


    }
}
