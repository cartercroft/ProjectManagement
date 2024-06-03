using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Classes
{
    public static class Enumeration
    {
        public enum ProjectTaskStatus
        {
            NotSet = -1,
            [Display(Name = "To Do")]
            ToDo = 1,
            [Display(Name = "In Progress")]
            InProgress = 2,
            Blocked = 3,
            [Display(Name = "Pending Approval")]
            PendingApproval = 4,
            Complete = 5
        }
    }
}
