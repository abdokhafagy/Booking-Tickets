namespace Booking_Tickets.ViewModels.Tickets
{
    public class TicketsModelView
    {
        public int EventID { get; set; }
        public string TicketType { get; set; }
        public float Price { get; set; }
        public int AvailableQuantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
     //   public Data.Entites.Event Event { get; set; }
    }
}
