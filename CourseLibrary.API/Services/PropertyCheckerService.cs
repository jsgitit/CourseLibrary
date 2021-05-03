using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CourseLibrary.API.Services
{
    public class PropertyCheckerService : IPropertyCheckerService
    {
        public bool TypeHasProperties<T>(string fields)
        {
            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            // the fields are separated by a comma, so split it
            var fieldsAfterSplit = fields.Split(',');

            // check if the requiest fields exist on source
            foreach (var field in fieldsAfterSplit)
            {
                // trim each field
                var propertyName = field.Trim();

                // use reflection to check if the property can be foudn on T.
                var propertyInfo = typeof(T)
                    .GetProperty(propertyName,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                // if there are no properties, return false
                if (propertyInfo == null)
                {
                    return false;
                }
            }

            // otherwise, return true, since we have properties in this T
            return true;
        }
    }
}
