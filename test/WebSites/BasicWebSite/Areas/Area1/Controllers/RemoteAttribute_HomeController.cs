// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Mvc;
using BasicWebSite.Models;

namespace BasicWebSite.Areas.Area1.Controllers
{
    [Area("Area1")]
    [Route("[Area]/[Controller]/[Action]", Order = -2)]
    public class RemoteAttribute_HomeController : Controller
    {
        private static User _user;

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            ModelState.Remove("id");
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            _user = user;
            return RedirectToAction(nameof(Details));
        }

        [Route("/[Area]", Name = "Area1Home", Order = -3)]
        [Route("/[Area]/[Controller]/Index", Order = -2)]
        public IActionResult Details()
        {
            return View(_user);
        }
    }
}