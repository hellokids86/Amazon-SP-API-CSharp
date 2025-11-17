
using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace FikaAmazonAPI.NotificationMessages
{


    /// <summary>
    /// The notification response schema that comprises the entire JSON document for
    /// PRICING_HEALTH notification
    /// </summary>
    public class PricingHealth
    {

        public partial class Payload
        {
            /// <summary>
            /// The issue type for the notification
            /// </summary>
            [JsonProperty("issueType")]
            public string IssueType { get; set; }

            [JsonProperty("merchantOffer")]
            public MerchantOffer MerchantOffer { get; set; }

            [JsonProperty("offerChangeTrigger")]
            public OfferChangeTrigger OfferChangeTrigger { get; set; }

            /// <summary>
            /// The seller identifier for the offer
            /// </summary>
            [JsonProperty("sellerId")]
            public string SellerId { get; set; }

            [JsonProperty("summary")]
            public Summary Summary { get; set; }
        }

        /// <summary>
        /// Offer details of the merchant receiving the notification
        /// </summary>
        public class MerchantOffer
        {
            [JsonProperty("condition")]
            public string Condition { get; set; }

            [JsonProperty("fulfillmentType")]
            public FulfillmentType FulfillmentType { get; set; }

            [JsonProperty("landedPrice")]
            public MoneyType LandedPrice { get; set; }

            [JsonProperty("listingPrice")]
            public MoneyType ListingPrice { get; set; }

            [JsonProperty("points", NullValueHandling = NullValueHandling.Ignore)]
            public Points Points { get; set; }

            [JsonProperty("shipping")]
            public MoneyType Shipping { get; set; }
        }

        /// <summary>
        /// ListingPrice + Shipping
        ///
        /// The price of the item
        ///
        /// The shipping cost
        ///
        /// The average selling price of the item
        ///
        /// The competitive price threshold from external competitors of Amazon
        ///
        /// The manufacturer suggested retail price for the ASIN
        ///
        /// The 14 day maximum of retail offer price
        /// </summary>




        /// <summary>
        /// The event that caused the notification to be sent
        /// </summary>
        public class OfferChangeTrigger
        {
            /// <summary>
            /// The asin for the item that had an offer change
            /// </summary>
            [JsonProperty("asin")]
            public string Asin { get; set; }

            /// <summary>
            /// The condition of the item that had an offer change
            /// </summary>
            [JsonProperty("itemCondition")]
            public string ItemCondition { get; set; }

            /// <summary>
            /// The marketplace identifier of the item that had an offer change
            /// </summary>
            [JsonProperty("marketplaceId")]
            public string MarketplaceId { get; set; }

            /// <summary>
            /// The update time for the offer that caused this notification
            /// </summary>
            [JsonProperty("timeOfOfferChange")]
            public string TimeOfOfferChange { get; set; }
        }

        public class Summary
        {
            /// <summary>
            /// A list that contains the total number of offers that are eligible for the Buy Box for the
            /// given conditions and fulfillment channels
            /// </summary>
            [JsonProperty("buyBoxEligibleOffers")]
            public OfferCount[] BuyBoxEligibleOffers { get; set; }

            /// <summary>
            /// A list that contains the Buy Box price of the item for the given conditions
            /// </summary>
            [JsonProperty("buyBoxPrices", NullValueHandling = NullValueHandling.Ignore)]
            public BuyBoxPrice[] BuyBoxPrices { get; set; }

            /// <summary>
            /// A list that contains the total number of offers for the item for the given conditions and
            /// fulfillment channels
            /// </summary>
            [JsonProperty("numberOfOffers")]
            public OfferCount[] NumberOfOffers { get; set; }

            [JsonProperty("referencePrice")]
            public ReferencePrice ReferencePrice { get; set; }

            /// <summary>
            /// A list that contains the sales rankings of the asin in various product categories
            /// </summary>
            [JsonProperty("salesRankings", NullValueHandling = NullValueHandling.Ignore)]
            public SalesRank[] SalesRankings { get; set; }
        }

        public class OfferCount
        {
            [JsonProperty("condition")]
            public string Condition { get; set; }

            [JsonProperty("fulfillmentType")]
            public FulfillmentType FulfillmentType { get; set; }

            /// <summary>
            /// The total number of offers for the specified condition and fulfillment channel
            /// </summary>
            [JsonProperty("offerCount")]
            public long OfferCountOfferCount { get; set; }
        }

        public class BuyBoxPrice
        {
            [JsonProperty("condition")]
            public string Condition { get; set; }

            [JsonProperty("landedPrice")]
            public MoneyType LandedPrice { get; set; }

            [JsonProperty("listingPrice")]
            public MoneyType ListingPrice { get; set; }

            [JsonProperty("points", NullValueHandling = NullValueHandling.Ignore)]
            public Points Points { get; set; }

            [JsonProperty("shipping")]
            public MoneyType Shipping { get; set; }
        }

        /// <summary>
        /// A set of reference prices for the given ASIN
        /// </summary>
        public class ReferencePrice
        {
            [JsonProperty("averageSellingPrice", NullValueHandling = NullValueHandling.Ignore)]
            public MoneyType AverageSellingPrice { get; set; }

            [JsonProperty("competitivePriceThreshold", NullValueHandling = NullValueHandling.Ignore)]
            public MoneyType CompetitivePriceThreshold { get; set; }

            [JsonProperty("msrpPrice", NullValueHandling = NullValueHandling.Ignore)]
            public MoneyType MsrpPrice { get; set; }

            [JsonProperty("retailOfferPrice", NullValueHandling = NullValueHandling.Ignore)]
            public MoneyType RetailOfferPrice { get; set; }
        }

        public class SalesRank
        {
            /// <summary>
            /// The product category for the rank
            /// </summary>
            [JsonProperty("productCategoryId")]
            public string ProductCategoryId { get; set; }

            /// <summary>
            /// The sales rank of the ASIN
            /// </summary>
            [JsonProperty("rank")]
            public long Rank { get; set; }
        }

        /// <summary>
        /// Indicates whether the item is fulfilled by Amazon or by the seller
        /// </summary>
        public enum FulfillmentType { Afn, Mfn };

        internal static class Converter
        {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters =
            {
                FulfillmentTypeConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
            };
        }

        internal class FulfillmentTypeConverter : JsonConverter
        {
            public override bool CanConvert(Type t) => t == typeof(FulfillmentType) || t == typeof(FulfillmentType?);

            public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null) return null;
                var value = serializer.Deserialize<string>(reader);
                switch (value)
                {
                    case "AFN":
                        return FulfillmentType.Afn;
                    case "MFN":
                        return FulfillmentType.Mfn;
                }
                throw new Exception("Cannot unmarshal type FulfillmentType");
            }

            public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
            {
                if (untypedValue == null)
                {
                    serializer.Serialize(writer, null);
                    return;
                }
                var value = (FulfillmentType)untypedValue;
                switch (value)
                {
                    case FulfillmentType.Afn:
                        serializer.Serialize(writer, "AFN");
                        return;
                    case FulfillmentType.Mfn:
                        serializer.Serialize(writer, "MFN");
                        return;
                }
                throw new Exception("Cannot marshal type FulfillmentType");
            }

            public static readonly FulfillmentTypeConverter Singleton = new FulfillmentTypeConverter();
        }
    }
}
