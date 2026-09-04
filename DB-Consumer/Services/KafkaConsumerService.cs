using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using DB_Consumer.Models;
using DnsClient.Internal;
using Microsoft.Extensions.Logging;

namespace DB_Consumer.Services
{
    public class KafkaConsumerService
    {
        private IConsumer<Null, string> _consumer;
        ILogger<KafkaConsumerService> _logger;
        public KafkaConsumerService(KafkaConfigs configs, ILogger<KafkaConsumerService> logger)
        {
            ConsumerConfig consumerConfigs = new ConsumerConfig
            {
                BootstrapServers = configs.BootstrapServers,
                GroupId = configs.GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Null, string>(consumerConfigs).Build();
            _consumer.Subscribe(configs.ProcessingDataTopicName);
            _logger = logger;
        }


        public ConsumeResult<Null, string> Consume()
        {
            while (true)
            {
                try
                {
                    return _consumer.Consume();
                }
                catch (ConsumeException e)
                {
                    if (e.Error.Code == ErrorCode.UnknownTopicOrPart)
                    {
                        _logger.LogWarning($"Topic is not available yet. Retrying in 5 seconds...");
                        Task.Delay(TimeSpan.FromSeconds(5)).Wait();
                    }
                    else
                    {
                        _logger.LogError($"Consume error: {e.Error.Reason}");
                    }
                }
            }

            
        }

        public void Dispose()
        {
            _consumer.Close();
            _consumer.Dispose();
        }
    }
}