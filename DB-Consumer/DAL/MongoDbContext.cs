using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DB_Consumer.Models;
using MongoDB.Driver;

namespace DB_Consumer.DAL
{
    public class MongoDbContext
    {
        public IMongoCollection<Survey> Surveys;

        public MongoDbContext(MongoConfigs configs)
        {
            IMongoClient mongoClient = new MongoClient(configs.ConnectionString);
            Surveys = mongoClient.GetDatabase(configs.DatabaseName).GetCollection<Survey>(configs.SurveysCollectionName);
        }
    }
}