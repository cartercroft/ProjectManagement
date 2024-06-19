namespace ProjectManagement.Classes
{
    public class UserProfile
    {
        public UserProfile() {}
        public string? Username { get; set; }
        public string? Email { get; set; }
        public Dictionary<string, string> Claims { get; set; } = new Dictionary<string, string>();

        //other claims...
    }
}
