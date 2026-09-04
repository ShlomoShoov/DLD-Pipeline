using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DAL;
using API.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace API.Repositories
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

        public async Task<object> GetUsesDocumentation()
        {
            var filter = Builders<Survey>.Filter.Eq(s=> s.UsesDocumentation, true);
            return new {Count = await _context.Surveys.CountDocumentsAsync(filter)};
        }

        public async Task<object> GetUseDocsAndAi()
        {
            var filter = Builders<Survey>.Filter.And(
                                        Builders<Survey>.Filter.Eq(s => s.UsesDocumentation, true)
                                        ,Builders<Survey>.Filter.Eq(s=> s.UsesAIForLearning, true));
            return new { Count = await _context.Surveys.CountDocumentsAsync(filter) };
        }

        public async Task<object> GetByAiAcc(string aiAcc)
        {
            var filter = Builders<Survey>.Filter.Eq(s=> s.AIAcc, aiAcc);
            return new { Count = await _context.Surveys.CountDocumentsAsync(filter) };
        }

        public async Task<object> GetByExperienceLevel(string level)
        {
            var filter = Builders<Survey>.Filter.Eq(s => s.ExperienceLevel, level);
            return new { Count = await _context.Surveys.CountDocumentsAsync(filter) };
        }

        public async Task<IEnumerable<Survey>> GetTopBackendAi()
        {
    
            return await _context.Surveys.Find(s=> s.DevType!= null && s.DevType.Contains("Developer, back-end") && s.UsesAIForLearning)
                            .SortByDescending(s=> s.YearsCode)
                            .Limit(20)
                            .ToListAsync();
        }




    }
}