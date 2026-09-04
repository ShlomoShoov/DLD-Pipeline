using DB_Consumer.DAL;
using DB_Consumer.Models;
using DB_Consumer.Orchestrators;
using DB_Consumer.Repositories;
using DB_Consumer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var configs = new ConfigurationBuilder()
                    .AddEnvironmentVariables()
                    .Build();


KafkaConfigs kafkaConfigs = new KafkaConfigs()
                            {                         
                            };

configs.GetSection("kafka").Bind(kafkaConfigs);


MongoConfigs mongoConfigs = new MongoConfigs
                            {                          
                            };
configs.GetSection("mongo").Bind(mongoConfigs);
System.Console.WriteLine(kafkaConfigs.BootstrapServers);
System.Console.WriteLine(mongoConfigs.ConnectionString);
System.Console.WriteLine(mongoConfigs.DatabaseName);
ServiceCollection serviceDescriptors = new ServiceCollection();

serviceDescriptors.AddScoped<KafkaConsumerService>();
serviceDescriptors.AddScoped<MongoDbContext>();
serviceDescriptors.AddScoped<SurveysRepository>();
serviceDescriptors.AddScoped<ConsumerDbOrchestrator>();
serviceDescriptors.AddLogging(builder=>
                            {
                                builder.AddConsole();
                                builder.SetMinimumLevel(LogLevel.Information);
                            });

serviceDescriptors.AddSingleton(kafkaConfigs);
serviceDescriptors.AddSingleton(mongoConfigs);

ServiceProvider serviceProvider = serviceDescriptors.BuildServiceProvider();

ConsumerDbOrchestrator orchestrator = serviceProvider.GetRequiredService<ConsumerDbOrchestrator>();

await orchestrator.Run();