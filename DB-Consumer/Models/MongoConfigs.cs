using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_Consumer.Models
{
    public class MongoConfigs
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string SurveysCollectionName { get; set; } = null!;
    }
}