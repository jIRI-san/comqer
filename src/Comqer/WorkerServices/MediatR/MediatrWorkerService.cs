using System.Drawing;
using MediatR;

namespace Comqer.WorkerServices.MediatR;

internal class MediatrWorkerService : IWorkerService {
  public const string FeatureToggle = "WorkerService.MediatR";

  private readonly IMediator _mediator;

  public MediatrWorkerService(IMediator mediator) {
    _mediator = mediator;
  }

  public Task<string> Command(string input) {
    return  _mediator.Send(new Commands.Command(input));
  }

  public Task<string> Query(string input) {
    return _mediator.Send(new Queries.Query(input));
  }
}