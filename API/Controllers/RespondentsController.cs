using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RespondentsController : ControllerBase
    {
        private SurveysRepository _repository;
        public RespondentsController(SurveysRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("uses-documentation")]
        public async Task<ActionResult<object>> GetUsesDocumentation()
        {
            return Ok(await _repository.GetUsesDocumentation());
        }

        [HttpGet("uses-docs-and-ai")]
        public async Task<ActionResult<object>> GetUseDocsAndAi()
        {
            return Ok(await _repository.GetUseDocsAndAi());
        }

        [HttpGet("ai-accuracy")]
        public async Task<ActionResult<object>> GetByAiAcc(string accuracy)
        {
            return Ok(await _repository.GetByAiAcc(accuracy));
        }
        [HttpGet("experience-level")]
        public async Task<ActionResult<object>> GetByExperienceLevel(string level)
        {
            return Ok(await _repository.GetByExperienceLevel(level));
        }
        [HttpGet("top-backend-ai")]
        public async Task<ActionResult<IEnumerable<Survey>>> GetTopBackendAi()
        {
            return Ok(await _repository.GetTopBackendAi());
        }





    }
}