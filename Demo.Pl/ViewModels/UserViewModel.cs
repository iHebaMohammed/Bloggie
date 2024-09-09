namespace Demo.Pl.ViewModels
{
    public class UserViewModel
    {
        public List<User> Users { get; set; }

        // For create user:
        public string Username { get; set; }
        public string Email{ get; set; }
        public string Password { get; set; }
        public bool AdminRoleCheckbox { get; set; }
    }
}
