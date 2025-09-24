using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication2F.Controllers
{
    public class AdminController : Controller
    {
        [Authorize (Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
