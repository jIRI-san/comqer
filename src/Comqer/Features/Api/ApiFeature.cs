using Comqer.WorkerServices;
using MediatR;
using Microsoft.FeatureManagement;

namespace Comqer.Features.Api;

internal class ApiFeature : IFeatureRegistration {
    public const string ApiFeatureToggle = "Api";

    private readonly IWorkerServiceSelector _workerServiceSelector;
    private readonly IFeatureManager _featureManager;
    private readonly ILogger<ApiFeature> _logger;

    public ApiFeature(IWorkerServiceSelector workerServiceSelector, IFeatureManager featureManager, ILogger<ApiFeature> logger) {
        _workerServiceSelector = workerServiceSelector;
        _featureManager = featureManager;
        _logger = logger;
    }

    public async void Register(WebApplication app) {
        if(!await _featureManager.IsEnabledAsync(ApiFeatureToggle)) {
            _logger.LogInformation($"{nameof(ApiFeature)} is not enabled");
            return;
        }

        _logger.LogInformation($"{nameof(ApiFeature)} enabled, registering feature");

        app.MapPost("/command", async (string input) => {
            _logger.LogDebug($"/command endpoint invoked");
            var workerService = await _workerServiceSelector.GetService();
            var result = await workerService.Command(input);
            return result;
        })
        .WithName("Command");

        app.MapGet("/query", async (string input) => {
            _logger.LogDebug($"/query endpoint invoked");
            var workerService = await _workerServiceSelector.GetService();
            var result = await workerService.Query(input);
            return result;
        })
        .WithName("Query");
    }
}