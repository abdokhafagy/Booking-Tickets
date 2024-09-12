using Booking_Tickets.Data;
using Booking_Tickets.Data.Entites;
using Booking_Tickets.Helper;
using Booking_Tickets.ViewModels.Event;
using Booking_Tickets.ViewModels.Venue;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Booking_Tickets.Controllers
{
    public class EventController : Controller
    {
        private ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Main()
        {
            var events = await _context.Events.GroupBy(e => e.Type).ToListAsync();
                
            return View(events);
        }
        public async Task<IActionResult> Index(Helper.Type.EventType type)
        {
            
            var events = await _context.Events
                .Include(e => e.Venue)
                .Where(e => e.Type == type)
                .ToListAsync();

            return View(events);
        }


        [HttpGet]
        public IActionResult Create()
        {
           // var categories = _context.Categories.Select(c => new { c.ID, c.Name }).ToList();
            var venues = _context.Venues.Select(v => new { v.ID, v.Name }).ToList();

            // Pass the data to the view using ViewBag
          //  ViewBag.Categories = new SelectList(categories, "ID", "Name");
            ViewBag.Venues = new SelectList(venues, "ID", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EventModelView model)
        {
          //  var categories = _context.Categories.Select(c => new { c.ID, c.Name }).ToList();
            var venues = _context.Venues.Select(v => new { v.ID, v.Name }).ToList();
          //  ViewBag.Categories = new SelectList(categories, "ID", "Name");
            ViewBag.Venues = new SelectList(venues, "ID", "Name");
            if (!ModelState.IsValid)
                return View(model);

            //var existEvent = await _context.Events.FirstOrDefaultAsync(c => c.Name == model.Name);
            //if (existEvent is not null)
            //{
            //    ModelState.AddModelError("Name", $"the {existEvent.Name} is already exist before ");
            //    return View(model);
            //}

            Event even = new()
            {
                Name = model.Name,

                Team1=model.Team1,
                Team2=model.Team2,

                Type=model.Type,

                TeamImage1=model.TeamImage1,
                TeamImage2=model.TeamImage2,
                
                Description = model.Description,
                Date = model.Date,

                VenueID = model.VenueID,
                CreatedAt = DateTime.UtcNow,
                
            };

            await _context.AddAsync(even);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Event");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var venue = await _context.Events.FindAsync(id);

            //var categories =await _context.Categories
            //    .Select(c => new { c.ID, c.Name })
            //    .FirstOrDefaultAsync(cat=>cat.ID==venue.CategoryID);
            //var venues =await _context.Venues
            //    .Select(v => new { v.ID, v.Name })
            //    .FirstOrDefaultAsync(ven=>ven.ID==venue.VenueID);
            // Pass the data to the view using ViewBag
           // var categories = _context.Categories.Select(c => new { c.ID, c.Name }).ToList();
            var venues = _context.Venues.Select(v => new { v.ID, v.Name }).ToList();

            //ViewBag.Categories = new SelectList(categories, "ID", "Name");
            ViewBag.Venues = new SelectList(venues, "ID", "Name");
            return View(venue);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, EventModelView model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var even = await _context.Events.FirstOrDefaultAsync(c => c.ID == id);

            even.Name = model.Name;
            even.Description = model.Description;
            even.Date = model.Date;
            even.VenueID = model.VenueID;
            even.UpdatedAt = DateTime.UtcNow;


            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Event");
        }
        public async Task<IActionResult> Detail(int id)
        {
            var events = await _context.Events
                                       .Include(e => e.Venue)
                                       .FirstOrDefaultAsync(e=>e.ID==id);
            Carts carts = new()
            {
                
            };
         //   var cart=await _context.Carts.
            return View(events);
        }
        public async Task<IActionResult> Remove(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var even = await _context.Events.FindAsync(id);
            if (even == null)
            {
                return NotFound();
            }
            _context.Remove(even);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Event");
        }
    
    }
}
