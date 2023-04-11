using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportTrigger.Models
{
    public class GuestModel
    {
        [JsonProperty("guestType")]
        public string GuestType { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("nationality")]
        public string Nationality { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }
        [JsonProperty("phoneNumbers")]
        public string[] PhoneNumbers { get; set; }
        [JsonProperty("dateOfBirth")]
        public string DateofBirth { get; set; }
        [JsonProperty("extensions")]
        public List<Extension> Extensions { get; set; }
        [JsonProperty("identifiers")]
        public List<Identifier> Identifiers { get; set; }
    }
    public class Extension
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("loyaltyTier")]
        public string LoyaltyTier { get; set; }
        [JsonProperty("saudiid")]
        public string SaudiIdNumber { get; set; }
        [JsonProperty("residencystatus")]
        public string ResidencyStatus { get; set; }
        [JsonProperty("campus")]
        public string Campus { get; set; }
    }

    public class Identifier
    {
        [JsonProperty("provider")]
        public string Provider { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
