// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using System.IO.Pipelines;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Http.Features
{
    public class HttpResponseFeature : IHttpResponseFeature, IResponseBodyPipeFeature
    {
        internal Stream _internalStream;
        internal PipeWriter _internalPipeWriter;

        public HttpResponseFeature()
        {
            StatusCode = 200;
            Headers = new HeaderDictionary();
            _internalStream = Stream.Null;
        }

        public int StatusCode { get; set; }

        public string ReasonPhrase { get; set; }

        public IHeaderDictionary Headers { get; set; }

        public Stream Body
        {
            get
            {
                return _internalStream;
            }
            set
            {
                _internalStream = value;
                var responsePipeWriter = new StreamPipeWriter(_internalStream);
                _internalPipeWriter = responsePipeWriter;
            }
        }

        public virtual bool HasStarted
        {
            get { return false; }
        }

        public PipeWriter ResponseBodyPipe
        {
            get
            {
                return _internalPipeWriter;
            }
            set
            {
                _internalPipeWriter = value;
                _internalStream = new WriteOnlyPipeStream(_internalPipeWriter);
            }
        }

        public virtual void OnStarting(Func<object, Task> callback, object state)
        {
        }

        public virtual void OnCompleted(Func<object, Task> callback, object state)
        {
        }
    }
}
