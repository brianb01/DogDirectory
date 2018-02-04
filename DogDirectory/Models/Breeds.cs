using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DogDirectory.Models
{

    public class BreedsList
    {
        public string Status { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string[] Breeds { get; set; }

    }

    public class BreedImageUrl
    {
        public string Status { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string ImageUrl { get; set; }

    }

}