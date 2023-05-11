namespace BookResale.Api.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } public string Email { get; set; } = string.Empty;
        public string CID { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int Role { get; set; }//0 administrator - 1 seller & buyer - 2 buyer
    }
}
