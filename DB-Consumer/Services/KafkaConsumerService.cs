using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using DB_Consumer.Models;

namespace DB_Consumer.Services
{
    public class KafkaConsumerService
    {
        private IConsumer<Null, string> _consumer;
        public KafkaConsumerService(KafkaConfigs configs)
        {
            ConsumerConfig consumerConfigs = new ConsumerConfig
            {
                BootstrapServers = configs.BootstrapServers,
                GroupId = configs.GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Null, string>(consumerConfigs).Build();
            _consumer.Subscribe(configs.ProcessingDataTopicName);
        }

        public ConsumeResult<Null, string> Consume()
        {
            return _consumer.Consume();
        }

        public void Dispose()
        {
            _consumer.Close();
            _consumer.Dispose();
        }
    }
}