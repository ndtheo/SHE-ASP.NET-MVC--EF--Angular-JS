#region Using Directives

using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

#endregion

namespace Utilities.CustomJsonContractResolver
{
    /// <summary>
    /// Custom Json.NET Contract Resolver, Does not add a value to the properties with the <see cref="JsonGetOnlyAttribute"/> decorator
    /// </summary>
    public class GetOnlyContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// Creates a <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> for the given <see cref="T:System.Reflection.MemberInfo" />.
        /// </summary>
        /// <param name="memberSerialization">The member's parent <see cref="T:Newtonsoft.Json.MemberSerialization" />.</param>
        /// <param name="member">The member to create a <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> for.</param>
        /// <returns>A created <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> for the given <see cref="T:System.Reflection.MemberInfo" />.</returns>
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            if (property == null || !property.Writable)
                return property;

            var attributes = property.AttributeProvider.GetAttributes(typeof(JsonGetOnlyAttribute), true);
            if (attributes?.Count > 0)
                property.Writable = false;

            return property;
        }
    }
}