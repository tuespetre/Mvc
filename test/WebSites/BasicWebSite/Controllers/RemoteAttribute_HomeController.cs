// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Mvc;
using BasicWebSite.Models;

namespace BasicWebSite.Controllers
{
    public class RemoteAttribute_HomeController : Controller
    {
        private static Person _person;

        [HttpGet]
        public IActionResult Index()
        {
            return View(_person);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Person person)
        {
            ModelState.Remove("id");
            if (!ModelState.IsValid)
            {
                return View(person);
            }

            _person = person;
            return RedirectToAction(nameof(Index));
        }
    }
}