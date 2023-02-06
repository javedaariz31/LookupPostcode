using Newtonsoft.Json;

namespace LookupPostcode.API.Service.Data
{
    public class PostCodeResult
    {
        //The returned postcode.
        [JsonProperty("postcode")]
        public string Postcode { get; set; }

        //The quality of the postcode.
        [JsonProperty("quality")]
        public int Quality { get; set; }

        //Country the postcode is in, region.
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        //Parliamentary/council stuff.
        [JsonProperty("parliamentary_constituency")]
        public string ParliamentaryConstituency { get; set; }

        [JsonProperty("admin_district")]
        public string AdminDistrict { get; set; }

        //Longitude, latitude.
        [JsonProperty("longitude")]
        public double Longitude { get; set; }
        [JsonProperty("latitude")]
        public double Latitude { get; set; }


        public string Area
        {
            get { return Latitude < 52.229466 ? "South" : 52.229466 <= Latitude ? "Midlands" : Latitude < 53.27169 ? "North" : "NA"; }
        }
    }
}
