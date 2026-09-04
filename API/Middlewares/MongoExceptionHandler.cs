using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace API.Middlewares
{
    public class MongoExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<MongoExceptionHandler> _logger;
        public MongoExceptionHandler(ILogger<MongoExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {

            if (exception is MongoAuthenticationException || exception is TimeoutException || exception is MongoServerException  || exception is MongoConnectionException )
            {
                _logger.LogError(exception, "Error While Connecting To the data base");

                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Error Connecting To data base",

                };
                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
                return true;
            }

            return false;

        }
    }
}