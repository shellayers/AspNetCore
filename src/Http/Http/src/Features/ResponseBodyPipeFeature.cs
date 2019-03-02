// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO.Pipelines;

namespace Microsoft.AspNetCore.Http.Features
{
    public class ResponseBodyPipeFeature : IResponseBodyPipeFeature
    {
        internal PipeWriter _internalPipeWriter;
        private HttpRequestFeature _feature;

        public ResponseBodyPipeFeature(HttpRequestFeature feature)
        {
            if (feature == null)
            {
                throw new ArgumentNullException(nameof(feature));
            }
            _feature = feature;
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
                _feature._internalStream = new WriteOnlyPipeStream(_internalPipeWriter);
            }
        }
    }
}
