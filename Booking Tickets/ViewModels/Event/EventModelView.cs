using Booking_Tickets.Data.Entites;
using static Booking_Tickets.Helper.Type;

namespace Booking_Tickets.ViewModels.Event
{
    public class EventModelView
    {
        public EventModelView()
        {
            Tickets = new List<Ticket>();
        }
        
        public string? Name { get; set; } // if party
        public IFormFile EventImage { get; set; } // if party

        public string? FirstTeamName { get; set; } // if match
        public IFormFile FirstTeamImage { get; set; } // if match
        public string? SecondTeamName { get; set; } // if match
        public IFormFile SecondTeamImage { get; set; } // if match

        public string OrganizedBy { get; set; }

        public EventType Type { get; set; } 
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public int VenueID { get; set; }
        public Data.Entites.Venue? Venue { get; set; }
        public ICollection<Ticket> Tickets { get; set; }


    }
}
