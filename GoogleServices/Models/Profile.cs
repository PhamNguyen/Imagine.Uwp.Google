using System;
using Newtonsoft.Json;

namespace Imagine.Uwp.Google
{
    public class Profile
    {
        [JsonProperty("id")]
        public string UserId { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("verified_email")]
        public bool IsVerified { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("given_name")]
        public string GivenName { get; set; }

        [JsonProperty("family_name")]
        public string FamilyName { get; set; }

        [JsonProperty("link")]
        public string GooglePlusUrl { get; set; }

        [JsonProperty("picture")]
        public string Avatar { get; set; }

        [JsonProperty("gender")]
        [JsonConverter(typeof(StringToGenderBoolJsonConverter))]
        public bool IsMale { get; set; }

        public class StringToGenderBoolJsonConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return (objectType == typeof(string));
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (reader != null && !string.IsNullOrEmpty(reader.Value.ToString()))
                {
                    return reader.Value.ToString().Equals("male");
                }
                return false;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
            }
        }
    }
}
