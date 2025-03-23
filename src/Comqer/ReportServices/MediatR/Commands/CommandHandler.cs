using MediatR;

namespace Comqer.ReportServices.MediatR.Commands;

public class CommandHandler : IRequestHandler<Command, string> {
    private readonly ILogger<CommandHandler> _logger;

    public CommandHandler(ILogger<CommandHandler> logger) {
        _logger = logger;
    }

    public Task<string> Handle(Command request, CancellationToken cancellationToken) {
        _logger.LogDebug($"> Run command with input {request.Input}");
        _logger.LogDebug($"- Starting command with input {request.Input}");
        Task.Delay(10000).Wait();
        _logger.LogDebug($"< Run command with input {request.Input}");
        return Task.FromResult($"Command result for {request.Input}");
    }
}