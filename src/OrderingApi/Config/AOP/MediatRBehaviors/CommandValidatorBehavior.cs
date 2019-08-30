using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingApi.Config.AOP.MediatRBehaviors
{
    public class CommandValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<CommandValidatorBehavior<TRequest, TResponse>> _logger;
        private readonly IValidator<TRequest> _validator;

        public CommandValidatorBehavior(IValidator<TRequest> validator, ILogger<CommandValidatorBehavior<TRequest, TResponse>> logger)
        {
            _validator = validator;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation("----- Validating command {CommandType}");

            var failures = _validator.Validate(request);

            if (!failures.IsValid)
            {
                _logger.LogWarning("Validation errors - {CommandType} - Command: {@Command} - Errors: {@ValidationErrors}", "test", request, failures);

                throw new Exception( 
                    $"Command Validation Errors for type {typeof(TRequest).Name}", new ValidationException("Validation exception", failures.Errors));
            }

            return await next();
        }
    }
}
