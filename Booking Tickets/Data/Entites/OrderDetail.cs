namespace Booking_Tickets.Data.Entites
{
    public class OrderDetail
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int TicketID { get; set; }
        public int Quantity { get; set; }  // Number of units of this specific ticket
        public float Price { get; set; }  // Price per unit of the ticket
        public float TotalAmount => Quantity * Price;  // Total cost for this specific item in the order

        // Navigation Properties
        public Carts Order { get; set; }
        public Ticket Ticket { get; set; }
    }

}
