using Booking_Tickets.Data;
using Booking_Tickets.Data.Entites;
using Booking_Tickets.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Booking_Tickets.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
     /*   public async Task<IActionResult> Index()
        {
            var Categories=await _context.Categories.ToListAsync();
            return View(Categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryModelView model)
        {
            if(!ModelState.IsValid)
                return View(model);
            var existCat = await _context.Categories.FirstOrDefaultAsync(c=>c.Name==model.Name);
            if (existCat is not null)
            {
                ModelState.AddModelError("Name", $"the {existCat.Name} is already exist before ");
                return View(model);
            }

            Category cat = new()
            {
                Name = model.Name,
                CreatedAt = DateTime.UtcNow,
            };

            await _context.AddAsync(cat);
           await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Category");
        }

        [HttpGet]
        public async Task< IActionResult> Update(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
                var cat = await _context.Categories.FindAsync(id);
            return View(cat);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,CategoryModelView model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var cat = await _context.Categories.FirstOrDefaultAsync(c => c.ID == id);

            cat.Name = model.Name;
            cat.UpdatedAt = DateTime.UtcNow;

           
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Category");
        }*/

    }
}
