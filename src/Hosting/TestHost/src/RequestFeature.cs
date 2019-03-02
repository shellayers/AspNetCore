// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.IO;
using System.IO.Pipelines;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace Microsoft.AspNetCore.TestHost
{
    internal class RequestFeature : IHttpRequestFeature
    {
        private Stream _internalStream;
        private HttpContext _context;

        public RequestFeature(HttpContext context)
        {
            Body = Stream.Null;
            Headers = new HeaderDictionary();
            Method = "GET";
            Path = "";
            PathBase = "";
            Protocol = "HTTP/1.1";
            QueryString = "";
            Scheme = "http";
            _context = context;
        }

        public Stream Body
        {
            get
            {
                return _internalStream;
            }
            set
            {
                _internalStream = value;
            }
        }

        public IHeaderDictionary Headers { get; set; }

        public string Method { get; set; }

        public string Path { get; set; }

        public string PathBase { get; set; }

        public string Protocol { get; set; }

        public string QueryString { get; set; }

        public string Scheme { get; set; }

        public string RawTarget { get; set; }
    }
}
