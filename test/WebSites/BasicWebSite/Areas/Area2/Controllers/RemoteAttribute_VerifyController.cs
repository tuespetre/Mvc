// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Mvc;
using BasicWebSite.Models;

namespace BasicWebSite.Areas.Area2.Controllers
{
    [Area("Area2")]
    [Route("[Area]/[Controller]/[Action]", Order = -2)]
    public class RemoteAttribute_VerifyController : Controller
    {
        // Demonstrates validation action when AdditionalFields causes client to send multiple values.
        [HttpGet]
        public IActionResult IsIdAvailable(User user)
        {
            return Json(data: string.Format(
                "/Area2/RemoteAttribute_Verify/IsIdAvailable rejects '{0}' with '{1}', '{2}', and '{3}'.",
                user.UserId4,
                user.UserId1,
                user.UserId2,
                user.UserId3));
        }
    }
}