﻿using Microsoft.AspNetCore.Mvc;
using Module32.Models.Db;

namespace Module32.Controllers
{
    public class UsersController: Controller
    {
        private readonly IBlogRepository _repo;
        public UsersController(IBlogRepository repo)
        {
            _repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            var authors = await _repo.GetUsers();
            return View(authors);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User newUser)
        {
            await _repo.AddUser(newUser);
            return View(newUser);
        }
    }
}
