using MediatR;

namespace Comqer.WorkerServices.MediatR.Queries;

public class Query(string input) : IRequest<string> {
  private readonly string _input = input;

  public string Input => _input;
}