using Booking_Tickets.Data.Entites;
using static Booking_Tickets.Helper.Type;

namespace Booking_Tickets.ViewModels.Event
{
    public class DetailModelView
    {
        public string? Name { get; set; } // if party or match
        public string? EventImage { get; set; } // if party
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public string Venue { get; set; }
        public string OrganizedBy { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
