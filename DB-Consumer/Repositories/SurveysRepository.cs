using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DB_Consumer.DAL;
using DB_Consumer.Models;
using DnsClient.Internal;
using Microsoft.Extensions.Logging;

namespace DB_Consumer.Repositories
{
    public class SurveysRepository
    {
        private MongoDbContext _context;
        private ILogger<SurveysRepository> _logger;

        public SurveysRepository(MongoDbContext context, ILogger<SurveysRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddSurvey(Survey survey)
        {
            await _context.Surveys.InsertOneAsync(survey);
        }
    }
}