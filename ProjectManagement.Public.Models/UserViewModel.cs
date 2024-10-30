using LayerAbstractions.Interfaces;

namespace ProjectManagement.Public.Models
{
    public class UserViewModel : IViewModel<Guid>
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
