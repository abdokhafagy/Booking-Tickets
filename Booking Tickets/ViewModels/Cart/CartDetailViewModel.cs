using Booking_Tickets.Data.Entites;

namespace Booking_Tickets.ViewModels.Cart
{
    internal class CartDetailViewModel
    {
        public Carts Cart { get; set; }
        public Data.Entites.Venue Venue { get; set; }
        public List<Ticket> Tickets { get; set; }
        public Data.Entites.Event Event { get; set; }
    }
}
