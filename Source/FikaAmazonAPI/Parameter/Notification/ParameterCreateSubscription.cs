using FikaAmazonAPI.Search;
using static FikaAmazonAPI.Utils.Constants;

namespace FikaAmazonAPI.Parameter.Notification
{
    public class ParameterCreateSubscription : ParameterBased
    {
        public string payloadVersion { get; set; }
        public string destinationId { get; set; }
        public NotificationType notificationType { get; set; }

        public ProcessingDirective processingDirective { get; set; }
        public class ProcessingDirective
        {
            /// <summary>A notificationType specific filter.</summary>
            [Newtonsoft.Json.JsonProperty("eventFilter", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public EventFilter EventFilter { get; set; }


        }

        public class EventFilter : AggregationFilter
        {
            [Newtonsoft.Json.JsonProperty("marketplaceIds", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public MarketplaceIds MarketplaceIds { get; set; }

            /// <summary>An eventFilterType value that is supported by the specific notificationType. This is used by the subscription service to determine the type of event filter. Refer to the section of the [Notifications Use Case Guide](doc:notifications-api-v1-use-case-guide) that describes the specific notificationType to determine if an eventFilterType is supported.</summary>
            [Newtonsoft.Json.JsonProperty("eventFilterType", Required = Newtonsoft.Json.Required.Always)]
            [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
            public string EventFilterType { get; set; }


        }

        public class MarketplaceFilter
        {
            [Newtonsoft.Json.JsonProperty("marketplaceIds", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public MarketplaceIds MarketplaceIds { get; set; }


        }

        public  class MarketplaceIds : System.Collections.ObjectModel.Collection<string>
        {

        }

        public  class AggregationFilter
        {
            [Newtonsoft.Json.JsonProperty("aggregationSettings", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public AggregationSettings AggregationSettings { get; set; }


        }
        public  class AggregationSettings
        {
            /// <summary>The supported time period to use to perform marketplace-ASIN level aggregation.</summary>
            [Newtonsoft.Json.JsonProperty("aggregationTimePeriod", Required = Newtonsoft.Json.Required.Always)]
            [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
            [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
            public AggregationTimePeriod AggregationTimePeriod { get; set; }


        }

        public enum AggregationTimePeriod
        {
            [System.Runtime.Serialization.EnumMember(Value = @"FiveMinutes")]
            FiveMinutes = 0,

            [System.Runtime.Serialization.EnumMember(Value = @"TenMinutes")]
            TenMinutes = 1,

        }


    }
}
