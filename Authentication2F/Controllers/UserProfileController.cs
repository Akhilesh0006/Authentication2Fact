using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication2F.Controllers
{
    public class UserProfileController : Controller
    {
        [Authorize (Roles = "User")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
