using LayerAbstractions.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ProjectManagement.Models
{
    public class User : IdentityUser<Guid>, IModel<Guid>
    {
        public DateTime CreatedWhen { get; set; }
        public DateTime UpdatedWhen { get; set; }
        public bool IsDeleted { get; set; }
    }
}
