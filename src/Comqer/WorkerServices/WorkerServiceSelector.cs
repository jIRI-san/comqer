using Comqer.WorkerServices.MediatR;
using Comqer.WorkerServices.Plain;
using Microsoft.FeatureManagement;

namespace Comqer.WorkerServices;

internal class WorkerServiceSelector : IWorkerServiceSelector {
    private readonly IFeatureManager _featureManager;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<WorkerServiceSelector> _logger;

    public WorkerServiceSelector(IFeatureManager featureManager, IServiceProvider serviceProvider, ILogger<WorkerServiceSelector> logger) {
        _featureManager = featureManager;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task<IWorkerService> GetService() {
        if (await _featureManager.IsEnabledAsync(PlainWorkerService.FeatureToggle)) {
            _logger.LogDebug($"Selecting {nameof(PlainWorkerService)}");
            return _serviceProvider.GetRequiredService<PlainWorkerService>();
        } else if (await _featureManager.IsEnabledAsync(MediatrWorkerService.FeatureToggle)) {
            _logger.LogDebug($"Selecting {nameof(MediatrWorkerService)}");
            return _serviceProvider.GetRequiredService<MediatrWorkerService>();
        } else {
            _logger.LogError("Invalid configuration, no known worker service configured.");
            throw new InvalidOperationException("Invalid configuration, no known worker service configured.");
        }
    }
}