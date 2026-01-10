using Microsoft.AspNetCore.Mvc;
using UrlShortener.Api.Data;
using UrlShortener.Api.Models;

namespace UrlShortener.Api.Controllers
{
    [ApiController]
    [Route("api/urls")]
    public class UrlsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UrlsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateShortUrl([FromBody] string originalUrl)
        {
            var shortCode = Guid.NewGuid().ToString("N").Substring(0, 6);

            // Temporary: single default user
            var user = _context.Users.FirstOrDefault();
            if (user == null)
            {
                user = new User();
                _context.Users.Add(user);
                _context.SaveChanges();
            }

            var shortUrl = new ShortUrl
            {
                OriginalUrl = originalUrl,
                ShortCode = shortCode,
                UserId = user.Id
            };

            _context.ShortUrls.Add(shortUrl);
            _context.SaveChanges();

            return Ok(new
            {
                shortUrl = shortCode,
                originalUrl
            });
        }
    }
}
