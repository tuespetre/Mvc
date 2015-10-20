// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNet.Mvc.Logging
{
    public static class RedirectResultLoggerExtensions
    {
        private static Action<ILogger, string, string, Exception> _redirectResult;

        static RedirectResultLoggerExtensions()
        {
            _redirectResult = LoggerMessage.Define<string, string>(LogLevel.Information, 1, "RedirectResult for action {ActionName} executed. The destination was {Destination}");
        }

        public static void RedirectResultExecuted(this ILogger logger, ActionContext context,
            string destination)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            _redirectResult(logger, actionName, destination, null);
        }
    }
}
