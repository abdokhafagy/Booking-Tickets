namespace Booking_Tickets.Data.Entites
{
    public class Ticket
    {
        public int ID { get; set; }
        public int EventID { get; set; }
        public string TicketType { get; set; }
        public float Price { get; set; }
        public int AvailableQuantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation Property
        public Event Event { get; set; }
    }

}
