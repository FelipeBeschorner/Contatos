using Contato.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Contato.Config
{
    public class InternalException : Exception
    {
        public virtual HttpStatusCode HttpStatus { get; set; }
        public virtual string Header { get; set; }

        public InternalException(HttpStatusCode HttpStatus, string header, string message) : base(message)
        {
            this.HttpStatus = HttpStatus;
            this.Header = header;
        }
    }

    public class ExceptionResponse
    {
        public HttpStatusCode statusCode { get; set; }
        public ErrorDTO error { get; set; }
        public HttpRequestMessage request { get; set; }
    }
}
