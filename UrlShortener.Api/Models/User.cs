namespace UrlShortener.Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public decimal WalletBalance { get; set; }
        public int SharePercentage { get; set; } = 10;
    }
}
