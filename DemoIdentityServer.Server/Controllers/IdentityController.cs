﻿using DemoIdentityServer.Server.Models;
using IdentityServer4;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DemoIdentityServer.Server.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    public class IdentityController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;

        public IdentityController(IIdentityServerInteractionService interaction)
        {
            _interaction = interaction ?? throw new ArgumentNullException(nameof(interaction));
        }

        [HttpGet("[action]")]
        public IActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please. Validate your credentials and try again.");
                return View(model);
            }

            if (model.UserName.ToLower() != "test")
            {
                ModelState.AddModelError("UserName", "User not found");
                return View(model);
            }

            await HttpContext.SignInAsync(new IdentityServerUser(model.UserName));

            return Redirect(model.ReturnUrl);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Logout(string logoutId)
        {
            var logout = await _interaction.GetLogoutContextAsync(logoutId);
            await HttpContext.SignOutAsync();

            return Redirect(logout.PostLogoutRedirectUri);
        }
    }
}
