using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.API.Controllers
{
    [Route("/api/[controller]/[action]")]
    [ApiController]

    public class APIControllerBase : ControllerBase
    {
    }
}
