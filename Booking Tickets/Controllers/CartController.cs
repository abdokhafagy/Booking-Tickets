using Booking_Tickets.Data;
using Booking_Tickets.Data.Entites;
using Booking_Tickets.ViewModels.Cart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Booking_Tickets.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        

        [HttpPost]
        public async Task<IActionResult> AddToCart(CartViewModel model)
        {
            // Get User ID from the claims (as b{efore)
            if (!User.Identity.IsAuthenticated) 
                return RedirectToAction("Login", "Account");// If the user is not logged in or the claim is missing
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)!.Value;
            int userId = int.Parse(userIdClaim);

            // Fetch or create the cart
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserID == userId && !c.OrderStatus);
            if (cart == null)
            {
                cart = new Carts
                {
                    UserID = userId,
                    OrderDate = DateTime.Now,
                    OrderStatus = false,
                    TotalAmount = 0
                };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            // Add order details based on the quantities in the model
            if (model.ThirdClassQty > 0)
            {
                var orderDetail = new OrderDetail
                {
                    OrderID = cart.ID,
                    TicketID = 1, // Assuming ID for Third Class Ticket
                    Quantity = model.ThirdClassQty,
                    Price = 200.00f
                };
                _context.OrderDetails.Add(orderDetail);
                cart.TotalAmount += orderDetail.TotalAmount;
            }

            if (model.FirstClassQty > 0)
            {
                var orderDetail = new OrderDetail
                {
                    OrderID = cart.ID,
                    TicketID = 2, // Assuming ID for First Class Ticket
                    Quantity = model.FirstClassQty,
                    Price = 2000.00f
                };
                _context.OrderDetails.Add(orderDetail);
                cart.TotalAmount += orderDetail.TotalAmount;
            }

            if (model.ExclusiveBoxQty > 0)
            {
                var orderDetail = new OrderDetail
                {
                    OrderID = cart.ID,
                    TicketID = 3, // Assuming ID for Exclusive Box Ticket
                    Quantity = model.ExclusiveBoxQty,
                    Price = 5000.00f
                };
                _context.OrderDetails.Add(orderDetail);
                cart.TotalAmount += orderDetail.TotalAmount;
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Redirect to the Cart Details or Order Summary page
            return RedirectToAction("Detail", "Cart", new { id = cart.ID });
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            // Fetch the cart by ID
            var cart = await _context.Carts
                                     .Include(c => c.OrderDetails)
                                     .ThenInclude(od => od.Ticket)
                                     .FirstOrDefaultAsync(c => c.ID == id);

            if (cart == null)
            {
                return NotFound();
            }

            // Retrieve the first order detail
            var firstOrderDetail = cart.OrderDetails.FirstOrDefault();
            if (firstOrderDetail == null)
            {
                return NotFound("No order details found for this cart.");
            }

            // Extract the EventID from the first order detail's ticket
            var eventId = firstOrderDetail.Ticket?.EventID;
            if (eventId == null)
            {
                return NotFound("Event ID not found in ticket.");
            }

            // Now, safely query the event information
            var eventInfo = await _context.Events
                                          .Include(e => e.Venue)
                                          .FirstOrDefaultAsync(e => e.ID == eventId);

            if (eventInfo == null)
            {
                return NotFound("Event details not found.");
            }

            var tickets = _context.Tickets
                                  .Where(t => t.EventID == eventInfo.ID)
                                  .ToList();

            // ViewModel to pass data to the view
            var viewModel = new CartDetailViewModel
            {
                Cart = cart,
                Event = eventInfo,
                Venue = eventInfo.Venue,
                Tickets = tickets
            };

            return View(viewModel);
        }




    }
}
