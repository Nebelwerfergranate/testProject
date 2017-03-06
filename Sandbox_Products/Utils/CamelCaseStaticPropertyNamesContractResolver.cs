using Newtonsoft.Json.Serialization;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace Sandbox_Products.Utils
{
    public class CamelCaseStaticPropertyNamesContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        {
            var baseMembers = base.GetSerializableMembers(objectType);

            PropertyInfo[] staticMembers =
                objectType.GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);

            baseMembers.AddRange(staticMembers);

            return baseMembers;
        }
    }
}