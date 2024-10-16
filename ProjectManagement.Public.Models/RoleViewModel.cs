using LayerAbstractions.Interfaces;

namespace ProjectManagement.Public.Models
{
    public class RoleViewModel : IViewModel<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedWhen { get; set; }
        public DateTime UpdatedWhen { get; set; }
    }
}
