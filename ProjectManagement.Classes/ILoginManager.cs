using System.Security.Claims;

namespace ProjectManagement.Classes
{
    public interface ILoginManager
    {
        public Task Login(string username, string password);
    }
}
