using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApiNetCore.Models
{
    public class Link
    {
        public static Link To(string routeName, object routeValue = null)
            => new Link()
            {
                Method = Get,
                Relations = null,
                RouteName = routeName,
                RouteValue = routeValue
            };

        public static Link ToCollection(string routeName, object routeValue = null)
        {
            return new Link() {
                Method = Get,
                Relations = "Collection",
                RouteName = routeName,
                RouteValue = routeValue
            };

        }

        const string Get = "get";

        public string Href { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(Get)]
        public string Method { get; set; }

        [JsonProperty(PropertyName ="Rel")]
        public string Relations { get; set; }

        [JsonIgnore]
        public string RouteName { get; set; }

        public object RouteValue { get; set; }
    }
}
