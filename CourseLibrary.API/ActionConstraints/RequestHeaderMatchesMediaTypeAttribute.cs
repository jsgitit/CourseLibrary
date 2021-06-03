using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.ActionConstraints
{
    public class RequestHeaderMatchesMediaTypeAttribute : Attribute, IActionConstraint
    {
        private readonly MediaTypeCollection _mediaTypes = new MediaTypeCollection();
        private readonly string _requestHeaderToMatch;

        public RequestHeaderMatchesMediaTypeAttribute(
            string requestHeaderToMatch,
            string mediaType,
            params string[] otherMediaTypes)
        {
            _requestHeaderToMatch = requestHeaderToMatch ?? throw new ArgumentNullException(nameof(requestHeaderToMatch));

            // If the media types are valid media types,
            // add them to the _mediaTypes collection

            if (MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
            {
                _mediaTypes.Add(parsedMediaType);
            }
            else
            {
                throw new ArgumentException(nameof(mediaType));
            }

            foreach (var otherMediaType in otherMediaTypes)
            {
                if (MediaTypeHeaderValue.TryParse(otherMediaType, out MediaTypeHeaderValue parsedOtherMediaType))
                {
                    _mediaTypes.Add(parsedOtherMediaType);
                }
                else
                {
                    throw new ArgumentNullException(nameof(otherMediaType));
                }
            }
        }
        public int Order => 0;

        public bool Accept(ActionConstraintContext context)
        {
            var requestHeaders = context.RouteContext.HttpContext.Request.Headers;
            
            // These 4 commented line was giving 404 error for each of the three POST Authors
            // The check probably needs to be updated, since false is always returned.

            //if (!requestHeaders.ContainsKey(_requestHeaderToMatch)); 
            //{
            //    return false;
            //}

            var parsedRequestMediaType = new MediaType(requestHeaders[_requestHeaderToMatch]);

            // if one of the media types matches, return true
            foreach (var mediaType in _mediaTypes)
            {
                var parsedMediaType = new MediaType(mediaType);
                if (parsedRequestMediaType.Equals(parsedMediaType))
                {
                    return true;
                }

            }
            return false;
        }
    }
}
