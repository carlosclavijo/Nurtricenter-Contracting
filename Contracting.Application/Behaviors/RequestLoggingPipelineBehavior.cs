﻿using Joseco.DDD.Core.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Contracting.Application.Behaviors;

public sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>(
	ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
	: IPipelineBehavior<TRequest, TResponse>
	where TRequest : class
	where TResponse : Result
{
	public async Task<TResponse> Handle(
		TRequest request,
		RequestHandlerDelegate<TResponse> next,
		CancellationToken cancellationToken)
	{
		string requestName = typeof(TRequest).Name;

		logger.LogInformation("Processing request {RequestName}", requestName);

		TResponse result = await next();

		if (result.IsSuccess)
		{
			logger.LogInformation("Completed request {RequestName}", requestName);
		}
		else
		{
			LogError(result.Error, requestName);
		}

		return result;

	}

	private void LogError(Error error, string requestName)
	{
		using (LogContext.PushProperty("Error", error, true))
		{
			logger.LogError("Completed request {RequestName} with error", requestName);
		}
	}
}
