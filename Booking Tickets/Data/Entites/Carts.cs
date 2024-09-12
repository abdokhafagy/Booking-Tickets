namespace Booking_Tickets.Data.Entites
{
    public class Carts
    {

        public int ID { get; set; }
        public int UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public float TotalAmount { get; set; }  // Total cost of all items in the order
        public bool OrderStatus { get; set; }

        // Navigation Property
        public User User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }


    }

}
