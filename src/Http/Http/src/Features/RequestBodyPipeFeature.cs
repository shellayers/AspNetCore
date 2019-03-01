// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO.Pipelines;

namespace Microsoft.AspNetCore.Http.Features
{
    public class RequestBodyPipeFeature : IRequestBodyPipeFeature
    {
        private PipeReader _internalPipeReader;
        private HttpContext _context;

        public RequestBodyPipeFeature(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _context = context;
        }

        public PipeReader RequestBodyPipe
        {
            get
            {
                return _internalPipeReader;
            }
            set
            {
                _internalPipeReader = value;
                _context.Request.Body = new ReadOnlyPipeStream(_internalPipeReader);
            }
        }
    }
}
