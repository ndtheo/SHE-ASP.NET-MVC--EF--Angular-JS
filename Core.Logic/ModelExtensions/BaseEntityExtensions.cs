using Core.Entities;
using Newtonsoft.Json;
using Utilities.CustomJsonContractResolver;

namespace Core.Logic.ModelExtensions
{
    public static class BaseEntityExtensions
	{
		public static T JsonCopy<T>(this T source) where T : BaseEntity
		{
			string sourceString = JsonConvert.SerializeObject(source);
			var serializerSettings = new JsonSerializerSettings {ContractResolver = new GetOnlyContractResolver()};
			var copy = JsonConvert.DeserializeObject<T>(sourceString, serializerSettings);
			copy.Id = 0;
			return copy;
		}
	}
}