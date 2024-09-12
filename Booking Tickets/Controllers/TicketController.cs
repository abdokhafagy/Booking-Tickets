using Booking_Tickets.Data;
using Booking_Tickets.Data.Entites;
using Booking_Tickets.ViewModels.Event;
using Booking_Tickets.ViewModels.Tickets;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Booking_Tickets.Controllers
{
    public class TicketController : Controller
    {
        private ApplicationDbContext _context;

        public TicketController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var tickets = await _context.Tickets
               .Include(e => e.Event)
               .ToListAsync();
            return View(tickets);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var events = _context.Events.Select(v => new { v.ID, v.Name }).ToList();

            ViewBag.Venues = new SelectList(events, "ID", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TicketsModelView model)
        {
            //  var categories = _context.Categories.Select(c => new { c.ID, c.Name }).ToList();
            var events = _context.Events.Select(v => new { v.ID, v.Name }).ToList();
            ViewBag.Venues = new SelectList(events, "ID", "Name");
            if (!ModelState.IsValid)
                return View(model);


            Ticket tic = new()
            {
                TicketType = model.TicketType,

                Price = model.Price,
                AvailableQuantity = model.AvailableQuantity,

                
                
                EventID = model.EventID,
                CreatedAt = DateTime.UtcNow,

            };

            await _context.AddAsync(tic);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Ticket");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var tic = await _context.Tickets.FindAsync(id);
            var events = _context.Events.Select(v => new { v.ID, v.Name }).ToList();

            //ViewBag.Categories = new SelectList(categories, "ID", "Name");
            ViewBag.Venues = new SelectList(events, "ID", "Name");
            return View(tic);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, TicketsModelView model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var tic = await _context.Tickets.FirstOrDefaultAsync(c => c.ID == id);

            tic.TicketType = model.TicketType;
            tic.Price = model.Price;
            tic.AvailableQuantity = model.AvailableQuantity;
            tic.EventID = model.EventID;
            tic.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Ticket");
        }

        public async Task<IActionResult> Remove(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var tic = await _context.Tickets.FindAsync(id);
            if (tic == null)
            {
                return NotFound();
            }
            _context.Remove(tic);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Ticket");
        }
    }
}
