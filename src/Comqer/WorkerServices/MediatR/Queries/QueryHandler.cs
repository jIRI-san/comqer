using Comqer.WorkerServices.MediatR.Queries;
using MediatR;

namespace Comqer.Features.MediatR.Queries;

public class QueryHandler : IRequestHandler<Query, string> {
    private readonly ILogger<QueryHandler> _logger;

    public QueryHandler(ILogger<QueryHandler> logger) {
        _logger = logger;
    }

    public Task<string> Handle(Query request, CancellationToken cancellationToken) {
        _logger.LogDebug($"> Run query with input {request.Input}");
        _logger.LogDebug($"- Starting query with input {request.Input}");
        _logger.LogDebug($"< Finished query with input {request.Input}");
        return Task.FromResult($"Query result for {request.Input}");
    }
}