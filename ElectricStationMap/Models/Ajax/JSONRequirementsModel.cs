using Newtonsoft.Json;
using System.Collections;
using System.Text.Json.Serialization;

namespace ElectricStationMap.Models.Ajax
{
    public class JSONRequirementsModel
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("distance")]
        public string Distance { get; set; }
    }
}
