using DockerNewPsg.Models;
using DockerNewPsg.Services;
using Microsoft.AspNetCore.Mvc;

namespace DockerNewPsg.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }




        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAsync();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                await _userService.CreateAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
    }
}
