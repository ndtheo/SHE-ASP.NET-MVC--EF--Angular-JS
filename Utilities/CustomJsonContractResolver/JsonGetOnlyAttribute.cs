#region Using Directives

using System;

#endregion

namespace Utilities.CustomJsonContractResolver
{
	/// <summary>
	/// Marks that an property can only be deserialized and not serialized back again, when using the "GetOnlyContractResolver" json resolver for the WebAPI
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class JsonGetOnlyAttribute : Attribute { }
}