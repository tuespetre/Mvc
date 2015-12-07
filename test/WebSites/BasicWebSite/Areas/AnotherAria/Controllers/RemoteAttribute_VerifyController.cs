// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Mvc;
using BasicWebSite.Models;

namespace BasicWebSite.Ares.AnotherAria.Controllers
{
    [Area("AnotherAria")]
    public class RemoteAttribute_VerifyController : Controller
    {
        // Demonstrates validation action when AdditionalFields causes client to send multiple values.
        [HttpGet]
        public IActionResult IsIdAvailable(RemoteAttributeUser user)
        {
            return Json(data: string.Format(
                "/AnotherAria/RemoteAttribute_Verify/IsIdAvailable rejects '{0}' with '{1}', '{2}', and '{3}'.",
                user.UserId4,
                user.UserId1,
                user.UserId2,
                user.UserId3));
        }
    }
}