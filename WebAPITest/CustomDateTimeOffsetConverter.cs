using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebAPITest
{
    public class CustomDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
		private readonly string Format;
		public CustomDateTimeOffsetConverter(string format)
		{
			Format = format;
		}
		public override void Write(Utf8JsonWriter writer, DateTimeOffset date, JsonSerializerOptions options)
		{
			writer.WriteStringValue(date.ToString(Format));
		}
		public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return DateTimeOffset.ParseExact(reader.GetString(), Format, null);
		}
	}
}
