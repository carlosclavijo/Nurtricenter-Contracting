using Contracting.Application.Behaviors;
using Joseco.DDD.Core.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;

namespace Contracting.Test.Application.Behaviors;

public class RequestLoggingPipelineBehaviorTest
{
    private readonly Mock<ILogger<RequestLoggingPipelineBehavior<FakeRequest, Result>>> _loggerMock;
    private readonly RequestLoggingPipelineBehavior<FakeRequest, Result> _behavior;

    public RequestLoggingPipelineBehaviorTest()
    {
        _loggerMock = new Mock<ILogger<RequestLoggingPipelineBehavior<FakeRequest, Result>>>();
        _behavior = new RequestLoggingPipelineBehavior<FakeRequest, Result>(_loggerMock.Object);
    }

    [Fact]
    public async Task HandleIsValid()
    {
        var request = new FakeRequest();
        var result = Result.Success();
        RequestHandlerDelegate<Result> next = (CancellationToken _) => Task.FromResult(Result.Success());
        var response = await _behavior.Handle(request, next, CancellationToken.None);
        Assert.True(response.IsSuccess);

        _loggerMock.Verify(
            l => l.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, _) => v.ToString()!.Contains("Processing request")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()
            ),
            Times.Once
        );

        _loggerMock.Verify(
            l => l.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, _) => v.ToString()!.Contains("Completed request")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()
            ),
            Times.Once
        );
    }

    [Fact]
    public async Task HandleIsInvalid()
    {
        var request = new FakeRequest();
        var error = Error.NotFound("404", "Not found");
		var result = Result.Failure(error);

		Task<Result> next(CancellationToken _ = default) => Task.FromResult(result);
		var response = await _behavior.Handle(request, next, CancellationToken.None);
        Assert.False(response.IsSuccess);

        _loggerMock.Verify(
            l => l.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, _) => v.ToString()!.Contains("Completed request")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()
            ),
            Times.Once
        );
    }

    public class FakeRequest { }
}
