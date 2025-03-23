using Comqer.ReportServices.MediatR;
using Comqer.ReportServices.Plain;
using Microsoft.FeatureManagement;

namespace Comqer.ReportServices;

internal class ReportServiceSelector : IReportServiceSelector {
    private readonly IFeatureManager _featureManager;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ReportServiceSelector> _logger;

    public ReportServiceSelector(IFeatureManager featureManager, IServiceProvider serviceProvider, ILogger<ReportServiceSelector> logger) {
        _featureManager = featureManager;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task<IReportService> GetService() {
        if (await _featureManager.IsEnabledAsync(PlainReportService.FeatureToggle)) {
            _logger.LogDebug($"Selecting {nameof(PlainReportService)}");
            return _serviceProvider.GetRequiredService<PlainReportService>();
        } else if (await _featureManager.IsEnabledAsync(MediatrReportService.FeatureToggle)) {
            _logger.LogDebug($"Selecting {nameof(MediatrReportService)}");
            return _serviceProvider.GetRequiredService<MediatrReportService>();
        } else {
            _logger.LogError("Invalid configuration, no known worker service configured.");
            throw new InvalidOperationException("Invalid configuration, no known worker service configured.");
        }
    }
}