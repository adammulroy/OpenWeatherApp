using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenWeatherApp.Api.Converters
{
    //REF - https://stackoverflow.com/questions/66310421/deserialise-json-timestamp-with-system-text-json
    public class UnixToNullableUtcDateTimeConverter : JsonConverter<DateTime?>
    {
        private static readonly long _unixMinSeconds =
            DateTimeOffset.MinValue.ToUnixTimeSeconds() -
            UnixEpoch.ToUnixTimeMilliseconds(); // -62_135_596_800

        private static readonly long _unixMaxSeconds =
            DateTimeOffset.MaxValue.ToUnixTimeSeconds() -
            UnixEpoch.ToUnixTimeMilliseconds(); // 253_402_300_799

        public static DateTimeOffset UnixEpoch => DateTimeOffset.FromUnixTimeMilliseconds(0);

        public override bool HandleNull => true;
        public bool? IsFormatInSeconds { get; set; } = null;

        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TryGetInt64(out var time))
            {
                // if 'IsFormatInSeconds' is unspecified, then deduce the correct type based on whether it can be represented in the allowed .net DateTime range
                if (IsFormatInSeconds == true ||
                    (IsFormatInSeconds == null && time > _unixMinSeconds && time < _unixMaxSeconds))
                    return DateTimeOffset.FromUnixTimeSeconds(time).UtcDateTime;

                return DateTimeOffset.FromUnixTimeMilliseconds(time).UtcDateTime;
            }

            return null;
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }
    }
}