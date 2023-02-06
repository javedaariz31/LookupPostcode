using Newtonsoft.Json;
using System.Collections.Generic;

namespace LookupPostcode.API.Service.Data
{
    //The full postcode info for a requested postcode.
    public class PostCodesInfo
    {
        //The HTTP status response from Postcodes.IO.
        [JsonProperty("status")]
        public int Status;

        //If an error is returned, it is held here.
        [JsonProperty("error")]
        public string Error;

        //The actual result of the API call.
        [JsonProperty("result")]
        public ICollection<string> Result;
    }
}
