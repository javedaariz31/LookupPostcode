using Newtonsoft.Json;

namespace LookupPostcode.API.Service.Data
{
    //The full postcode info for a requested postcode.
    [JsonObject]
    public class PostCodeInfo
    {
        //The HTTP status response from Postcodes.IO.
        [JsonProperty("status")]
        public int Status { get; set; }

        //If an error is returned, it is held here.
        [JsonProperty("error")]
        public string Error { get; set; }

        //The actual result of the API call.
        [JsonProperty("result")]
        public PostCodeResult Result { get; set; }
    }
}
