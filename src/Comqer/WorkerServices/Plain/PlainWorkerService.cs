namespace Comqer.WorkerServices.Plain;

internal class PlainWorkerService : IWorkerService {
  public const string FeatureToggle = "WorkerService.Plain";

  private readonly ILogger<PlainWorkerService> _logger;
  private readonly SemaphoreSlim _commandSemaphore = new(1, 1);
  private readonly SemaphoreSlim _querySemaphore = new(1, 1);

  public PlainWorkerService(ILogger<PlainWorkerService> logger) {
    _logger = logger;
  }

  public Task<string> Command(string input) {
    _logger.LogDebug($"> Run command for input {input}");
    _commandSemaphore.Wait();
    _logger.LogDebug($"- Start command for input {input}");
    Task.Delay(10000).Wait();
    _commandSemaphore.Release();
    _logger.LogDebug($"< Command finished for input {input}");
    return Task.FromResult($"Command result for {input}");
  }

  public Task<string> Query(string input) {
    _logger.LogDebug($"> Run query for input {input}");
    _querySemaphore.Wait();
    _logger.LogDebug($"- Start query for input {input}");
    _querySemaphore.Release();
    _logger.LogDebug($"< Query finished for input {input}");
    return Task.FromResult($"Query result for {input}");
  }
}