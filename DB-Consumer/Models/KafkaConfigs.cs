using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace DB_Consumer.Models
{
    public class KafkaConfigs
    {
        public string BootstrapServers { get; set; } = string.Empty;
        public string GroupId { get; set; } = string.Empty;
        public string ProcessingDataTopicName { get; set; } = string.Empty;

    }
}