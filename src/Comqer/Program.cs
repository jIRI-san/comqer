using Comqer.Features;
using Comqer.Features.Api;
using Comqer.ReportServices;
using Comqer.ReportServices.MediatR;
using Comqer.ReportServices.Plain;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddFeatureManagement();
builder.Logging.ClearProviders();
builder.Logging.AddSimpleConsole(options => {
    options.IncludeScopes = false;
    options.SingleLine = true;
    options.TimestampFormat = "[HH:mm:ss] ";
});

// register some worker services classes
builder.Services.AddSingleton<IReportServiceSelector, ReportServiceSelector>();
builder.Services.AddSingleton<PlainReportService>();
builder.Services.AddSingleton<MediatrReportService>();

// register features manually, because this is not Autofac and we cannot
// register all implementations of interface in assembly, it seems...
builder.Services.AddSingleton<IFeatureRegistration, ApiFeature>();

// but look, MediatR can do it...
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var features = app.Services.GetServices<IFeatureRegistration>();
foreach (var feature in features) {
    feature.Register(app);
}

app.Run();

