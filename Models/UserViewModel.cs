namespace Productapp_with_SQL.Models
{
    public class UserViewModel
    {
        public string UserId { get; set; }

        public string Email { get; set; }

        public IList<string> Roles { get; set; }
    }
}
