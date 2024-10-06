using FlowerGarden.DbContextClass;
using FlowerGarden.Models.BusinessObjects;
using Microsoft.AspNetCore.Mvc;

namespace FlowerGarden.Controllers
{
    public class FlowerController : Controller
    {
        private readonly FlowerDbContext _context;

        public FlowerController(FlowerDbContext context)
        {
            _context = context;
        }

        // Show all posts (sorted by CreatedAt in descending order)
        public IActionResult Index()
        {
            var flowerPosts = _context.FlowerPosts
                .OrderByDescending(p => p.CreatedAt)
                .ToList();
            return View(flowerPosts);
        }

        // Create new post
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FlowerPost model, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                // Save image (in wwwroot or cloud storage) and get the URL
                if (imageFile != null)
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageFile.FileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    model.ImageUrl = "/images/" + imageFile.FileName;
                }

                model.CreatedAt = DateTime.Now;
                _context.FlowerPosts.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }

}
