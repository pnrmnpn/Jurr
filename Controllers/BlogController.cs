using Jurr.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Jurr.Controllers
{
    public class BlogController : Controller
    {
        private readonly JurrDbContext _db;
        private readonly PasswordHasher<Admin> _passwordHasher = new();
        public BlogController(JurrDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var posts = await _db.Posts
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
            return View(posts);
        }

        [HttpGet("blog/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var post = await _db.Posts.Include(p=>p.Admin).FirstOrDefaultAsync(p => p.PostId == id);
            if (post == null) return NotFound();
            return View(post);
        }

        [HttpGet]
        [Route("blog/admin/login")]
        public IActionResult AdminLogin()
        {
            return View();
        }

        public record AdminLoginModel(string UserName, string Password);

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("blog/admin/login")]
        public async Task<IActionResult> AdminLogin(AdminLoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var admin = await _db.Admins
                .FirstOrDefaultAsync(a => a.UserName == model.UserName && a.IsActive);

            if (admin == null)
            {
                ModelState.AddModelError(string.Empty, "Невірні дані входу.");
                return View(model);
            }

            // Verify hashed password using ASP.NET Core Identity's PasswordHasher
            var result = _passwordHasher.VerifyHashedPassword(admin, admin.PasswordHash, model.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError(string.Empty, "Невірні дані входу.");
                return View(model);
            }

            TempData["AdminLoggedIn"] = true;
            TempData["AdminName"] = admin.UserName;
            TempData["AdminId"] = admin.AdminId;

            return RedirectToAction(nameof(Create));
        }

        [HttpGet]
        [Route("blog/create")] 
        public IActionResult Create()
        {
            if (!(TempData["AdminLoggedIn"] as bool? ?? false))
                return RedirectToAction(nameof(AdminLogin));
            return View();
        }

        public class CreatePostModel
        {
            public string Title { get; set; } = string.Empty;
            public string? Content { get; set; }
            public IFormFile? Image { get; set; }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("blog/create")] 
        public async Task<IActionResult> Create(CreatePostModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var adminIdObj = TempData["AdminId"];
            int adminId = adminIdObj is int i ? i : 0;
            if (adminId == 0)
            {
                var name = TempData["AdminName"] as string;
                var admin = await _db.Admins.FirstOrDefaultAsync(a => a.UserName == name);
                adminId = admin?.AdminId ?? 0;
            }

            string? imageUrl = null;
            if (model.Image != null && model.Image.Length > 0)
            {
                var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "blog");
                Directory.CreateDirectory(uploadsDir);
                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(model.Image.FileName)}";
                var filePath = Path.Combine(uploadsDir, fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    await model.Image.CopyToAsync(stream);
                }
                imageUrl = $"/images/blog/{fileName}";
            }

            var post = new Post
            {
                Title = model.Title,
                Content = model.Content,
                CreatedAt = DateTime.UtcNow,
                AdminId = adminId,
                ImageUrl = imageUrl
            };
            _db.Posts.Add(post);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
