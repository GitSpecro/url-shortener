using Microsoft.AspNetCore.Mvc;
using UrlShortener.Api.Data;
using UrlShortener.Api.Models;
using UrlShortener.Api.DTOs;

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
        public IActionResult CreateShortUrl([FromBody] CreateShortUrlRequest request)
        {

            if(string.IsNullOrWhiteSpace(request.OriginalUrl))
                return BadRequest("OriginalUrl is required");

            var shortCode = Guid.NewGuid().ToString("N").Substring(0, 6);

            var user = _context.Users.FirstOrDefault();
            if (user == null)
            {
                user = new User();
                _context.Users.Add(user);
                _context.SaveChanges();
            }

            var shortUrl = new ShortUrl
            {
                OriginalUrl = request.OriginalUrl,
                ShortCode = shortCode,
                UserId = user.Id
            };

            _context.ShortUrls.Add(shortUrl);
            _context.SaveChanges();

            return Ok(new
            {
                shortUrl = shortCode
            });
        }

        [HttpGet("/{shortCode}")]
        public IActionResult RedirectToOriginal(string shortCode)
        {
            var shortUrl = _context.ShortUrls
                .FirstOrDefault(su => su.ShortCode == shortCode);

            if (shortUrl == null)
                return NotFound();

            shortUrl.ClickCount++;

            var share = Math.Min(10 + (shortUrl.ClickCount / 5) * 10, 80);
            shortUrl.SharePercentage = share;

            var user = _context.Users.First(u => u.Id == shortUrl.UserId);

            var earnings = 10 * (share / 100m);
            user.WalletBalance += earnings;

            _context.SaveChanges();

            return Redirect(shortUrl.OriginalUrl);
        }

        [HttpGet]
        public IActionResult GetDashboard()
        {
            var data = _context.ShortUrls
                .Select(s => new
                {
                    s.ShortCode,
                    s.OriginalUrl,
                    s.ClickCount,
                    WalletBalance = s.User.WalletBalance,
                    s.SharePercentage
                })
                .ToList();

            return Ok(data);
        }
    }
}
