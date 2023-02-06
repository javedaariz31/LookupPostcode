using LookupPostcode.API.Service.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LookupPostcode.API.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostCodeController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ILogger<PostCodeController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public PostCodeController(ILogger<PostCodeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// To Get the post Code suppiled with all the information. 
        /// </summary>
        /// <param name="postcode"></param>
        /// <returns></returns>
        [HttpGet("{postcode}", Name = "GetPostCode")]
        public ActionResult GetPostCode(string postcode)
        {
            string contents = GetPostCodeAPI(postcode);

            //Deserializing using Newtonsoft.JSON to a PostcodeInfo object.
            var postcodeInfo = JsonConvert.DeserializeObject<PostCodeInfo>(contents);

            //Checking for errors.
            if (postcodeInfo.Status != 200)
            {
                throw new Exception(postcodeInfo.Error);
            }
            //Returning.
            return Ok(postcodeInfo.Result);
        }


        /// <summary>
        /// To Get the post Codes to fullfill AutoCOmplete Functionality
        /// </summary>
        /// <param name="postcode"></param>
        /// <returns></returns>
        [HttpGet("{postcode}/autocomplete", Name = "GetPostCodes")]
        public ActionResult GetPostCodeLists(string postcode)
        {
            List<object> resultlist = new List<object>();
            if (!string.IsNullOrEmpty(postcode))
                resultlist = GetPostCodeAutoCompleteAPI(postcode);

            if (resultlist != null)
                return Ok(resultlist);
            return NotFound();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postcode"></param>
        /// <returns></returns>
        private List<object> GetPostCodeAutoCompleteAPI(string postcode)
        {
            string contents;
            postcode = postcode.Replace(" ", "%20");
            List<object> postCodeObjects = new List<object>();

            using (var wc = new System.Net.WebClient())
            {
                contents = wc.DownloadString("http://api.postcodes.io/postcodes/" + postcode + "/autocomplete");
            }
            ////Deserializing using Newtonsoft.JSON to a PostcodeInfo object.
            var postcodesInfo = JsonConvert.DeserializeObject<PostCodesInfo>(contents);
            if (postcodesInfo.Result != null)
            {
                foreach (var item in postcodesInfo.Result)
                {
                    var resultContent = GetPostCodeAPI(item);
                    var postcodeInfo = JsonConvert.DeserializeObject<PostCodeInfo>(resultContent);
                    //o South: latitude < 52.229466 ; //o Midlands: 52.229466 <= latitude < 53.27169 ; //o North: latitude >= 53.27169
                    //var latitude = postcodeInfo.Result.Latitude;
                    //var latituedString = latitude < 52.229466 ? "South" : 52.229466 <= latitude ? "Midlands" : latitude < 53.27169 ? "North" : "NA";
                    postCodeObjects.Add(new { postCode = postcodeInfo.Result.Postcode, Area = postcodeInfo.Result.Area });
                }
            }
            if (postCodeObjects != null)
                return postCodeObjects;
            else
                return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postcode"></param>
        /// <returns></returns>
        private string GetPostCodeAPI(string postcode)
        {
            postcode = postcode.Replace(" ", "%20");
            string contents;
            using (var wc = new System.Net.WebClient())
            {
                contents = wc.DownloadString("http://api.postcodes.io/postcodes/" + postcode);
            }

            return contents;
        }
    }
}
