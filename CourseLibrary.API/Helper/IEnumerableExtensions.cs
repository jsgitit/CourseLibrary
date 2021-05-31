using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CourseLibrary.API.Helper
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<ExpandoObject> ShapeData<TSource>(this IEnumerable<TSource> source, string fields)
        {
            if (source==null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            // create a list to hold our ExpandoObjects
            var expandoObjectList = new List<ExpandoObject>();

            // create a list with PropertyInfo objects on TSource.
            // PropertyInfo is defined in System.Reflection.
            // reflection is expensive, so rather than doing it for each 
            // object in the list, we do it once and reuse the results.
            // After all, part of the reflection is on the type of the object
            // (TSource), not on the instance.
            var propertyInfoList = new List<PropertyInfo>();

            // if no fields were requested, then return all fields in the Expando Object
            if (string.IsNullOrWhiteSpace(fields))
            {
                // all public properties should be in the Expando Object
                var propertyInfos = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                propertyInfoList.AddRange(propertyInfos);
            }
            else
            {
                // fields are requested so build those fields by splitting them
                var fieldsAfterSplit = fields.Split(',');

                foreach (var field in fieldsAfterSplit)
                {
                    // trim each field
                    var propertyName = field.Trim();

                    // use reflection to get the property on the source object
                    // we need to include public and instance because specifying
                    // a binding flag overwrites the already existing binding flags
                    var propertyInfo = typeof(TSource)
                        .GetProperty(propertyName,
                            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (propertyInfo == null)
                    {
                        throw new Exception($"Property {propertyName} was not found on" +
                            $" {typeof(TSource)}");
                    }

                    // add propertyInfo to the list
                    propertyInfoList.Add(propertyInfo);

                }
            }

            // run through the source object
            foreach (TSource sourceObject in source)
            {
                // create an ExpandoObject that will hold the selected properties and values
                var dataShapedObject = new ExpandoObject();

                // Get the value of each property needing to be returned.
                // for that we need to run through the list.

                foreach (var propertyInfo in propertyInfoList)
                {

                    // GetValue returns the value of the propert on the source object
                    var propertyValue = propertyInfo.GetValue(sourceObject);

                    // add the field to the ExpandoObject
                    ((IDictionary<string, object>)dataShapedObject).Add(propertyInfo.Name, propertyValue);

                }

                // add the ExpandoObject to the list
                expandoObjectList.Add(dataShapedObject);
            }

            return expandoObjectList;
        }
    }
}
