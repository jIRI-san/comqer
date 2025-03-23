using System.Drawing;
using MediatR;

namespace Comqer.ReportServices.MediatR;

internal class MediatrReportService : IReportService {
  public const string FeatureToggle = "WorkerService.MediatR";

  private readonly IMediator _mediator;

  public MediatrReportService(IMediator mediator) {
    _mediator = mediator;
  }

  public Task<string> Command(string input) {
    return  _mediator.Send(new Commands.Command(input));
  }

  public Task<string> Query(string input) {
    return _mediator.Send(new Queries.Query(input));
  }
}