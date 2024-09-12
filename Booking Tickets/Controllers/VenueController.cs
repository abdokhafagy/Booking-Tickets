using Booking_Tickets.Data;
using Booking_Tickets.Data.Entites;
using Booking_Tickets.ViewModels.Category;
using Booking_Tickets.ViewModels.Venue;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Booking_Tickets.Controllers
{
    public class VenueController : Controller
    {
        private ApplicationDbContext _context;

        public VenueController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var venues = await _context.Venues.ToListAsync();
            return View(venues);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(VenueModelView model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var existVenue = await _context.Venues.FirstOrDefaultAsync(c => c.Name == model.Name);
            if (existVenue is not null)
            {
                ModelState.AddModelError("Name", $"the {existVenue.Name} is already exist before ");
                return View(model);
            }

            Venue venue = new()
            {
                Name = model.Name,
                Address= model.Address,
                Capacity= model.Capacity,
                
                CreatedAt = DateTime.UtcNow,
            };

            await _context.AddAsync(venue);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Venue");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var venue = await _context.Venues.FindAsync(id);
            return View(venue);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, VenueModelView model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var venue = await _context.Venues.FirstOrDefaultAsync(c => c.ID == id);

            venue.Name = model.Name;
            venue.Address = model.Address;
            venue.Capacity=model.Capacity;
            venue.UpdatedAt = DateTime.UtcNow;


            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Venue");
        }

     
        public async Task<IActionResult> Remove(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var venue = await _context.Venues.FindAsync(id);
            if (venue == null)
            {
                return NotFound();
            }
            _context.Remove(venue);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Venue");
        }
    }
}
