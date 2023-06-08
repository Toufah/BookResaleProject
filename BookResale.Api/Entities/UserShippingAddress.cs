namespace BookResale.Api.Entities
{
    public class UserShippingAddress
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string? Address { get; set; }
        public string? city { get; set; }
        public string? phoneNumber { get; set; }
    }
}
