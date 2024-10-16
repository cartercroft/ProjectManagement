using LayerAbstractions.Interfaces;

namespace ProjectManagement.Public.Models
{
    public class RoleViewModel : IViewModel<Guid>
    {
        public Guid Id { get; set; }
    }
}
