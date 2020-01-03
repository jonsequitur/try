﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Concurrent;
using System.Reflection;
using Pocket;
using Xunit.Sdk;

namespace Microsoft.DotNet.Interactive.Tests
{
    internal class LogTestNamesToPocketLoggerAttribute : BeforeAfterTestAttribute
    {
        private static readonly ConcurrentDictionary<MethodInfo, OperationLogger> _operations = new ConcurrentDictionary<MethodInfo, OperationLogger>();

        public override void Before(MethodInfo methodUnderTest)
        {
            var x = Logger.Log.OnEnterAndExit(name: methodUnderTest.Name);
            _operations.TryAdd(methodUnderTest, x);
        }

        public override void After(MethodInfo methodUnderTest)
        {
            if (_operations.TryRemove(methodUnderTest, out var operation))
            {
                operation.Dispose();
            }
        }
    }
}