using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Confluent.Kafka;
using DB_Consumer.Models;
using DB_Consumer.Repositories;
using DB_Consumer.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DB_Consumer.Orchestrators
{
    public class ConsumerDbOrchestrator
    {
        private IServiceProvider _serviceProvider;
        ILogger<ConsumerDbOrchestrator> _logger;
        public ConsumerDbOrchestrator(IServiceProvider serviceProvider, ILogger<ConsumerDbOrchestrator> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task Run()
        {
            _logger.LogInformation("Start Running Consumer DB");

            using var kafkaScope = _serviceProvider.CreateScope();

            KafkaConsumerService kafka = kafkaScope.ServiceProvider.GetRequiredService<KafkaConsumerService>();
            
            while (true)
            {
                ConsumeResult<Null, string> result = kafka.Consume();

                if (result == null || result.Message == null)
                {
                    _logger.LogInformation("kafka consume and found nothing");
                    continue;
                }
                
                _logger.LogInformation($"Consume New Event. topic: {result.Topic} | offset: {result.Offset} | Data: {result.Message.Value}");

                using var dbScope = _serviceProvider.CreateScope();

                SurveysRepository repository = dbScope.ServiceProvider.GetRequiredService<SurveysRepository>();
                Survey? survey = new();

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                try
                {
                    survey = JsonSerializer.Deserialize<Survey>(result.Message.Value, options);
                }
                catch(JsonException ex)
                {
                    _logger.LogError($"Error While Serializing The Survey! : {ex}");
                    continue;
                }

                if (survey == null)
                {
                    _logger.LogError($"Error while Serializing The Survey!");
                    continue;
                }

                await repository.AddSurvey(survey);
                



                
            }
        }

        
    }
}