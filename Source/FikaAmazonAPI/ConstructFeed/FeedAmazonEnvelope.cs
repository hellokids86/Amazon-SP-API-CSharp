﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using static FikaAmazonAPI.Utils.Constants;

namespace FikaAmazonAPI.ConstructFeed
{
    [XmlRoot(ElementName = "AmazonEnvelope")]

    [Serializable]
    public class FeedAmazonEnvelope
    {

        [XmlAttribute(AttributeName = "noNamespaceSchemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string noNamespaceSchemaLocation { get; set; }

        [XmlElement(Order = 1)]
        public FeedHeader Header { get; set; }

        [XmlElement(Order = 2)]
        public FeedMessageType? MessageType { get; set; }
        
        [XmlElement(Order = 3)]
        public bool? PurgeAndReplace { get; set; }
        [XmlIgnore]
        public bool PurgeAndReplaceSpecified { get { return PurgeAndReplace.HasValue; } }

        [XmlElement(ElementName = "Message", Order = 4)]
        public List<BaseMessage> Message;

    }
}
