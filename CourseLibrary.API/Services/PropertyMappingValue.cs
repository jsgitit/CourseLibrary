using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Services
{
    public class PropertyMappingValue
    {
        public IEnumerable<string> DestinationProperties { get; private set; }
        public bool ReverseSort { get; private set; }

        public PropertyMappingValue(IEnumerable<string> destinationProperties, bool reverseSort = false)
        {
            DestinationProperties = destinationProperties ?? 
                throw new ArgumentNullException(nameof(destinationProperties));
            ReverseSort = reverseSort;
        }

    }
}
