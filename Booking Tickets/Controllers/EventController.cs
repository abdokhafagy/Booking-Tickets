using Booking_Tickets.Data;
using Booking_Tickets.Data.Entites;
using Booking_Tickets.Helper;
using Booking_Tickets.ViewModels.Event;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Booking_Tickets.Controllers
{
    public class EventController : Controller
    {
        private ApplicationDbContext _context;
        private readonly IWebHostEnvironment _host;

        public EventController(ApplicationDbContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
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
                .Include(e=>e.Tickets)
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
            var firstTeamImage = await UploadImage.UploadImageAsync(model.FirstTeamImage, _host.WebRootPath, "Uploaded Images/Events");
            var secondTeamImage = await UploadImage.UploadImageAsync(model.SecondTeamImage, _host.WebRootPath, "Uploaded Images/Events");
            var eventImage = await UploadImage.UploadImageAsync(model.EventImage, _host.WebRootPath, "Uploaded Images/Events");
           
            Event even = new()
            {
                Name = model.Name,
                EventImage = eventImage,

                FirstTeamName = model.FirstTeamName,
                SecondTeamName = model.SecondTeamName,

                Type = model.Type,

                FirstTeamImage = firstTeamImage,
                SecondTeamImage = secondTeamImage,

                Description = model.Description,
                Date = model.Date,

                VenueID = model.VenueID,
                CreatedAt = DateTime.UtcNow,
                OrganizedBy=model.OrganizedBy

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
            var even = await _context.Events.Include(q=>q.Venue).FirstOrDefaultAsync(q=>q.ID==id);
            var venues = _context.Venues.Select(v => new { v.ID, v.Name }).ToList();
            ViewBag.Venues = new SelectList(venues, "ID", "Name");

            UpdateEventModelView model = new()
            {
                ID = even.ID,
                Name = even.Name,
               // EventImage = even.EventImage,

                FirstTeamName = even.FirstTeamName,
                SecondTeamName = even.SecondTeamName,

                Type = even.Type,

               // FirstTeamImage = firstTeamImage,
               // SecondTeamImage = secondTeamImage,

                Description = even.Description,
                Date = even.Date,

                VenueID = even.VenueID,
                Venue=even.Venue,
                OrganizedBy = even.OrganizedBy

            };
            return View(model);
        }
     
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateEventModelView model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var even = await _context.Events.FirstOrDefaultAsync(c => c.ID == id);

            var firstTeamImage = await UploadImage.UploadImageAsync(model.FirstTeamImage, _host.WebRootPath, "Uploaded Images/Events");
            var secondTeamImage = await UploadImage.UploadImageAsync(model.SecondTeamImage, _host.WebRootPath, "Uploaded Images/Events");
            var eventImage = await UploadImage.UploadImageAsync(model.EventImage, _host.WebRootPath, "Uploaded Images/Events");

            even.Name = model.Name;
            even.EventImage=eventImage;

            even.FirstTeamImage = firstTeamImage;
            even.SecondTeamImage = secondTeamImage;

            even.Description = model.Description;
            even.Date = model.Date;
            even.VenueID = model.VenueID;
            even.UpdatedAt = DateTime.UtcNow;

            even.OrganizedBy=model.OrganizedBy;


            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Event");
        }
      
        public async Task<IActionResult> Detail(int id)
        {
            var events = await _context.Events
                                       .Include(e => e.Venue)
                                       .Include(e => e.Tickets)
                                       .FirstOrDefaultAsync(e => e.ID == id);

            if (events == null)
            {
                return NotFound();
            }

            var model = new DetailModelView
            {
                Name = events.Name,
                Description = events.Description,
                EventImage = events.EventImage,
                Date = events.Date,
                Venue = events.Venue.Name,
                OrganizedBy=events.OrganizedBy,
                Tickets = new List<Ticket>()
            };

            // Iterate over events.Tickets and add them to the model.Tickets list
            foreach (var item in events.Tickets)
            {
                model.Tickets.Add(item);
            }

            return View(model);
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
